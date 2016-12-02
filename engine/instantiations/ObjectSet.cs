using System.Collections.Generic;
using System;

using ObjectList = System.Collections.Generic.LinkedList<Object>;
using Objects = System.Collections.Generic.Dictionary<string, 
        System.Collections.Generic.LinkedList<Object>>;

public class ObjectSet
{
    ObjectList objects;

    public ObjectSet()
    {
        objects = new LinkedList<Object>();
    }

    ////////////////////////////////////////////////////////
    //                                                    //
    //                   Begin Checkers                   //
    //                                                    //
    ////////////////////////////////////////////////////////

    public Object Contains(Func<Object, bool> p)
    {
        return ForEachUntil(p);
    }

    public Object RContains(Func<Object, bool> p)
    {
        return RContainsHelper(p, this);
    }

    //Currently a DFS
    public Object RContainsHelper(Func<Object, bool> p, ObjectSet start)
    {
        if(this == start)
        {
            return null;
        }
        if(!(start != null))
        {
            start = this;
        }
        return ForEachUntil(
            o => {
                if(p(o))
                {
                    return o;
                }
                return o._contains.RContainsHelper(p, start);
            });
    }

    ////////////////////////////////////////////////////////
    //                                                    //
    //                    Begin Actions                   //
    //                                                    //
    ////////////////////////////////////////////////////////

    public Object ThrowOut(Object o)
    {
        objects.Remove(o);
        return o;
    }

    //Remove this method and just make people use contains separately?
    public Object ThrowOut(Func<Object, bool> p)
    {
        Object toChuck = Contains(p);
        if(!(toChuck != null))
        {
            return null;
        }
        return ThrowOut(toChuck);
    }

    public void TakeIn(Object o)
    {
        objects.AddFirst(o);
    }

    public Object ForEachUntil(Func<Object, Object> f)
    {
        ObjectList.Enumerator e = objects.GetEnumerator();
        Object res;
        while(e.MoveNext())
        {
            res = f(e.Current);
            if(res != null)
            {
                return res;
            }
        }
        return null;
    }

    public Object ForEachUntil(Func<Object, bool> f)
    {
        ObjectList.Enumerator e = objects.GetEnumerator();
        while(e.MoveNext())
        {
            if(f(e.Current))
            {
                return e.Current;
            }
        }
        return null;
    }

    public void ForEach(Action<Object> f)
    {
        ForEachUntil(o => {f(o); return true;});
    }
}
