using System;

/* This class needs more work.
 * It primarily serves as a container for requirements and effects.
 */

public class Action
{
    protected int numObjects;
    protected Requirement[] requirements;
    protected Effect[] effects;

    protected bool RequirementsMet(Object[] o, Player p)
    {
        for(int i = 0; i < requirements.Length; i++)
        {
            if(!requirements[i].Met(o, p))
            {
                return false;
            }
        }
        return true;
    }

    public Action() { numObjects = 0; }
}
