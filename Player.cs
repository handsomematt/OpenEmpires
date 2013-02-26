using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEmpires
{
    public class Player
    {
        public byte SupplyUsed;
        public byte SupplyLimit;

        public ushort Wood;
        public ushort Food;
        public ushort Gold;
        public ushort Stone;

        public Player()
        {
            Wood = 0;
            Food = 0;
            Gold = 0;
            Stone = 0;
            SupplyUsed = 0;
            SupplyLimit = 6; //6 is the starting limit ( i think )
        }
    }
}
