using System.Collections.Generic;

public class Object
{
    private string type;
    private Object _in; //"in" is taken
    private Dictionary<string, LinkedList<Object>> _has;

    //Constructors
    public Object(string t)
    {
        type = t;
        _in = null;
        _has = new Dictionary<string, LinkedList<Object>>();
    }

    public Object(string t, Object i)
    {
        type = t;
        _in = i;
        _has = new Dictionary<string, LinkedList<Object>>();
    }

    public Object(string t, string th, LinkedList<Object> h)
    {
        type = t;
        _in = null;
        _has = new Dictionary<string, LinkedList<Object>>();
        _has.Add(th, h);
    }

    public Object(string t, Object i, string th, LinkedList<Object> h)
    {
        type = t;
        _in = i;
        _has = new Dictionary<string, LinkedList<Object>>();
        _has.Add(th, h);
    }

    public Object(string t, Dictionary<string, LinkedList<Object>> h)
    {
        type = t;
        _in = null;
        _has = h;
    }

    public Object(string t, Object i, Dictionary<string, LinkedList<Object>> h)
    {
        type = t;
        _in = i;
        _has = h;
    }
}
