using System.Collections.Generic;

public class Object
{
    private string type;
    private Object _in; //"in" is taken
    private Dictionary<string, LinkedList<Object>> _contains;

    //Methods
    public bool In(Object o)
    {
        return _in != null && Object.ReferenceEquals(_in, o);
    }

    public bool InType(string t)
    {
        return _in != null && _in.type == t;
    }

    //Note that a.contains(b) is much slower than b.in(a)
    public bool Contains(Object o)
    {
        LinkedList<Object> l;
        if(!_contains.TryGetValue(o.type, out l))
        {
            return false;
        }
        return l.Contains(o);
    }

    public bool ContainsType(string t)
    {
        LinkedList<Object> l;
        if(!_contains.TryGetValue(t, out l))
        {
            return false;
        }
        return l.Count > 0;
    }

    //PutIn and TakeIn are mutually recursive -- very un-optimized
    public void PutIn(Object o)
    {
        if(!In(o))
        {
            _in = o;
            o.TakeIn(this);
        }
    }

    public void TakeIn(Object o)
    {
        if(!Contains(o))
        {
            LinkedList<Object> l;
            if(!_contains.TryGetValue(o.type, out l))
            {
                l = new LinkedList<Object>();
            }
            l.AddFirst(o);
            o.PutIn(this);
        }
    }

    //Constructors --- need to update contains and in props of other objects
    public Object(string t)
    {
        type = t;
        _in = null;
        _contains = new Dictionary<string, LinkedList<Object>>();
    }

    public Object(string t, Object i)
    {
        type = t;
        _in = i;
        _contains = new Dictionary<string, LinkedList<Object>>();
    }

    public Object(string t, string th, LinkedList<Object> h)
    {
        type = t;
        _in = null;
        _contains = new Dictionary<string, LinkedList<Object>>();
        _contains.Add(th, h);
    }

    public Object(string t, Object i, string th, LinkedList<Object> h)
    {
        type = t;
        _in = i;
        _contains = new Dictionary<string, LinkedList<Object>>();
        _contains.Add(th, h);
    }

    public Object(string t, Dictionary<string, LinkedList<Object>> h)
    {
        type = t;
        _in = null;
        _contains = h;
    }

    public Object(string t, Object i, Dictionary<string, LinkedList<Object>> h)
    {
        type = t;
        _in = i;
        _contains = h;
    }
}
