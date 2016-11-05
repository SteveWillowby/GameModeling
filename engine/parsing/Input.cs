using System;
using System.Linq;

public class Input
{
    public enum ParamType {Object, Type, Conditional, Number, Modifier};

    public static bool IsNumber(string s)
    {
        int n;
        return int.TryParse(s, out n);
    }
}
