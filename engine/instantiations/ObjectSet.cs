using System.Collections.Generic;
using System;

using ObjectList = System.Collections.Generic.LinkedList<Object>;
using Objects = System.Collections.Generic.Dictionary<string, 
        System.Collections.Generic.LinkedList<Object>>;

public class ObjectSet
{
    Objects objects;

    public ObjectSet()
    {
        objects = new Dictionary<string, LinkedList<Object>>();
    }

    ////////////////////////////////////////////////////////
    //                                                    //
    //                   Begin Checkers                   //
    //                                                    //
    ////////////////////////////////////////////////////////

    public Object Contains(Func<Object, bool> p)
    {
        Objects.Enumerator e = objects.GetEnumerator();
        Object ans;
        while(e.MoveNext())
        {
            ans = ListContains(e.Current.Value, p);
            if(ans != null)
            {
                return ans;
            }
        }
        return null;
    }

    public Object Contains(Func<Object, bool> p, string t)
    {
        ObjectList l;
        if(!objects.TryGetValue(t, out l))
        {
            return null;
        }
        return ListContains(l, p);
    }

    protected static Object ListContains(ObjectList l, Func<Object, bool> p)
    {
        ObjectList.Enumerator e = l.GetEnumerator();
        while(e.MoveNext())
        {
            if(p(e.Current))
            {
                return e.Current;
            }
        }
        return null;
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

        Objects.Enumerator e = objects.GetEnumerator();
        ObjectList.Enumerator e2;
        Object ans;
        while(e.MoveNext())
        {
            e2 = e.Current.Value.GetEnumerator();
            while(e2.MoveNext())
            {
                if(p(e2.Current))
                {
                    return e2.Current;
                }

                ans = e2.Current._contains.RContainsHelper(p, start);
                if(ans != null)
                {
                    return ans;
                }
            }
        }
        return null;
    }

    public Object[] ContainsAsArray()
    {
        int totalSize = 0;
        Objects.Enumerator e = objects.GetEnumerator();
        while(e.MoveNext())
        {
            totalSize += e.Current.Value.Count;
        }

        Object[] ret = new Object[totalSize];
        int idx = 0;
        e = objects.GetEnumerator();
        while(e.MoveNext())
        {
            ObjectList.Enumerator e2 = e.Current.Value.GetEnumerator();
            while(e2.MoveNext())
            {
                ret[idx] = e2.Current;
                idx++;
            }
        }
        return ret;
    }

    ////////////////////////////////////////////////////////
    //                                                    //
    //                    Begin Actions                   //
    //                                                    //
    ////////////////////////////////////////////////////////

    public Object ThrowOut(Object o)
    {
        ObjectList l = objects[o.type];
        l.Remove(o);
        o._in = null;
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
        ObjectList l;
        if(!objects.TryGetValue(o.type, out l))
        {
            l = new ObjectList();
            objects.Add(o.type, l);
        }
        l.AddFirst(o);
    }
}
