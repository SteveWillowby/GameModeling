using System.Collections.Generic;
using System;

using ObjectList = System.Collections.Generic.LinkedList<Object>;
using Objects = System.Collections.Generic.Dictionary<string, 
        System.Collections.Generic.LinkedList<Object>>;

public class ObjectSet
{
    protected Objects all;

    protected bool ContainsType(Objects o, string t)
    {
        ObjectList l;
        return o.TryGetValue(t, out l);
    }

    public bool Contains(string t)
    {
        return ContainsType(all, t);
    }
}


