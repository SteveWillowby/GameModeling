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

    ////////////////////////////////////////////////////////
    //                                                    //
    //                   Begin Checkers                   //
    //                                                    //
    ////////////////////////////////////////////////////////

    public override bool Equals(System.Object o)
    {
        return o != null && Object.ReferenceEquals(this, o);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public Object Contains(Func<Object, bool> p)
    {
        Objects.Enumerator e = _contains.GetEnumerator();
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
        if(!_contains.TryGetValue(t, out l))
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
    protected Object RContainsHelper(Func<Object, bool> p, Object start)
    {
        if(this == start)
        {
            return null;
        }
        if(!(start != null))
        {
            start = this;
        }
        if(p(this))
        {
            return this;
        }

        Objects.Enumerator e = _contains.GetEnumerator();
        ObjectList.Enumerator e2;
        Object ans;
        while(e.MoveNext())
        {
            e2 = e.Current.Value.GetEnumerator();
            while(e2.MoveNext())
            {
                ans = e2.Current.RContainsHelper(p, start);
                if(ans != null)
                {
                    return ans;
                }
            }
        }
        return null;
    }

    public bool Contains(Object o)
    {
        return Contains(o.Equals) != null;
    }

    public bool Contains(string t)
    {
        return Contains((o => o.type == t)) != null;
    }

    public bool RContains(Object o)
    {
        return RContains(o.Equals) != null;
    }

    public bool RContains(string t)
    {
        return RContains((o => o.type == t)) != null;
    }

    public Object In(Func<Object, bool> p)
    {
        if(p(_in))
        {
            return _in;
        }
        return null;
    }

    public Object RIn(Func<Object, bool> p)
    {
        if(_in == null)
        {
            return null;
        }
        return _in.RInHelper(p, this, false);
    }

    protected Object RInHelper(Func<Object, bool> p, Object tortoise, bool step)
    {
        if(p(this))
        {
            return this;
        }
        if(this == tortoise)
        {
            return null;
        }
        if(step)
        {
            tortoise = tortoise._in;
        }
        return _in.RInHelper(p, tortoise, !step);
    }

    public bool RIn(Object o)
    {
        return RIn(o.Equals) != null;
    }

    public bool RIn(string t)
    {
        return RIn((o => o.type == t)) != null;
    }

    public Object[] ContainsAsArray()
    {
        int totalSize = 0;
        Objects.Enumerator e = _contains.GetEnumerator();
        while(e.MoveNext())
        {
            totalSize += e.Current.Value.Count;
        }

        Object[] ret = new Object[totalSize];
        int idx = 0;
        e = _contains.GetEnumerator();
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
        ObjectList l = _contains[o.type];
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
        if(o._in != null)
        {
            o._in.ThrowOut(o.Equals);
        }
        ObjectList l;
        if(!_contains.TryGetValue(o.type, out l))
        {
            l = new ObjectList();
            _contains.Add(o.type, l);
        }
        l.AddFirst(o);
        o._in = this;
    }
}
