using System;

/* This class provides the Parse() method that takes
 * a header and a regular line, creating a Requirement.
 *
 * This class could really stand to have more
 * checks for malformed input and throw errors when such 
 * input occurs.
 */

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

    /* Used for the Split function to, for example, convert
     * "a.in.in.contains" into ["a", "in", "in", "contains"]
     */
    protected static char[] dot = {'.'};

    /* This function has a lot of "Magic Constants." To understand them
     * you really should know the model as defined in the documentation.
     */
    public static Requirement Parse(Line header, Line line)
    {
        /* Since any requirement can be negated, all indices we reference
         * might be offset by 1. Also, the final return value of evaluator()
         * should perhaps be negated.
         */
        bool negate = line.tokens[1] == "not";
        int offset = negate ? 1 : 0;

        Func<Object[], Player, bool> evaluator = (o, p) => true;

        string t = line.tokens[1 + offset];
        Type type = ParseType(t);

        if(type == Type.Owns)
        {
            string[] arg1 = line.tokens[2 + offset].Split(dot);
            int argIdx1 = ActionParser.ObjectIndex(header, arg1[0]);
            if(argIdx1 != -1) //require owns obj
            {
                evaluator = (o, p) => {
                    Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                    return p.Owns(o1);
                };
            }
            else if(line.tokens.Length == 3 + offset) //require owns type
            {
                evaluator = (o, p) => {
                    return p.Owns(line.tokens[2 + offset]);
                };
            }
            else if(line.tokens[3 + offset] == "<") //require owns type < num
            {
                evaluator = (o, p) => {
                    int numObjects = Int32.Parse(line.tokens[4 + offset]);
                    return p.OwnsLess(line.tokens[2 + offset], numObjects);
                };
            }
            else if(line.tokens[3 + offset] == ">") //require owns type > num
            {
                evaluator = (o, p) => {
                    int numObjects = Int32.Parse(line.tokens[4 + offset]);
                    return p.OwnsMore(line.tokens[2 + offset], numObjects);
                };
            }
        }
        else if(type == Type.Timer) //require timer
        {
            evaluator = (o, p) => Timer.TimeOnTheClock();
        }
        else if(type == Type.In || type == Type.RIn ||
                type == Type.Contains || type == Type.RContains)
        {
            string[] arg1 = line.tokens[2 + offset].Split(dot);
            string[] arg2 = line.tokens[3 + offset].Split(dot);
            int argIdx1 = ActionParser.ObjectIndex(header, arg1[0]);
            if(argIdx1 == -1)
            {
                throw new Exception("In/Contains require an object");
            }
            int argIdx2 = ActionParser.ObjectIndex(header, arg2[0]);

            if(type == Type.In)
            {
                //require in obj1.blah.type obj2
                if(argIdx2 != -1 && arg1[arg1.Length - 1] == "type")
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        return o1.In(o2.type);
                    };
                }
                else if(argIdx2 != -1) //require in obj1 obj2
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        return o1.In(o2);
                    };
                }
                else //require in obj type
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        return o1.In(arg2[0]);
                    };
                }
            }
            else if(type == Type.RIn)
            {
                //require rin obj1.blah.type obj2
                if(argIdx2 != -1 && arg1[arg1.Length - 1] == "type")
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        return o1.RIn(o2.type);
                    };
                }
                else if(argIdx2 != -1) //require rin obj1 obj2
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        return o1.RIn(o2);
                    };
                }
                else //require rin obj type
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        return o1.RIn(arg2[0]);
                    };
                }
            }
            else if(type == Type.Contains)
            {
                if(argIdx2 != -1 && arg1[arg1.Length - 1] == "type" &&
                    arg1.Length > 1 && arg1[arg1.Length - 2] == "contains")
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        return CheckForAll(o3 => o1.Contains(o3.type),
                                            o2._contains);
                    };
                }
                else if(argIdx2 != -1 && arg1[arg1.Length - 1] == "type")
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        return o1.Contains(o2.type);
                    };
                }
                else if(argIdx2 != -1 && arg1[arg1.Length - 1] == "contains")
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        return CheckForAll(o3 => o1.Contains(o3),
                                            o2._contains);
                    };
                }
                else if(argIdx2 != -1)
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        return o1.Contains(o2);
                    };
                }
                else
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        return o1.Contains(arg2[0]);
                    };
                }
            }
            else if(type == Type.RContains)
            {
                if(argIdx2 != -1 && arg1[arg1.Length - 1] == "type" &&
                    arg1.Length > 1 && arg1[arg1.Length - 2] == "contains")
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        return CheckForAll(o3 => o1.RContains(o3.type),
                                            o2._contains);
                    };
                }
                else if(argIdx2 != -1 && arg1[arg1.Length - 1] == "type")
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        return o1.RContains(o2.type);
                    };
                }
                else if(argIdx2 != -1 && arg1[arg1.Length - 1] == "contains")
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        return CheckForAll(o3 => o1.Contains(o3),
                                            o2._contains);
                    };
                }
                else if(argIdx2 != -1)
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        return o1.RContains(o2);
                    };
                }
                else
                {
                    evaluator = (o, p) => {
                        Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                        return o1.RContains(arg2[0]);
                    };
                }
            }
        }

        return new Requirement((o, p) => negate ^ evaluator(o, p));
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

    protected static bool CheckForAll(Func<Object, bool> p, ObjectSet o)
    {
        return o.ForEachUntil(o1 => !p(o1)) == null;
    }
}
