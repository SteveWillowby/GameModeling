using System.Collections.Generic;
using System;

/* An Object is the basic unit of this model and engine.
 * Almost everything revolves around objects.
 */

public class Object //Class makes sure it's a reference type
{
    //These could probably be made protected rather than public in the future.
    //It might take a bit of work.
    public string type;
    public Object _in; //"in" is reserved, hence the name "_in"
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
    //              (aka querying functions)              //
    //                                                    //
    ////////////////////////////////////////////////////////

    //Allows you to use == to see if the objects are the same
    public override bool Equals(System.Object o)
    {
        return o != null && Object.ReferenceEquals(this, o);
    }

    //Needs to be here in conjunction with overriding Equals()
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static Func<Object, bool> HasType(string t)
    {
        if(t == "Object") //Refers to any type
        {
            return o => true;
        }
        return o => o.type == t;
    }

    //These are simply wrappers
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
        return Contains(HasType(t)) != null;
    }

    public bool RContains(Object o)
    {
        return RContains(o.Equals) != null;
    }

    public bool RContains(string t)
    {
        return RContains(HasType(t)) != null;
    }

    public Object In(Func<Object, bool> p)
    {
        if(_in != null && p(_in))
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
        return In(HasType(t)) != null;
    }

    public Object RIn(Func<Object, bool> p)
    {
        if(_in == null)
        {
            return null;
        }
        return _in.RInHelper(p, this, false);
    }

    /* This function might seem strange at first glance.
     * It uses the tortoise-hare method to avoid getting stuck in
     * an infinite loop ("Floyd's cycle-finding algorithm").
     * 
     * If any object up the _in-chain satisfies p, that object is returned.
     * Otherwise, the function returns null.
     */
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
        return RIn(HasType(t)) != null;
    }

    ////////////////////////////////////////////////////////
    //                                                    //
    //                    Begin Actions                   //
    //                                                    //
    ////////////////////////////////////////////////////////

    //Used when removing an object to "tell" that object
    //it's no longer inside another object
    protected void NullifyIn(Object o)
    {
        o._in = null;
    }

    //A bunch of wrapper functions
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

    //Adds an object to contains
    public void TakeIn(Object o)
    {
        if(o._in != null)
        {
            //Before adding an object, remove it from one it's
            //already in
            o._in.ThrowOut(o.Equals);
        }
        _contains.Add(o);
        o._in = this;
    }
}
