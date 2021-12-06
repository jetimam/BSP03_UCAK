using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[SerializeField] private float playerSpeed;
	private float size;
	private GameClock gameClock;

	private float timeNeeded = 1f;
	private float timeElapsedLerp = 0;

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
		
		Vector3 destination = randomAI.Update(transform.position);

		switch(pathFindingType)
		{
			case "random":
				LerpTest(transform.position, destination, size);
				break;
		}
	}

	public void LerpTest(Vector3 startPosition, Vector3 destination, float size)
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