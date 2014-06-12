using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MonsterPang_Console
{
    class Stage
    {
        protected Board board;
        protected Monster monster;
        protected int level;
        public Stage(int level)
        {
            board = new Board();
            monster = new Monster(level);
            this.level = level;
        }
        public void Play()
        {
            while (monster.hp > 0)
            {
                while (board.Deletable() > 0)
                {
                    Damage(board.Deletable());
                    board.Sort();
                }

                if (board.IsMovable() == true) // IsMovabel() 다혜가 구현하기
                {
                    int row1;
                    int col1;
                    int row2;
                    int col2;
                    do
                    {
                        row1 = ReadInteger();
                        col1 = ReadInteger();
                        row2 = ReadInteger();
                        col2 = ReadInteger();
                    } while (!board.Switchable(row1, col1, row2, col2)); //Switchable() 다혜가 구현하기
                    board.Swap(row1, col1, row2, col2); //Swap(..) 다혜가 구현하기
                    continue;
                }
                else
                {
                    board.Refresh();
                    continue;
                }
            }
        }
        public void Damage(int num)
        {
            monster.hp = monster.hp - num;
        }
        public int ReadInteger()
        {
            Console.Write("값을 입력하세요(row, col순서대로입력해주세요): ");
            return int.Parse(Console.ReadLine());
        }
    }
}