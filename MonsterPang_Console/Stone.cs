using System;

namespace MonsterPang_Console
{
    class Stone
    {
        public int type;
        public int row;
        public int col;

        public Stone(int row, int col, Random random)
        {
            type = random.Next(1, 7);
            this.row = row;
            this.col = col;
        }
    }
}