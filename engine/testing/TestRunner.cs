using System;

public class TestRunner
{
    private int testNumber;
    public void Compare(string expect, string val)
    {
        if(expect != val)
        {
            Console.WriteLine("Test " + testNumber + " failed.");
            Console.WriteLine("Expected: " + expect + " \tGot: " + val);
        }
        testNumber++;
    }

    public void Compare(int expect, int val)
    {
        if(expect != val)
        {
            Console.WriteLine("Test " + testNumber + " failed.");
            Console.WriteLine("Expected: " + expect + " \tGot: " + val);
        }
        testNumber++;
    }

    public void Compare(bool expect, bool val)
    {
        if(expect != val)
        {
            Console.WriteLine("Test " + testNumber + " failed.");
            Console.WriteLine("Expected: " + expect + " \tGot: " + val);
        }
        testNumber++;
    }

    public TestRunner()
    {
        testNumber = 0;
    }
}
