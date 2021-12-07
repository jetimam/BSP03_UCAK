using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[SerializeField] private float playerSpeed;
	private float size;
	private GameClock gameClock;

	private float timeNeeded = 1f;
	private float timeElapsedLerp;

	private readonly string pathFindingType = "random";

	IPathFinding randomAI;

	void Start()
	{
		gameClock = GameObject.Find("Game").GetComponent<GameLoop>().gameClock;
		size = GameObject.Find("Game").GetComponent<GameLoop>().size;
		randomAI = new RandomAI(gameClock);
	}

	void Update()
	{
		gameClock.Update(Time.time);
		List<Vector3> path = randomAI.Search(transform.position);

		switch(pathFindingType)
		{
			case "random":
				for (int i = 0; i < 10; i++)
				{
					if (Time.time == gameClock.Step())
					{
						timeElapsedLerp = 0;
						LerpTest(transform.position, path[i]);
					}
				}
				break;
		}
	}

	public void LerpTest(Vector3 startPosition, Vector3 destination)
	{
		timeElapsedLerp += Time.deltaTime;
		float pathPercentage = timeElapsedLerp/timeNeeded;
		transform.position = Vector3.Lerp(startPosition, destination, pathPercentage);
	}
	
	public void ManualMovement()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(x, y, 0);
	
		transform.Translate((movement * playerSpeed) * Time.deltaTime);
	}
}