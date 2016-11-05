/* This class simply a group of lines */
using System;

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
