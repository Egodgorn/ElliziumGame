using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellizium.Core.Generation.Blocks
{
    public struct Block
    {
        public ushort ID;
        public ushort TextureID;

        public Block(ushort id, ushort textureID)
        {
            ID = id;
            TextureID = textureID;
        }
    }
}
