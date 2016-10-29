using System;

using ObjectList = System.Collections.Generic.LinkedList<Object>;
using Objects = System.Collections.Generic.Dictionary<string, 
        System.Collections.Generic.LinkedList<Object>>;

public class Player
{
    protected Objects owns;

    public Player()
    {
        owns = new Objects();
    }
}
