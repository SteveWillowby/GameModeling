using System;

/* This class needs more work.
 * It primarily serves as a container for requirements and effects.
 */

public class Action
{
    public int numObjects;
    public Requirement[] requirements;
    public Effect[] effects;
    public string name;

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

    public Action()
    {
        numObjects = 0;
        requirements = new Requirement[0];
        effects = new Effect[0];
        name = "Null Action";
    }
}
