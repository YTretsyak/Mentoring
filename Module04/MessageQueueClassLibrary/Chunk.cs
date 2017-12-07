using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueueClassLibrary
{
  public class Chunk
  {
    public int Position { get; set; }

    public int Size { get; set; }

    public List<byte> Buffer { get; set; }

    public int BufferSize { get; set; }
  }
}
