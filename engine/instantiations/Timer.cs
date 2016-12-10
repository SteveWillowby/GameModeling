using System;

/* Used in conjunction with GameState and PlayerState, this class
 * maintains static data on the game's timer.
 *
 * Once set, the timer immediately begins counting down. All you can
 * currently query is whether the timer has run out or not.
 *
 * The timer can be set before it finishes running out.
 */

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
