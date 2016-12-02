using System.Collections.Generic;
using System;

using ObjectList = System.Collections.Generic.LinkedList<Object>;
using ObjectListNode = System.Collections.Generic.LinkedListNode<Object>;

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


    protected Object ForEachUntil(Func<ObjectListNode, Object> f)
    {
        ObjectListNode n = objects.First;
        ObjectListNode old;
        Object res;

        while(n != objects.Last)
        {
            old = n;
            n = n.Next;

            res = f(old);
            if(res != null)
            {
                return res;
            }
        }
        return null;
    }
 
    public Object ForEachUntil(Func<Object, Object> f)
    {
        return ForEachUntil(n => f(n.Value));
    }

    public Object ForEachUntil(Func<Object, bool> f)
    {
        return ForEachUntil(o => {
                if(f(o))
                {
                    return o;
                }
                return null;
            });
    }

    public void ForEach(Action<Object> f)
    {
        ForEachUntil(o => {f(o); return null;});
    }

    public void Add(Object o)
    {
        objects.AddFirst(o);
    }

    public Object RemoveFirst(Func<Object, bool> p, Action<Object> f)
    {
        return ForEachUntil(
            n => {
                Object o = n.Value;
                if(p(o))
                {
                    objects.Remove(n);
                    f(o);
                    return o;
                }
                return null;
            });
    }

    public void RemoveAll(Func<Object, bool> p, Action<Object> f)
    {
        ForEachUntil(
            n => {
                Object o = n.Value;
                if(p(o))
                {
                    objects.Remove(n);
                    f(o);
                }
                return null;
            });
    }
}
