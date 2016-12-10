using System;

/* Used in conjunction with GameState and Timer, this class
 * maintains static data on the players.
 * 
 * This class keeps track of which players are active, and
 * its shift function allows for various ways to change the
 * set of active players.
 */

public class PlayerState
{
    protected static int numPlayers;
    protected static Player[] all;
    protected static bool[] active;
    /* former stores whichever players were previously active.
     * See the Shift() function for its usage.
     */
    protected static bool[] former;

    //This should be called once to initialize things
    public static void Init(int nP, bool[] a, bool[] f)
    {
        numPlayers = nP;
        active = a;
        former = f;
        all = new Player[nP];
    }

    /* Converts a reference to a player to the index in
     * all[] where that player is stored.
     * Returns -1 if the player is not found.
     */
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

    //A helper function for Shift
    protected static void Swap()
    {
        bool[] temp = active;
        active = former;
        former = temp;
    }

    /* Shift() changes which players are active.
     *
     * The shift argument specifies which type of shift will be performed.
     *
     * The player argument is used to indicate who the shift is relative to.
     * Specifically, see the "player", "player.left" and "player.right"
     * options in the switch statement.
     */
    public static void Shift(string shift, Player p)
    {
        //Swap the active and former values
        Swap();
        //Former is now correct; we need to update active.

        int playerIdx = Index(p);
        if(playerIdx == -1)
        {
            throw new Exception("An unregistered player performed a move.");
        }

        /* val() maps player indices to a value for whether or not
         * they are now active.
         *
         * location() simply specifies an ordering in which this
         * mapping is performed (currenly only needed in former.right).
         */
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
                /* In former.left and former.right we want to shift all
                 * the values by one index. Hence the save variable.
                 */
                save = active[0];
                val = i => (i == numPlayers - 1 ? save : active[i + 1]);
                break;
            case "former.right":
                save = active[numPlayers - 1];
                /* Here we process the array from end to beginning due
                 * to the nature of val().
                 */
                location = i => (numPlayers - 1) - i;
                val = i => (i == 0 ? save : active[i - 1]);
                break;
            default:
                break;
        }

        //Actually perform the mapping
        for(int i = 0; i < numPlayers; i++)
        {
            active[location(i)] = val(location(i));
        }
    }
}
