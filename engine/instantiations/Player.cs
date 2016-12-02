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
        
    }

    public bool Owns(Object o)
    {
        return true;
    }

    public bool Disown(Object o)
    {
        return true;
    }
}
