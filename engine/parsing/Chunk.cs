using System;

/* This class simply stores lines as a header and subsequent lines
 *
 * The intuition here is that something like:
 *
 * "Action MyAction Thing one Thing two
 *   requirement .......
 *   effect ......
 *   effect ......"
 *
 * falls nicely into the header-lines scheme.
 */

public class Chunk
{
    public Line header;
    public Line[] lines;

    public Chunk(Line h, Line[] l)
    {
        header = h;
        lines = l;
    }
}
