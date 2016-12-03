using System.Collections.Generic;
using System;

public class Object //Class makes sure it's a reference type
{
    public string type;
    public Object _in; //"in" is taken
    public ObjectSet _contains;

    public Object(string t)
    {
        type = t;
        _in = null;
        _contains = new ObjectSet();
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
        return _contains.Contains(p);
    }

    public Object RContains(Func<Object, bool> p)
    {
        return _contains.RContains(p);
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

    public bool In(Object o)
    {
        return In(o.Equals) != null;
    }

    public bool In(string t)
    {
        return In((o => o.type == t)) != null;
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
        return RIn(o => o.type == t) != null;
    }

    ////////////////////////////////////////////////////////
    //                                                    //
    //                    Begin Actions                   //
    //                                                    //
    ////////////////////////////////////////////////////////

    protected void NullifyIn(Object o)
    {
        o._in = null;
    }

    public Object ThrowOut(Object o)
    {
        return _contains.RemoveFirst(o.Equals, NullifyIn);
    }

    public Object ThrowOut(Func<Object, bool> p)
    {
        return _contains.RemoveFirst(p, NullifyIn);
    }

    public Object ThrowOutN(Object o, int N)
    {
        return _contains.RemoveN(o.Equals, NullifyIn, N);
    }

    public Object ThrowOutN(Func<Object, bool> p, int N)
    {
        return _contains.RemoveN(p, NullifyIn, N);
    }

    public void ThrowOutAll(Func<Object, bool> p)
    {
        _contains.RemoveAll(p, NullifyIn);
    }

    public void TakeIn(Object o)
    {
        if(o._in != null)
        {
            o._in.ThrowOut(o.Equals);
        }
        _contains.Add(o);
        o._in = this;
    }
}
