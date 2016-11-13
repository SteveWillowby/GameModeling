/*  */
using System;

public class File
{
    public Line[] lines;

    public File(string filename)
    {
        //Open File
        //Fill Lines, Skipping Blank Ones

        System.IO.StreamReader file = new System.IO.StreamReader(filename);
        string line;
        int count = 0;
        while((line = file.ReadLine()) != null)
        {
            if(!String.IsNullOrWhiteSpace(line))
            {
                count++;
            }
        }

        lines = new Line[count];
        file = new System.IO.StreamReader(filename);
        count = 0;
        while((line = file.ReadLine()) != null)
        {
            if(!String.IsNullOrWhiteSpace(line))
            {
                lines[count] = new Line(line);
                count++;
            }
        }
    }
}
