using System;

using ObjectList = System.Collections.Generic.LinkedList<Object>;
using Objects = System.Collections.Generic.Dictionary<string, 
        System.Collections.Generic.LinkedList<Object>>;

public class Action
{
    protected int numObjects;
    protected Requirement[] requirements;
    protected int[][] rApplyTo;
    protected Effect[] effects;

    //It might be much more efficient (in terms of runtime) to
    //actually create an array of evaluate functions
    protected bool RequirementsMet(Object[] o)
    {
        if(requirements.Length == 0)
        {
            return true;
        }
        int currLen = rApplyTo[0].Length;
        Object[] pass = new Object[currLen];
        for(int i = 0; i < requirements.Length; i++)
        {
            currLen = rApplyTo[i].Length;
            if(pass.Length != currLen)
            {
                pass = new Object[currLen];
            }
            for(int j = 0; j < currLen; j++)
            {
                pass[j] = o[rApplyTo[i][j]];
            }
            if(!requirements[i].Met(pass))
            {
                return false;
            }
        }
        return true;
    }

    public Action() { numObjects = 0; }
}
