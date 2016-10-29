using System;

public class Requirement
{
    protected int numObjects;
    protected Func<Object[], bool> evaluator;

    public bool Met(Object[] o)
    {
        return o.Length == numObjects && evaluator(o);
    }

    public int GetNumObjects()
    {
        return numObjects;
    }

    public Requirement()
    {
        numObjects = 0;
        evaluator = o => true;
    }

    public Requirement(Func<Object[], bool> e, int n)
    {
        numObjects = n;
        evaluator = e;
    }
}
