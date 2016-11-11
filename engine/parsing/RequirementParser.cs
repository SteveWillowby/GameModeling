/* Parses a requirement line */
using System;

public class RequirementParser
{
    public enum Type 
    {
        In
        , Contains
        , Owns
        , Timer
        , RIn
        , RContains
    };

    protected static char[] dot = {'.'};

    public Requirement Parse(Line header, Line line)
    {
        bool negate = line.tokens[1].val == "not";
        int offset = negate ? 1 : 0;

        Func<Object[], bool> evaluator = o => true;
        int[] indices;

        string type = line.tokens[1 + offset].val;

        if(type == "owns")
        {
            indices = new int[0];
        }
        else if(type == "timer")
        {
            indices = new int[0];
        }
        else // in/contains stuff
        {
            indices = new int[0];

            string[] thing1 = line.tokens[2 + offset].val.Split(dot);
            string[] thing2 = line.tokens[3 + offset].val.Split(dot);
            int index1 = ObjectIndex(header, thing1[0]);
            if(index1 == -1)
            {
                throw new Exception("In/Contains require and object");
            }
            int index2 = ObjectIndex(header, thing2[0]);

            if(type == "in")
            {
                if(index2 != -1)
                {
                    
                }
                else
                {
                    
                }
            }
        }

        return new Requirement(evaluator, indices);
    }

    protected static int ObjectIndex(Line header, string o)
    {
        for(int i = 3; i < header.tokens.Length; i += 2)
        {
            if(header.tokens[i].val == o)
            {
                return (i - 3) / 2;
            }
        }
        return -1;
    }

    protected static Object[] AllObjects(Object o, string[] path)
    {
        return [];
    }
}
