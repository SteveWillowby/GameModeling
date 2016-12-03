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

    public int Size()
    {
        return objects.Count;
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
        return RForEachUntil(p);
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
        while(n != null)
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

    protected Object RForEachUntil(Func<ObjectListNode, Object> f)
    {
        return RForEachUntilHelper(f, null);
    }

    //Currently a DFS
    //Considering the structure as a tree, this operates on leaf nodes first
    public Object RForEachUntilHelper(Func<ObjectListNode, Object> f, 
                                         ObjectSet start)
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
            n => {
                Object ret = n.Value._contains.RForEachUntilHelper(f, start);
                if(ret != null)
                {
                    return ret;
                }
                return f(n);
            });
    }

    public Object RForEachUntil(Func<Object, Object> f)
    {
        return RForEachUntil(n => f(n.Value));
    }

    public Object RForEachUntil(Func<Object, bool> f)
    {
        return RForEachUntil(n => {
                if (f(n.Value))
                {
                    return n.Value;
                } 
                return null;
            });
    }

    public void Add(Object o)
    {
        objects.AddFirst(o);
    }

    //Returns the last object removed (null if none are removed)
    public Object RemoveN(Func<Object, bool> p, Action<Object> f, int N)
    {
        int count = 0;
        return ForEachUntil(
            n => {
                Object o = n.Value;
                if(p(o))
                {
                    objects.Remove(n);
                    f(o);
                    count++;
                    if(count == N)
                    {
                        return o;
                    }
                }
                return null;
            });
    }

    public Object RemoveFirst(Func<Object, bool> p, Action<Object> f)
    {
        return RemoveN(p, f, 1);
    }

    public void RemoveAll(Func<Object, bool> p, Action<Object> f)
    {
        RemoveN(p, f, objects.Count);
    }

    public ObjectSet Subset(Func<Object, bool> p)
    {
        ObjectSet s = new ObjectSet();
        ForEach(o => { if(p(o)) { s.Add(o); } });
        return s;
    }

    public int Count(Func<Object, bool> p)
    {
        int count = 0;
        ForEach(o => { if(p(o)) { count++; } });
        return count;
    }
}
