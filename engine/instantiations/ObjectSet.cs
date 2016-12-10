using System.Collections.Generic;
using System;

//If you're more familiar with C/C++,
//these two lines are essentially "typdefs" that are local to this file
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


    /* Calls f() on every object in the set until f() returns a
     * non-null Object.
     * Every subsequent "foreach" method in some way ultimately 
     * calls this one.
     */
    protected Object ForEachUntil(Func<ObjectListNode, Object> f)
    {
        ObjectListNode n = objects.First;
        ObjectListNode old;
        Object res;
        while(n != null)
        {
            //Update n before applying f() in case f() removes
            //the node from the list.
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

    /////////////////// Switching to Recursive ForEach ///////////////////

    protected Object RForEachUntil(Func<ObjectListNode, Object> f)
    {
        return RForEachUntilHelper(f, null);
    }

    /* Recursively explores the objects contained within the set.
     * That is, it explores the objects contained in the objects,
     * along with the objects contained in the objects contained,
     * and so on.
     * 
     * The function f() is applied to these objects, and the process
     * is cut short if f() returns a non-null Object.
     *
     * Currently implemented as a DFS
     * Considering the contains structure as a tree, this operates on 
     * leaf nodes first (i.e. Objects containing nothing).
     */
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

    ////////////////// More "Concrete" Operations //////////////////

    public void Add(Object o)
    {
        objects.AddFirst(o);
    }

    /* Removes the first N objects satisfying p(). (Or less if fewer than
     * N actually satisfy p())
     * 
     * Once an object is removed, the function f() is applied to it.
     * 
     * Returns the last object removed (null if none are removed)
     */
    public Object RemoveN(Func<Object, bool> p, Action<Object> f, int N)
    {
        if(N <= 0)
        {
            return null;
        }
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

    //Returns an Object set of all the objects satisfying p()
    public ObjectSet Subset(Func<Object, bool> p)
    {
        ObjectSet s = new ObjectSet();
        ForEach(o => { if(p(o)) { s.Add(o); } });
        return s;
    }

    //Returns the number of objects contained that satisfy p()
    public int Count(Func<Object, bool> p)
    {
        int count = 0;
        ForEach(o => { if(p(o)) { count++; } });
        return count;
    }
}
