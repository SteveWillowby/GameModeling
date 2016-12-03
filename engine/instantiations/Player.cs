using System;

public class Player
{
    protected ObjectSet owns;

    public Player()
    {
        owns = new ObjectSet();
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
        return owns.Contains(o => o.type == t) != null;
    }

    public bool OwnsLess(string t, int n)
    {
        ObjectSet s = owns.Subset(o => o.type == t);
        return s.Size() < n;
    }

    public bool OwnsMore(string t, int n)
    {
        ObjectSet s = owns.Subset(o => o.type == t);
        return s.Size() > n;
    }

    public void Disown(Object o)
    {
        owns.RemoveFirst(o.Equals, a => {});
    }
}
