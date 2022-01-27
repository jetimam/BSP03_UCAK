public class GameClock
{
	private float clock = 0;
	private float clockGate = 0;
	private float min = 0;

	public GameClock() {}

	public void Update(float time)
	{
		if (time > min)
		{
			min += 0.5f;
			clock += 0.5f;
		}
	}

	public float Step()
	{
		return this.clock;
	}

	public float GetClockGate()
	{
		return this.clockGate;
	}

	public void IncrementClockGate()
	{
		this.clockGate += 0.5f;
	}
}