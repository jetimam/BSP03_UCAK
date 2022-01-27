using System.Collections.Generic;
using UnityEngine;

public class MovementHunter : MonoBehaviour
{
	private IPathFinding bfsAI;
	private GameClock gameClock;
	private List<Vector3> path;

	void Start()
	{
		bfsAI = new BreadthFirstAI();
		gameClock = new GameClock();
	}

	void Update()
	{
		gameClock.Update(Time.time);

		BFSMovement();
	}

	public void BFSMovement()
	{
		if (timePassed())
		{
			Vector3 destination = GameObject.FindWithTag("Prey").transform.position;
			path = bfsAI.Search(transform.position, destination);

			gameClock.IncrementClockGate();
		}

		if (path.Count == 1)
		{
			transform.position = path[0];
		}
		try //this section is for when the distance to the prey is only one step, 
		{	//so the BFS path is an array of only 1 element.
			transform.position = path[1];
		} catch{}
	}

	public bool timePassed()
	{
		return (gameClock.Step() == gameClock.GetClockGate());
	}
}