/* Parses a requirement line */
using System;

public class RequirementParser
{
    public enum Type
    {
        In
        , RIn
        , Contains
        , RContains
        , Owns
        , Timer
    };

    protected static char[] dot = {'.'};

    public Requirement Parse(Line header, Line line)
    {
        bool negate = line.tokens[1].val == "not";
        int offset = negate ? 1 : 0;

        Func<Object[], bool> evaluator = o => true;
        int[] indices;

        string t = line.tokens[1 + offset].val;
        Type type = ParseType(t);

        indices = new int[0];
        if(type == Type.Owns)
        {
        }
        else if(type == Type.Timer)
        {
        }
        else if(type == Type.In || type == Type.RIn || type == Type.Contains ||
                type == Type.RContains)
        {
            string[] thing1 = line.tokens[2 + offset].val.Split(dot);
            string[] thing2 = line.tokens[3 + offset].val.Split(dot);
            int index1 = ObjectIndex(header, thing1[0]);
            if(index1 == -1)
            {
                throw new Exception("In/Contains require an object");
            }
            int index2 = ObjectIndex(header, thing2[0]);

            if(type == Type.In)
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

    protected static Type ParseType(string t)
    {
        switch(t)
        {
            case "in":
                return Type.In;
            case "rin":
                return Type.RIn;
            case "contains":
                return Type.Contains;
            default:
                throw new Exception("Invalid requirement type " + t);
        }
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

    protected static bool CheckOnAll<T>(Func<T, bool> p, T[] o)
    {
        for(int i = 0; i < o.Length; i++)
        {
            if(!p(o[i]))
            {
                return false;
            }
        }
        return true;
    }

    protected static Func<Object, bool> CheckOnAll(Type requirement, Object[] o)
    {
        if(requirement == Type.Contains)
        {
            return o2 => CheckOnAll<Object>(o2.Contains, o);
        }
        if(requirement == Type.RContains)
        {
            return o2 => CheckOnAll<Object>(o2.RContains, o);
        }
        throw new Exception("Invalid requirement passed to CheckOnAll");
    }

    protected static Func<Object, bool> CheckOnAll(Type requirement, string[] t)
    {
        if(requirement == Type.Contains)
        {
            return o2 => CheckOnAll<string>(o2.Contains, t);
        }
        if(requirement == Type.RContains)
        {
            return o2 => CheckOnAll<string>(o2.RContains, t);
        }
        throw new Exception("Invalid requirement passed to CheckOnAll");
    }

    protected static Object WalkInChain(Object o, string[] fullPath)
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
