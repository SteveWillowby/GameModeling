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
        bool negate = line.tokens[1] == "not";
        int offset = negate ? 1 : 0;

        Func<Object[], bool> evaluator = o => true;
        //int[] indices;

        string t = line.tokens[1 + offset];
        Type type = ParseType(t);

        //indices = new int[0];
        if(type == Type.Owns)
        {
        }
        else if(type == Type.Timer)
        {
            evaluator = o => Timer.TimeOnTheClock();
        }
        else if(type == Type.In || type == Type.RIn ||
                type == Type.Contains || type == Type.RContains)
        {
            string[] arg1 = line.tokens[2 + offset].Split(dot);
            string[] arg2 = line.tokens[3 + offset].Split(dot);
            int argIdx1 = ObjectIndex(header, arg1[0]);
            if(argIdx1 == -1)
            {
                throw new Exception("In/Contains require an object");
            }
            int argIdx2 = ObjectIndex(header, arg2[0]);

            /*
            if(argIdx2 == -1)
            {
                indices = new int[1];
            }
            else
            {
                indices = new int[2];
                indices[1] = argIdx2;
            }
            indices[0] = argIdx1;
            */

            if(type == Type.In)
            {
                if(argIdx2 != -1 && arg1[arg1.Length - 1] == "type")
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        Object o2 = WalkInChain(o[argIdx2], arg2);
                        return o1.In(o2.type);
                    };
                }
                else if(argIdx2 != -1)
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        Object o2 = WalkInChain(o[argIdx2], arg2);
                        return o1.In(o2);
                    };
                }
                else
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        return o1.In(arg2[0]);
                    };
                }
            }
            else if(type == Type.RIn)
            {
                if(argIdx2 != -1 && arg1[arg1.Length - 1] == "type")
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        Object o2 = WalkInChain(o[argIdx2], arg2);
                        return o1.RIn(o2.type);
                    };
                }
                else if(argIdx2 != -1)
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        Object o2 = WalkInChain(o[argIdx2], arg2);
                        return o1.RIn(o2);
                    };
                }
                else
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        return o1.RIn(arg2[0]);
                    };
                }
            }
            else if(type == Type.Contains)
            {
                if(argIdx2 != -1 && arg1[arg1.Length - 1] == "type" &&
                    arg1.Length > 1 && arg1[arg1.Length - 2] == "contains")
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        Object o2 = WalkInChain(o[argIdx2], arg2);
                        return CheckForAll(o3 => o1.Contains(o3.type),
                                            o2._contains);
                    };
                }
                else if(argIdx2 != -1 && arg1[arg1.Length - 1] == "type")
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        Object o2 = WalkInChain(o[argIdx2], arg2);
                        return o1.Contains(o2.type);
                    };
                }
                else if(argIdx2 != -1 && arg1[arg1.Length - 1] == "contains")
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        Object o2 = WalkInChain(o[argIdx2], arg2);
                        return CheckForAll(o3 => o1.Contains(o3),
                                            o2._contains);
                    };
                }
                else if(argIdx2 != -1)
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        Object o2 = WalkInChain(o[argIdx2], arg2);
                        return o1.Contains(o2);
                    };
                }
                else
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        return o1.Contains(arg2[0]);
                    };
                }
            }
            else if(type == Type.RContains)
            {
                if(argIdx2 != -1 && arg1[arg1.Length - 1] == "type" &&
                    arg1.Length > 1 && arg1[arg1.Length - 2] == "contains")
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        Object o2 = WalkInChain(o[argIdx2], arg2);
                        return CheckForAll(o3 => o1.RContains(o3.type),
                                            o2._contains);
                    };
                }
                else if(argIdx2 != -1 && arg1[arg1.Length - 1] == "type")
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        Object o2 = WalkInChain(o[argIdx2], arg2);
                        return o1.RContains(o2.type);
                    };
                }
                else if(argIdx2 != -1 && arg1[arg1.Length - 1] == "contains")
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        Object o2 = WalkInChain(o[argIdx2], arg2);
                        return CheckForAll(o3 => o1.Contains(o3),
                                            o2._contains);
                    };
                }
                else if(argIdx2 != -1)
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        Object o2 = WalkInChain(o[argIdx2], arg2);
                        return o1.RContains(o2);
                    };
                }
                else
                {
                    evaluator = o => {
                        Object o1 = WalkInChain(o[argIdx1], arg1);
                        return o1.RContains(arg2[0]);
                    };
                }
            }
        }

        evaluator = o => negate ^ evaluator(o);

        return new Requirement(evaluator);
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
            case "rcontains":
                return Type.RContains;
            default:
                throw new Exception("Invalid requirement type " + t);
        }
    }

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

    protected static bool CheckForAll(Func<Object, bool> p, ObjectSet o)
    {
        return o.ForEachUntil(o1 => !p(o1)) == null;
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
