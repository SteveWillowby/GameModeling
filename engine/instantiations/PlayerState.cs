using System;

public class PlayerState
{
    protected static int numPlayers;
    protected static Player[] all;
    protected static bool[] active;
    protected static bool[] former;

    public static void Init(int nP, bool[] a, bool[] f)
    {
        numPlayers = nP;
        active = a;
        former = f;
        all = new Player[nP];
    }

    protected static int Index(Player p)
    {
        for(int i = 0; i < numPlayers; i++)
        {
            if(p == all[i])
            {
                return i;
            }
        }
        return -1;
    }

    public static bool Active(int i)
    {
        return active[i];
    }

    public static bool Active(Player p)
    {
        return active[Index(p)];
    }

    protected static void Swap()
    {
        bool[] temp = active;
        active = former;
        former = temp;
    }

    public static void Shift(string shift, Player p)
    {
        Swap();

        int playerIdx = Index(p);
        if(playerIdx == -1)
        {
            throw new Exception("An unregistered player performed a move.");
        }
        Func<int, int> location;
        Func<int, bool> val;
        location = i => i;
        val = i => true;
        bool save;

        switch(shift)
        {
            case "all":
                break;
            case "player":
                val = i => (i == playerIdx);
                break;
            case "player.left":
                val = i => (i == (playerIdx + 1) % numPlayers);
                break;
            case "player.right":
                val = i => (i == (playerIdx - 1 + numPlayers) % numPlayers);
                break;
            case "players.left":
                val = i => former[(i + 1) % numPlayers];
                break;
            case "players.right":
                val = i => former[(i - 1 + numPlayers) % numPlayers];
                break;
            case "former":
                val = i => active[i];
                break;
            case "former.left":
                save = active[0];
                val = i => (i == numPlayers - 1 ? save : active[i + 1]);
                break;
            case "former.right":
                save = active[numPlayers - 1];
                location = i => (numPlayers - 1) - i;
                val = i => (i == 0 ? save : active[i - 1]);
                break;
            default:
                break;
        }

        for(int i = 0; i < numPlayers; i++)
        {
            active[location(i)] = val(i);
        }
    }
}
