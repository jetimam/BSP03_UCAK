using System.Collections.Generic;
using UnityEngine;

public class MovementPrey : MonoBehaviour
{
	private IPathFinding randomAI;
	private GameClock gameClock;
	private List<Vector3> path;

	void Start()
	{
		gameClock = new GameClock();
		randomAI = new RandomAI(1);
	}

	void Update()
	{
		gameClock.Update(Time.time);

		RandomMovement();
	}

	public void RandomMovement()
	{
		if (timePassed())
		{
			path = randomAI.Search(transform.position, transform.position);

			gameClock.IncrementClockGate();
		}

		transform.position = path[0];
	}

	public bool timePassed()
	{
		return (gameClock.Step() == gameClock.GetClockGate());
	}
}