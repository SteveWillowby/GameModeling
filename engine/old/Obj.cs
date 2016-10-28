using System.Collections.Generic;
using System;

public class Obj : IObj
{
    protected string type;
    protected Obj _in; //"in" is taken
    protected Dictionary<string, LinkedList<Obj>> _contains;

    //Methods
    string IObj.GetType()
    {
        return type;
    }

    bool IObj.In(IObj o)
    {
        return _in != null && Object.ReferenceEquals(_in, o);
    }

    bool IObj.InType(string t)
    {
        return _in != null && _in.type == t;
    }

    //Note that a.contains(b) is much slower than b.in(a)
    bool IObj.Contains(IObj o)
    {
        return NodeContains(o) != null;
    }

    bool IObj.RIn(IObj o)
    {
        //wat
    }

    bool RInHelper(Obj goal, IObj init)
    {
        if(Object.ReferenceEquals(this, goal))
        {
            return true;
        }
        if(!(goal != null))
        {
            return false;
        }
        if(Object.ReferenceEquals(this, init))
        {
            return false;
        }
        return this._in.RInHelper(goal, init);
    }

    bool IObj.ContainsType(string t)
    {
        LinkedList<Obj> l = ListContains(t);
        return l != null && l.Count > 0;
    }

    protected LinkedList<Obj> ListContains(string t)
    {
        LinkedList<Obj> l;
        if(!_contains.TryGetValue(t, out l))
        {
            l = null;
        }
        return l;
    }

    protected LinkedListNode<Obj> NodeContains(IObj o)
    {
        LinkedList<Obj> l = ListContains(o.GetType());
        if(!(l != null) || l.Count == 0)
        {
            return null;
        }
        LinkedListNode<Obj> n = l.First;
        while(n != null)
        {
            if(Object.ReferenceEquals(n.Value, o))
            {
                return n;
            }
            n = n.Next;
        }
        return null;
    }

    //PutIn and TakeIn are mutually recursive -- very un-optimized
    //!!! If the other IObj is not of type Obj, this might not work !!!
    void IObj.PutIn(IObj o)
    {
        if(!((IObj) this).In(o))
        {
            if(_in != null) //First remove this from what it's in
            {
                ((IObj) this).RemoveFrom(_in);
            }

            _in = (Obj) o;
            o.TakeIn(this);
        }
    }

    void IObj.TakeIn(IObj o)
    {
        if(!((IObj) this).Contains(o))
        {
            LinkedList<Obj> l = ListContains(o.GetType());
            if(!(l != null))
            {
                l = new LinkedList<Obj>();
                _contains.Add(o.GetType(), l);
            }
            l.AddFirst((Obj) o);
            o.PutIn(this);
        }
    }

    //Mutually recursive with ThrowOut
    void IObj.RemoveFrom(IObj o)
    {
        if(((IObj) this).In(o))
        {
            _in = null;
            o.ThrowOut(this);
        }
    }

    void IObj.ThrowOut(IObj o)
    {
        LinkedListNode<Obj> n = NodeContains(o);
        if(n != null)
        {
            n.List.Remove(n);
            o.RemoveFrom(this);
        }
    }

    //Constructors --- need to update contains and in props of other objects
    public Obj(string t)
    {
        type = t;
        _in = null;
        _contains = new Dictionary<string, LinkedList<Obj>>();
    }

    public Obj(string t, Obj i)
    {
        type = t;
        _in = i;
        _contains = new Dictionary<string, LinkedList<Obj>>();
    }

    public Obj(string t, string th, LinkedList<Obj> h)
    {
        type = t;
        _in = null;
        _contains = new Dictionary<string, LinkedList<Obj>>();
        _contains.Add(th, h);
    }

    public Obj(string t, Obj i, string th, LinkedList<Obj> h)
    {
        type = t;
        _in = i;
        _contains = new Dictionary<string, LinkedList<Obj>>();
        _contains.Add(th, h);
    }

    public Obj(string t, Dictionary<string, LinkedList<Obj>> h)
    {
        type = t;
        _in = null;
        _contains = h;
    }

    public Obj(string t, Obj i, Dictionary<string, LinkedList<Obj>> h)
    {
        type = t;
        _in = i;
        _contains = h;
    }
}
