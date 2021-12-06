using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClock
{
	private int clock = 0;

	public GameClock() {}

	public void Update()
	{
		if (Time.time == Math.Truncate(Time.time))
		{
			clock += 1;
		}
	}

	public int Step()
	{
		return this.clock;
	}
}