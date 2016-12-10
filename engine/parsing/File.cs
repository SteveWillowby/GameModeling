using System;

/* A container for lines that can populate those lines with the
 * contents of a file.
 */

public class File
{
    public Line[] lines;

    public File(string filename)
    {
        System.IO.StreamReader file = new System.IO.StreamReader(filename);
        string line;
        int count = 0; //First just count the number of non-blank lines.
        while((line = file.ReadLine()) != null)
        {
            if(!String.IsNullOrWhiteSpace(line))
            {
                count++;
            }
        }

        lines = new Line[count]; //Then allocate space for them
        file = new System.IO.StreamReader(filename);
        count = 0;
        while((line = file.ReadLine()) != null) //and fill them
        {
            if(!String.IsNullOrWhiteSpace(line))
            {
                lines[count] = new Line(line);
                count++;
            }
        }
    }
}
