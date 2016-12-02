using System.Collections.Generic;
using System;

public class GameState
{
    protected static ObjectSet all = new ObjectSet();

    public static Object AddObject(string type)
    {
        Object o = new Object(type);
        all.Add(o);
        return o;
    }

    //Assumes o is in all
    public static void RemoveObject(Object o)
    {
        all.RemoveFirst(o2 => o == o2, o2 => {});
    }
}
