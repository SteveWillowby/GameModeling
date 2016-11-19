/* Parses a requirement line */
using System;

public class EffectParser
{
    public enum Type
    {
        Put
        , Remove
        , Create
        , Delete
        , Distribute
        , Disown
        , Players
        , SetTimer
    };

    protected static char[] dot = {'.'};

    public Effect Parse(Line header, Line line)
    {
        Action<Object[]> affector = o => {};

        string t = line.tokens[1].val;
        Type type = ParseType(t);

        if(type == Type.Put)
        {
            string[] arg1 = line.tokens[2].val.Split(dot);
            string[] arg2 = line.tokens[3].val.Split(dot);
            int argIdx1 = ObjectIndex(header, arg1[0]);
            int argIdx2 = ObjectIndex(header, arg2[0]);
            if(argIdx2 == -1)
            {
                throw new Exception("Put requires an object");
            }

            if(argIdx1 != -1)
            {
                affector = o => {
                    Object o1 = WalkInChain(o[argIdx1], arg1);
                    Object o2 = WalkInChain(o[argIdx2], arg2);
                    o2.TakeIn(o1);
                };
            }
            else if(line.tokens.Length == 4)
            {
                //Need a way to create an object registered with the game state
            }
            else if(line.tokens.Length == 5)
            {
                //Need a way to create an object registered with the game state
            }
        }
        else if(type == Type.Remove)
        {
        }
        else if(type == Type.Delete)
        {
        }
        else if(type == Type.Distribute)
        {
        }
        else if(type == Type.Disown)
        {
        }
        else if(type == Type.Players)
        {
        }
        else if(type == Type.SetTimer)
        {
        }

        return new Effect(affector);
    }

    protected static Type ParseType(string t)
    {
        switch(t)
        {
            case "put":
                return Type.Put;
            case "remove":
                return Type.Remove;
            case "create":
                return Type.Create;
            case "delete":
                return Type.Delete;
            case "distribute":
                return Type.Distribute;
            case "disown":
                return Type.Disown;
            case "players":
                return Type.Players;
            case "settimer":
                return Type.SetTimer;
            default:
                throw new Exception("Invalid requirement type " + t);
        }
    }

    //Note that this code is duplicated in RequirementParser.
    //Perhaps this should be moved to Action?
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

    //This method is duplicated in RequirementParser
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
