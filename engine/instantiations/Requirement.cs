using System;

/* This class is fairly simple. It's a subcomponent of
 * an Action. It simply stores a function that evaluates
 * whether a requirement is met.
 *
 * Most of the heavy-lifting concerning requirements is
 * coded in the RequirementParser, which interprets model 
 * files and translates requirement lines into actual 
 * functions.
 */

public class Requirement
{
    protected Func<Object[], Player, bool> evaluator;

    /* This function not strictly necessary, but it allows
     * evaluator() to be a protected variable set only via
     * the constructor.
     */    
    public bool Met(Object[] o, Player p)
    {
        return evaluator(o, p);
    }

    public Requirement() {}

    public Requirement(Func<Object[], Player, bool> e)
    {
        evaluator = e;
    }
}
