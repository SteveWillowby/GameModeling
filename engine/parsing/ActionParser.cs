using System;

/* Parses a chunk into an Action */

public class ActionParser
{
    public static Action Parse(Chunk c)
    {
        Action a = new Action();

        /* Parse each line of the chunk:
         *
         * if requirement, add the requirement to a
         * if effect, add the effect to a
         */

        return a;
    }

    /* Used in Requirement and Effect Parsers
     *
     * Given a header, states whether an object with name o
     * exists in the header. If so, the index is returned.
     * Otherwise, -1 is returned.
     */
    public static int ObjectIndex(Line header, string o)
    {
        for(int i = 3; i < header.tokens.Length; i += 2)
        {
            if(header.tokens[i] == o)
            {
                return (i - 3) / 2;
            }
        }
        return -1;
    }

    /* Used in Requirement and Effect Parsers
     *
     * fullPath is an array representing a string of the form
     * a.in.in.somethingHere
     *
     * Basically, for every "in" in the array, it steps up one layer of
     * "in's" with the object given to it.
     *
     * This is used as a helper function for the functions created by the
     * Requirement and Effect parsers.
     */
    public static Object WalkInChain(Object o, string[] fullPath)
    {
        //fullPath[0] is the object's name
        int i;
        for(i = 1; i < fullPath.Length && fullPath[i] == "in"; i++)
        {
            o = o._in; //could be null!
        }
        return o;
    }
}
