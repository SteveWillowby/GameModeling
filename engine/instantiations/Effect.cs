using System;

public class Effect
{
    //protected int numObjects;
    protected Action<Object[]> affector;

    public void Affect(Object[] o)
    {
        /*
        if(o.Length != numObjects)
        {
            return;
        }
        */
        affector(o);
    }

    public Effect()
    {
        //numObjects = 1;
        affector = o => {};
    }

    public Effect(Action<Object[]> a /*, int n*/)
    {
        //numObjects = n;
        affector = a;
    }
}
