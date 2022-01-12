using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHunter : MonoBehaviour
{
	[SerializeField] private float playerSpeed;
	// private float size;

	private float timeNeeded = 1f;
	private float timeElapsedLerp = 0;

	private readonly string pathFindingType = "bfs";

	private IPathFinding randomAI, bfsAI;
	private GameClock gameClock;
	private List<Vector3> path;
	private Vector3 startingPosition;

	private bool pathGenerationGate;
	private int index;
	private int randomMoveCap, pathMoveCap;

	void Start()
	{
		AIInitialization();
		startingPosition = transform.position;
		gameClock = new GameClock();
		// size = GameObject.Find("Game").GetComponent<GameLoop>().size;
		pathGenerationGate = false;
		randomMoveCap = 10;
	}

	void Update()
	{
		gameClock.Update(Time.time);

		switch(pathFindingType)
		{
			case "random":
				RandomMovement();
				break;
			case "bfs":
				BFSMovement();
				break;
		}
	}

	public void RandomMovement()
	{
		if (!pathGenerationGate)
		{
			path = randomAI.Search(transform.position, transform.position);
			index = 1;
			pathGenerationGate = true;
		}

		if (gameClock.Step() == gameClock.GetClockGate())
		{
			for (int i = 0; i < path.Count; i++)
				Debug.Log(path[i].x + " " + path[i].y);

			if (index < randomMoveCap)
			{
				gameClock.IncrementClockGate();
				TeleportMovementTest(path[index]);
				index += 1;
			}
		}
	}

	public void BFSMovement()
	{
		if (secondPassed())
		{
			Vector3 destination = GameObject.FindWithTag("Prey").transform.position;
			path = bfsAI.Search(transform.position, destination);
			for (int i = 0; i < 5; i++)
			{
				Debug.Log(path[i]);
			}
			Debug.Log("===============");

			gameClock.IncrementClockGate();

			startingPosition = transform.position;
		}

		TeleportMovementTest(path[1]);
	}

	public bool secondPassed()
	{
		return (gameClock.Step() == gameClock.GetClockGate());
	}

	public void TeleportMovementTest(Vector3 destination)
	{
		transform.position = destination;
	}

	public void AIInitialization()
	{
		switch(pathFindingType)
		{
			case "random":
				randomAI = new RandomAI(2);
				break;
			case "bfs":
				bfsAI = new BreadthFirstAI();
				break;
		}
	}

	public void LerpTest(Vector3 startPosition, Vector3 destination)
	{
		Debug.Log("lerping");
		timeElapsedLerp += Time.deltaTime;
		float pathPercentage = timeElapsedLerp/timeNeeded;
		transform.position = Vector3.Lerp(startPosition, destination, pathPercentage);
		Debug.Log("lerping done");
	}
	
	public void ManualMovement()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(x, y, 0);
	
		transform.Translate((movement * playerSpeed) * Time.deltaTime);
	}
}