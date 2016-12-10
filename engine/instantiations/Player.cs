using System;

/* Represents a single player.
 * 
 * Whether or not a player is active is kept track of in PlayerState.
 *
 * This class simply maintains which objects a player owns and
 * allows for querying of that data.
 */

public class Player
{
    protected ObjectSet owns;

    public Player()
    {
        owns = new ObjectSet();
    }

    //As with Object, we override Equals so we can use == to
    //compare players
    public override bool Equals(System.Object o)
    {
        return o != null && Object.ReferenceEquals(this, o);
    }

    //And as with Object, this function must be overridden if
    //Equals is overridden
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    //This player will now own o
    public void Own(Object o)
    {
        owns.Add(o);
    }

    //Removes this player's ownership of o
    public void Disown(Object o)
    {
        owns.RemoveFirst(o.Equals, a => {});
    }

    /////////////////// Checkers ////////////////////
    public bool Owns(Object o)
    {
        return owns.Contains(o.Equals) != null;
    }

    public bool Owns(string t)
    {
        return owns.Contains(Object.HasType(t)) != null;
    }

    public bool OwnsLess(string t, int n)
    {
        return owns.Count(Object.HasType(t)) < n;
    }

    public bool OwnsMore(string t, int n)
    {
        return owns.Count(Object.HasType(t)) > n;
    }
}
