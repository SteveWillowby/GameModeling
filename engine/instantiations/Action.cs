using System;

using ObjectList = System.Collections.Generic.LinkedList<Object>;
using Objects = System.Collections.Generic.Dictionary<string, 
        System.Collections.Generic.LinkedList<Object>>;

public class Action
{
    protected int numObjects;
    protected Requirement[] requirements;
    protected Effect[] effects;

    protected bool RequirementsMet(Object[] o)
    {
        for(int i = 0; i < requirements.Length; i++)
        {
            if(!requirements[i].Met(o))
            {
                return false;
            }
        }
        return true;
    }

    public Action() { numObjects = 0; }
}
