using System.Collections.Generic;
using System;

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
        return In(a, b) || RInHelper(a._in, b, a);
    }

    protected static bool RInHelper(Object a, Object b, Object start)
    {
        if(!(a != null) || Object.ReferenceEquals(a, start))
        {
            return false;
        }
        return In(a, b) || RInHelper(a._in, b, start);
    }
}
