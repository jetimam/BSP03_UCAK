using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPrey : MonoBehaviour
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
		gameClock.Update(Time.time);

		switch(pathFindingType)
		{
			case "random":
				int index = 0;
				List<Vector3> path = randomAI.Search(transform.position);
				if (gameClock.Step() == gameClock.GetClockGate())
				{
					gameClock.SetClockGate(gameClock.GetClockGate()+1);
					// timeElapsedLerp = 0;
					TeleportMovementTest(transform.position, path[index]);
					index += 1;
				}
				break;
		}
	}

	public void TeleportMovementTest(Vector3 startPosition, Vector3 destination)
	{
		transform.position = destination;
		// transform.Translate((destination * playerSpeed) * Time.deltaTime);
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