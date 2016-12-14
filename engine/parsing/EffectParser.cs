using System;

/* This class provides the Parse() method that takes
 * a header and a regular line, creating an Effect.
 *
 * This class could really stand to have more
 * checks for malformed input and throw errors when such 
 * input occurs.
 *
 * IMPORTANT!! You will note that the lambda expressions use variables
 * external to the lambda. This is treated as any other reference to
 * that variable. Thus if the value of the variable is changed after the
 * lambda expression is created, then when the lambda is called the new
 * value will be used. Since the lambda maintains a reference to the value,
 * the value will not be garbage-collected even if all other references are
 * removed.
 */

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

    /* Used for the Split function to, for example, convert
     * "a.in.in.contains" into ["a", "in", "in", "contains"]
     */
    protected static char[] dot = {'.'};

    /* This function has many "Magic Constants." To understand them
     * you really should know the model as defined in the documentation.
     */
    public static Effect Parse(Line header, Line line)
    {
        Action<Object[], Player> affector = (o, p) => {};

        string t = line.tokens[1];
        Type type = ParseType(t);

        /* Since the object array passed into our affector() will include
         * all the objects the Action deals with, not every one will be
         * used in a given Effect. Thus, for a specific effect, we need to
         * determine the indices of the array it should use.
         *
         * This is done with the "argIdx_ = ActionParser.ObjectIndex..."
         * statements.
         */
        if(type == Type.Put)
        {
            string[] arg1 = line.tokens[2].Split(dot);
            string[] arg2 = line.tokens[3].Split(dot);
            int argIdx1 = ActionParser.ObjectIndex(header, arg1[0]);
            int argIdx2 = ActionParser.ObjectIndex(header, arg2[0]);
            if(argIdx2 == -1)
            {
                throw new Exception("Put requires an object");
            }

            if(argIdx1 != -1) //effect put obj1 obj2
            {
                affector = (o, p) => {
                    Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                    Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                    o2.TakeIn(o1);
                };
            }
            else if(line.tokens.Length == 4) //effect put type obj
            {
                affector = (o, p) => {
                    Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                    Object fresh = GameState.AddObject(arg1[0]);
                    o2.TakeIn(fresh);
                };
            }
            else if(line.tokens.Length == 5) //effect put type obj num
            {
                affector = (o, p) => {
                    Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
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
            if(line.tokens.Length == 3) //effect remove obj
            {
                string[] arg1 = line.tokens[2].Split(dot);
                int argIdx1 = ActionParser.ObjectIndex(header, arg1[0]);
                if(argIdx1 == -1)
                {
                    throw new Exception("Remove requires an object");
                }
                
                affector = (o, p) => {
                    Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                    o1._in.ThrowOut(o1);
                };
            }
            else if(line.tokens.Length == 5)
            {
                string[] arg2 = line.tokens[3].Split(dot);
                int argIdx2 = ActionParser.ObjectIndex(header, arg2[0]);
                if(argIdx2 == -1)
                {
                    throw new Exception("Remove requires an object");
                }

                if(line.tokens[4] == "all") //effect remove type obj all
                {
                    affector = (o, p) => {
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        o2.ThrowOutAll(Object.HasType(line.tokens[2]));
                    };
                }
                else //effect remove type obj num
                {
                    affector = (o, p) => {
                        Object o2 = ActionParser.WalkInChain(o[argIdx2], arg2);
                        int numObjects = Int32.Parse(line.tokens[4]);
                        o2.ThrowOutN(Object.HasType(line.tokens[2]), numObjects);
                    };
                }
            }
        }
        else if(type == Type.Create) //effect create type
        {
            affector = (o, p) => {
                GameState.AddObject(line.tokens[2]);
            };
        }
        else if(type == Type.Delete)
        {
            string[] arg1 = line.tokens[2].Split(dot);
            int argIdx1 = ActionParser.ObjectIndex(header, arg1[0]);
            if(argIdx1 == -1)
            {
                throw new Exception("Remove requires an object");
            }

            if(arg1[arg1.Length - 1] == "contains") //effect delete obj.contains
            {
                affector = (o, p) => {
                    Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                    o1._contains.RemoveAll(o2 => true, o2 => {
                        //Once it's removed throw out everything it contains...
                        if(o2 != o1)
                        {
                            o2.ThrowOutAll(o3 => true);
                        }
                        //...and remove it from the game state
                        GameState.RemoveObject(o2);
                    });
                };
            }
            else //effect delete obj
            {
                affector = (o, p) => {
                    Object o1 = ActionParser.WalkInChain(o[argIdx1], arg1);
                    o1.ThrowOutAll(o2 => true);
                    GameState.RemoveObject(o1);
                };
            }
        }
        else if(type == Type.Distribute)
        {
            //Implement this
        }
        else if(type == Type.Disown)
        {
            //Implement this
        }
        else if(type == Type.Players) //effect players shiftType
        {
            affector = (o, p) => { PlayerState.Shift(line.tokens[2], p); };
        }
        else if(type == Type.SetTimer) //effect settimer num
        {
            affector = (o, p) => {
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
}
