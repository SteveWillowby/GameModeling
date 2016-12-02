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

        string t = line.tokens[1];
        Type type = ParseType(t);

        if(type == Type.Put)
        {
            string[] arg1 = line.tokens[2].Split(dot);
            string[] arg2 = line.tokens[3].Split(dot);
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
                affector = o => {
                    Object o2 = WalkInChain(o[argIdx2], arg2);
                    Object fresh = GameState.AddObject(arg1[0]);
                    o2.TakeIn(fresh);
                };
            }
            else if(line.tokens.Length == 5)
            {
                affector = o => {
                    Object o2 = WalkInChain(o[argIdx2], arg2);
                    Object fresh;
                    int numObjects = Int32.Parse(line.tokens[4]);
                    for(int i = 0; i < numObjects; i++)
                    {
                        fresh = GameState.AddObject(arg1[0]);
                        o2.TakeIn(fresh);
                    }
                };
            }
        }
        else if(type == Type.Remove)
        {
            string[] arg1 = line.tokens[2].Split(dot);
            string[] arg2 = line.tokens[3].Split(dot);
            int argIdx1 = ObjectIndex(header, arg1[0]);
            int argIdx2 = ObjectIndex(header, arg2[0]);
            if(argIdx2 == -1 && argIdx2 == -1)
            {
                throw new Exception("Remove requires an object");
            }

            if(argIdx1 != -1)
            {
                affector = o => {
                    Object o1 = WalkInChain(o[argIdx1], arg1);
                    o1._in.ThrowOut(o1);
                };
            }
            else if(line.tokens.Length == 5 && line.tokens[4] == "all")
            {
                affector = o => {
                    Object o2 = WalkInChain(o[argIdx2], arg2);
                    while(o2.Contains(line.tokens[2]))
                    {
                        o2.ThrowOut(o3 => o3.type == line.tokens[2]);
                    }
                };
            }
            else if(line.tokens.Length == 5)
            {
                affector = o => {
                    Object o2 = WalkInChain(o[argIdx2], arg2);
                    int numObjects = Int32.Parse(line.tokens[4]);
                    for(int i = 0; i < numObjects; i++)
                    {
                        o2.ThrowOut(o3 => o3.type == line.tokens[2]);
                    }
                };
            }
        }
        else if(type == Type.Create)
        {
            affector = o => {
                GameState.AddObject(line.tokens[2]);
            };
        }
        else if(type == Type.Delete)
        {
            string[] arg1 = line.tokens[2].Split(dot);
            int argIdx1 = ObjectIndex(header, arg1[0]);
            if(argIdx1 == -1)
            {
                throw new Exception("Remove requires an object");
            }

            if(arg1[arg1.Length - 1] == "contains")
            {
                affector = o => {
                    Object o1 = WalkInChain(o[argIdx1], arg1);
                    o1._contains.ForEach(o2 => {
                        if(o2 != o1)
                        {
                            while(o2.Contains(o3 => true) != null)
                            {
                                o2.ThrowOut(o3 => true);
                            }
                        }
                        o1.ThrowOut(o2);
                        GameState.RemoveObject(o2);
                    });
                };
            }
            else
            {
                affector = o => {
                    Object o1 = WalkInChain(o[argIdx1], arg1);
                    while(o1.Contains(o2 => true) != null)
                    {
                        o1.ThrowOut(o2 => true);
                    }
                    GameState.RemoveObject(o1);
                };
            }
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
            affector = o => {
                int seconds = Int32.Parse(line.tokens[2]);
                Timer.SetTimer(seconds);
            };
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
            if(header.tokens[i] == o)
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
