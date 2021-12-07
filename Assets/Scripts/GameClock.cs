using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClock
{
	private int clock = 0;
	private int clockGate = 0;
	private int min = 0;
	private int max = 1;

	public GameClock() {}

	public void Update(float time)
	{
		if (time > min && time < max)
		{
			min += 1;
			max += 1;
			clock += 1;
		}
	}

	public int Step()
	{
		return this.clock;
	}

	public int GetClockGate()
	{
		return this.clockGate;
	}

	public void SetClockGate(int clockGate)
	{
		this.clockGate = clockGate;
	}
}