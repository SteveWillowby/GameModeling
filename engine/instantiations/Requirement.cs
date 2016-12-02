using System;

public class Requirement
{
    protected Func<Object[], bool> evaluator;

    public bool Met(Object[] o)
    {
        return evaluator(o);
    }

    public Requirement() {}

    public Requirement(Func<Object[], bool> e)
    {
        SetEvaluator(e);
    }

    protected void SetEvaluator(Func<Object[], bool> e)
    {
        evaluator = e;
    }
}
