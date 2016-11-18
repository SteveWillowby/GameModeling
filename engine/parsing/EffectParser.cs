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
}
