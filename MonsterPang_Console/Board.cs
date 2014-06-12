using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterPang_Console
{
    class Board
    {
        public int boardSize = 6;
        public Stone[,] stones;
        public Random random = new Random(DateTime.Now.Millisecond);

        public Board()
        {
            Refresh();
        }

        public void Refresh()
        {
            stones = new Stone[boardSize, boardSize];// 바둑판을 그림
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    stones[row, col] = new Stone(row, col, random);
                }
            }
        }

        public Stone[,] ShowBoard()
        {
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (stones[row, col] == null)
                        Console.Write("_ ");
                    else
                    {
                        switch (stones[row, col].type)
                        {
                            case 1:
                                Console.Write("1 ");
                                break;
                            case 2:
                                Console.Write("2 ");
                                break;
                            case 3:
                                Console.Write("3 ");
                                break;
                            case 4:
                                Console.Write("4 ");
                                break;
                            case 5:
                                Console.Write("5 ");
                                break;
                            case 6:
                                Console.Write("6 ");
                                break;
                            default:
                                Console.Write("* ");
                                break;
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            return stones;
        }

        public bool IsLinear(Stone stone)
        {
            if ((Score(stone.row, stone.col)[0] >= 2) || (Score(stone.row, stone.col)[1] >= 2))
                return true;
            else
                return false;
        }

        public int Check(ref int score, int row, int col, int difRow, int difCol)
        {
            if ((GetStonesData(row + difRow, col + difCol) != null) && (stones[row, col].type != stones[row + difRow, col + difCol].type))
                return 0;
            else if ((GetStonesData(row + difRow, col + difCol) != null) && (stones[row, col].type == stones[row + difRow, col + difCol].type))
            {
                score = score + 1;
                Check(ref score, row + difRow, col + difCol, difRow, difCol);
                return score;
            }
            return score;
        }

        public int[] Score(int row, int col)
        {
            int[] score = new int[2];
            Check(ref score[0], row, col, 0, 1);
            Check(ref score[0], row, col, 0, -1);
            Check(ref score[1], row, col, 1, 0);
            Check(ref score[1], row, col, -1, 0);
            return score;
        }

        public int Deletable()
        {
            int[,] scores = new int[boardSize, boardSize];
            int maxCol = 0;
            int maxRow = boardSize - 1;
            int maxScore = scores[maxRow, maxCol];
            int maxType = stones[maxRow, maxCol].type;

            for (int col = 0; col < boardSize; col++)
            {
                for (int row = boardSize - 1; row >= 0; row--)
                {
                    if ((Score(row, col)[0] >= 2) || (Score(row, col)[1] >= 2))
                    {
                        if ((Score(row, col)[0] >= 2) && (Score(row, col)[1] >= 2))
                        {
                            scores[row, col] = Score(row, col)[0] + Score(row, col)[1] + 1;
                        }
                        else
                        {
                            if (Score(row, col)[0] > Score(row, col)[1])
                            {
                                scores[row, col] = Score(row, col)[0] + 1;
                            }
                            else
                            {
                                scores[row, col] = Score(row, col)[1] + 1;
                            }
                        }
                    }
                }
            }

            for (int col = 0; col < boardSize; col++)
            {
                for (int row = boardSize - 1; row >= 0; row--)
                {
                    if (scores[row, col] > maxScore)
                    {
                        maxScore = scores[row, col];
                        maxCol = col;
                        maxRow = row;
                        maxType = stones[row, col].type;
                    }
                }
            }
            Console.WriteLine("Delete [{0},{1}]", maxRow, maxCol);
            Delete(maxRow, maxCol, maxType);
            
            if (maxScore < 3)
                return -1;
            else
                return maxScore;
        }

        public void Delete(int row, int col, int type)
        {
            if ((Score(row, col)[0] >= 2) || (Score(row, col)[1] >= 2))
            {
                if ((Score(row, col)[0] >= 2) && (Score(row, col)[1] >= 2))
                {
                    DelStone(row, col, 0, 1, type);
                    DelStone(row, col, 0, -1, type);
                    DelStone(row, col, 1, 0, type);
                    DelStone(row, col, -1, 0, type);
                    stones[row, col].type = 0;
                }
                else
                {
                    if (Score(row, col)[0] >= 2)
                    {
                        DelStone(row, col, 0, 1, type);
                        DelStone(row, col, 0, -1, type);
                        stones[row, col].type = 0;
                    }
                    else
                    {
                        DelStone(row, col, 1, 0, type);
                        DelStone(row, col, -1, 0, type);
                        stones[row, col].type = 0;
                    }
                }
                ShowBoard();
            }
        }

        public void DelStone(int row, int col, int difRow, int difCol, int type)
        {
            while ((GetStonesData(row + difRow, col + difCol) != null) && (type == stones[row + difRow, col + difCol].type))
            {
                stones[row + difRow, col + difCol].type = 0;
                row = row + difRow;
                col = col + difCol;
                ShowBoard();
            }
        }

        public void Sort() //은주 - 6/11
        {
            for (int col = 0; col < boardSize; col++)
            {
                for (int row = boardSize - 1 ; row >= 0; row--)
                {
                    SortStone(row, col);
                }
            }
        }

        public void SortStone(int row, int col) //은주 - 6/11
        {
            if (stones[row, col].type == 0)
            {
                Console.WriteLine("now sorting[{0},{1}]", row, col);
                int temp = row - 1;
                while (stones[row, col].type == 0)
                {
                    if (GetStonesData(temp, col) == null)
                        stones[row, col] = new Stone(row, col, random);
                    else if (stones[temp, col].type != 0)
                    {
                        stones[row, col].type = stones[temp, col].type;
                        stones[temp, col].type = 0;
                    }
                    else
                        temp--;
                }
                ShowBoard();
            }
        }

        public void Swap(int row1, int col1, int row2, int col2) // 다혜 구현
        {
            int temp = stones[row1, col1].type;
            stones[row1, col1].type = stones[row2, col2].type;
            stones[row2, col2].type = temp;
        }

        public bool Switchable(int row1, int col1, int row2, int col2)
        {
            if (GetStonesData(row1, col1) == null || GetStonesData(row2, col2) == null) //방어코드 - 덕철오빠한테 배운 것.
            {
                return false;
            }
            Stone[,] temps = new Stone[boardSize, boardSize];
            for (int row = 0; row < boardSize; row++)
            {
                for(int col = 0; col < boardSize; col++)
                {
                    temps[row, col] = stones[row, col];
                }
            }
            Stone temp = new Stone (row1, col1, random);
            temp.type = temps[row1, col1].type;
            temps[row1, col1].type = temps[row2, col2].type;
            temps[row2, col2].type = temp.type;

            if (IsLinear(row1, col1, ref temps)) // ref로 한 이유는, 메모리를 아끼기 위해서... 아껴지나?
            {
                return true;
            }
            else if (IsLinear(row2, col2, ref temps))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsLinear(int row, int col, ref Stone[,] temps) // (남은 과제)for문을 이용해 간단하게 refactoring해보기!
        {
            if ((GetStonesData(row - 2, col) != null) && AreSameType(row - 2, col, 1, ref temps))
            // (GetStonesData(row-2, col) != null)는 체크 안해봐도 됨!
            // 첫번째 조건에서 false가 나오면 두번째 조건(AreSameType)은 체크하지 않고 넘어가므로 execption이 일어나지 않으리라 예상!
            {
                return true;
            }
            else if ((GetStonesData(row - 1, col) != null) && (GetStonesData(row + 1, col) != null) && AreSameType(row - 1, col, 1, ref temps))
            {
                return true;
            }
            else if ((GetStonesData(row + 2, col) != null) && AreSameType(row, col, 1, ref temps))
            {
                return true;
            }
            else if ((GetStonesData(row, col - 2) != null) && AreSameType(row, col - 2, 2, ref temps))
            {
                return true;
            }
            else if ((GetStonesData(row, col - 1) != null) && (GetStonesData(row, col + 1) != null) && AreSameType(row, col - 1, 2, ref temps))
            {
                return true;
            }
            else if ((GetStonesData(row, col - 2) != null) && AreSameType(row, col, 2, ref temps))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool AreSameType(int row, int col, int direction, ref Stone[,] temps)
        {
            int x;
            if (direction == 1)
            {
                x = row;
            }
            else if (direction == 2)
            {
                x = col;
            }
            else
            {
                Console.WriteLine("Error @AreSameType in Board class");
                return false;
            }
            int firstType = temps[row, col].type;
            int count = 1;
            for (int i = x + 1; i < x + 3; i++)
            {
                if (direction == 1)
                {
                    if (temps[i, col].type == firstType)
                    {
                        count++;
                    }
                }
                else
                {
                    if (temps[row, i].type == firstType)
                    {
                        count++;
                    }
                }

            }
            if (count == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsMovable()
        {
            for (int row = 0; row < stones.GetLength(0); row++)
            {
                for (int col = 0; col < stones.GetLength(1); col++)
                {
                    if (Switchable(row, col, row, col + 1) || Switchable(row, col, row + 1, col))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Stone GetStonesData(int row, int col) // exception check
        {
            if (((row >= boardSize) || (row < 0)) || ((col >= boardSize) || (col < 0)))
                return null;
            return stones[row, col];
        }
    }
}
