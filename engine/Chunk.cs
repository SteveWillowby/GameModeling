/* This class simply a group of lines */
using System;

public class Chunk
{
    protected Line header;
    protected Line[] lines;

    public Chunk(Line h, Line[] l)
    {
        header = h;
        lines = l;
    }
}
