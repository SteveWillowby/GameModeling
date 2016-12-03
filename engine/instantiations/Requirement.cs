using System;

public class Requirement
{
    protected Func<Object[], Player, bool> evaluator;

    public bool Met(Object[] o, Player p)
    {
        return evaluator(o, p);
    }

    public Requirement() {}

    public Requirement(Func<Object[], Player, bool> e)
    {
        evaluator = e;
    }
}
