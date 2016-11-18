using System;

public class Requirement
{
    protected Func<Object[], bool> evaluator;

    //protected int[] indices;
    //protected Object[] pass; //used in conjunction with indices

    public bool Met(Object[] o)
    {
        /*
        for(int i = 0; i < indices.Length; i++)
        {
            pass[i] = o[indices[i]];
        }
        */
        return evaluator(o /* pass */);
    }

    public Requirement() {}

    public Requirement(Func<Object[], bool> e /*, int[] i*/)
    {
        SetEvaluator(e);
        //SetIndices(i);
    }

    /*
    protected void SetIndices(int[] i)
    {
        indices = new int[i.Length];
        pass = new Object[i.Length];
        for(int j = 0; j < i.Length; j++)
        {
            indices[j] = i[j];
        }
    }
    */

    protected void SetEvaluator(Func<Object[], bool> e)
    {
        evaluator = e;
    }
}
