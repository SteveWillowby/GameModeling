using System.Collections.Generic;
using System;

using ObjectList = System.Collections.Generic.LinkedList<Object>;
using Objects = System.Collections.Generic.Dictionary<string, 
        System.Collections.Generic.LinkedList<Object>>;

public class GameState
{
    protected static Objects all = new Objects();

    public static Object AddObject(string type)
    {
        Object o = new Object(type);
        //This functionality can be found in the Object class (TakeIn())
        ObjectList l;
        if(!all.TryGetValue(o.type, out l))
        {
            l = new ObjectList();
            all.Add(o.type, l);
        }
        l.AddFirst(o);
        return o;
    }

    //Assumes o is in all
    public static void RemoveObject(Object o)
    {
        //This functionality may be found in the ObjectClass (ThrowOut())
        ObjectList l = all[o.type];
        l.Remove(o);
    }
}
