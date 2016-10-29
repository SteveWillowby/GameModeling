using System;

public class Requirement
{
    protected int[] indices;
    protected Func<Object[], bool> evaluator;

    public bool Met(Object[] o)
    {
        return evaluator(o);
    }

    public Requirement(string command, string[] parameters, string[] types)
    {
        switch(command)
        {
            case "in":
                In(parameters);
                break;
            case "contains":
                Contains(parameters);
                break;
            default:
                throw new Exception(command + " is not a valid requirement");
                break;
        }
    }

    protected void SetIndices(int[] i, int size)
    {
        if(i.Length != size)
        {
            throw new Exception("Bad requirement initialization list");
        }
        indices = new int[size];
        for(int j = 0; j < size; j++)
        {
            indices[j] = i[j];
        }
    }

    protected void In(string[] parameters, string[] types)
    {
        int len = parameters.Length;
        if(len == 2) //a in b
        {
            
        }
        else if(len == 3)
        {
            
        }
        else if(len == 4)
        {
            if(parameters[2] == "<")
            {
                
            }
            else if(parameters[2] == ">")
            {
                
            }
            else
            {
                throw new Exception("Invalid third parameter for in");
            }
        }
        else
        {
            throw new Exception("Invalid number of parameters");
        }
    }

    protected void Contains(string[] parameters)
    {
        
    }
}
