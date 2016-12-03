using System;

public class Player
{
    protected ObjectSet owns;

    public Player()
    {
        owns = new ObjectSet();
    }

    public override bool Equals(System.Object o)
    {
        return o != null && Object.ReferenceEquals(this, o);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public void Own(Object o)
    {
        owns.Add(o);
    }

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

    public void Disown(Object o)
    {
        owns.RemoveFirst(o.Equals, a => {});
    }
}
