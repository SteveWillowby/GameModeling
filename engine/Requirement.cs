using System;

public class Requirement
{
    protected int[] indices;
    protected Func<Object[], bool> evaluator;

    public bool Met(Object[] o)
    {
        return evaluator(o);
    }

    public Requirement(Func<Object[], bool> e, int[] i)
    {
        SetEvaluator(e);
        SetIndices(i);
    }

    protected void SetIndices(int[] i)
    {
        indices = new int[i.Length];
        for(int j = 0; j < i.Length; j++)
        {
            indices[j] = i[j];
        }
    }

    protected void SetEvaluator(Func<Object[], bool> e)
    {
        evaluator = e;
    }
}
