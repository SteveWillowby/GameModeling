using System;

public class Timer
{
    protected static DateTime startTime = new DateTime(0, 1, 1);
    protected static int tickTime = 0;

    public static void SetTimer(int seconds)
    {
        startTime = DateTime.UtcNow;
        tickTime = seconds;
    }

    public static bool TimeOnTheClock()
    {
        TimeSpan t = DateTime.UtcNow - startTime;
        return tickTime > (int) t.TotalSeconds;
    }
}
