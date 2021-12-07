using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPrey : MonoBehaviour
{
	[SerializeField] private float playerSpeed; //CHANGE THIS TO THE GENERAL MOVEMENT SCRIPT, REMOVE MOVEMENTHUNTER
	private float size;

	private float timeNeeded = 1f;
	private float timeElapsedLerp = 0;

	private readonly string pathFindingType = "random";

	IPathFinding randomAI;
	private GameClock gameClock;

	void Start()
	{
		gameClock = new GameClock();
		size = GameObject.Find("Game").GetComponent<GameLoop>().size;
		randomAI = new RandomAI();
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
					TeleportMovementTest(transform.position, path[index]);
					index += 1;
				}
				break;
		}
	}

	public void TeleportMovementTest(Vector3 startPosition, Vector3 destination)
	{
		transform.position = destination;
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