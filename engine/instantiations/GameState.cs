using System.Collections.Generic;
using System;

/* GameState stores one game only (i.e. everything is static).
 *
 * Currently, all it does is allow for objects to be added or
 * removed from the game.
 *
 * "GameState" might be a misleading name. Perhaps "ObjectState" would
 * be better. Currently players are handled in "PlayerState" and 
 * the Timer's value is maintained in a class of it's own.
 */

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
        all.RemoveFirst(o.Equals, o2 => {});
    }
}
