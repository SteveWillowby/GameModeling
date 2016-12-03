using System;

public class Effect
{
    protected Action<Object[], Player> affector;

    public void Affect(Object[] o, Player p)
    {
        affector(o, p);
    }

    public Effect()
    {
        affector = (o, p) => {};
    }

    public Effect(Action<Object[], Player> a)
    {
        affector = a;
    }
}
