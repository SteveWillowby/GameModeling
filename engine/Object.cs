using System.Collections.Generic;
using System;

using ObjectList = System.Collections.Generic.LinkedList<Object>;
using Objects = System.Collections.Generic.Dictionary<string, 
        System.Collections.Generic.LinkedList<Object>>;

public class Object //Class makes sure it's a reference type
{
    public string type;
    public Object _in; //"in" is taken
    public Dictionary<string, LinkedList<Object>> _contains;

    public Object(string t)
    {
        type = t;
        _in = null;
        _contains = new Dictionary<string, LinkedList<Object>>();
    }

    public static bool In(Object a, Object b)
    {
        return Object.ReferenceEquals(a._in, b);
    }

    public static bool RIn(Object a, Object b)
    {
        return In(a, b) || RInHelper(a._in, a, b, true);
    }

    protected static bool RInHelper(Object hare, Object tortise, 
                                    Object goal, bool tStep)
    {
        if(!(hare != null) || Object.ReferenceEquals(tortise, hare))
        {
            return false;
        }
        return In(hare, goal) || 
            RInHelper(hare._in, (tStep ? tortise._in : tortise), goal, !tStep);
    }

    public static bool In(string t, Object o)
    {
        ObjectList l;
        if(!o._contains.TryGetValue(t, out l))
        {
            return false;
        }
        return l.Count > 0;
    }

    public static bool RIn(string t, Object o)
    {
        return RIn(t, o) || RInHelper(t, o, null);
    }

    protected static bool RInHelper(string t, Object curr, Object start)
    {
        //Note that the check to see if curr is null should only be necessary if
        //the object tree is badly constructed (null entry in list).
        if(!(curr != null) || Object.ReferenceEquals(curr, start))
        {
            return false;
        }
        if(!(start != null))
        {
            start = curr;
        }
        if(RIn(t, curr))
        {
            return true;
        }

        Objects.Enumerator e = curr._contains.GetEnumerator();
        ObjectList.Enumerator e2;
        while(e.MoveNext())
        {
            e2 = e.Current.Value.GetEnumerator();
            while(e2.MoveNext())
            {
                if(RInHelper(t, e2.Current, start))
                {
                    return true;
                }
            }
        }
        return false;;
    }
}
