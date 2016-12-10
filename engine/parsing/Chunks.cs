/* Takes a file and breaks it into chunks */

public class Chunks
{
    public Chunk[] chunks;

    public Chunks(File f)
    {
        //First count the number of chunks so we can allocate the array
        int numChunks = 0;
        for(int i = 0; i < f.lines.Length; i++)
        {
            if(f.lines[i].indent == 0)
            {
                numChunks++;
            }
        }

        Line[] chunkLines;
        int idx = 1;
        int prevIdx = 0;
        chunks = new Chunk[numChunks];
        for(int c = 0; c < numChunks; c++)
        {
            while(idx < f.lines.Length && f.lines[idx].indent != 0)
            {
                idx++;
            }
            chunkLines = new Line[idx - prevIdx - 1];
            for(int l = 0; l < chunkLines.Length; l++)
            {
                chunkLines[l] = f.lines[prevIdx + l + 1];
            }
            chunks[c] = new Chunk(f.lines[idx], chunkLines);
            prevIdx = idx;
        }
    }
}
