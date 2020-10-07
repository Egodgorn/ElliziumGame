using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellizium.Core.Utility
{
    public static class Extender
    {



        public static int GetFree(this bool[] data)
        {
            for(int i = 0; i < data.Length; i++)
            {
                if (data[i]) return i;
            }

            return -1;
        }
    }
}
