using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minimax
{
    public class Minimax
    {
        public char[,] board { get; set; }
        //public int[,] position { get; set; }

        public Minimax()
        {
            board = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    board[i, j] = '.';
                }
            }
        }

        public int MinMax(char[,] state, int depth, bool isMAX)
        {
            int CheckState = GameOver(state);
            if(CheckState == 0)
            {
                return 0;
            }
            else if(CheckState == 1)
            {
                return 1;
            }
            else if(CheckState == -1)
            {
                return -1;
            }
            else
            {
                if (isMAX)
                {
                    int bestscore = int.MinValue;
                }
            }

            return 0;
        }

        public int GameOver(char[,] state)
        {
            // 1. Check Horizontally (Rows)
            for (int i = 0; i < 3; i++)
            {
                string build = $"{state[i, 0]}{state[i, 1]}{state[i, 2]}";
                if (build == "XXX") return 1;
                if (build == "OOO") return -1;
            }

            // 2. Check Vertically (Columns)
            for (int j = 0; j < 3; j++)
            {
                string build = $"{state[0, j]}{state[1, j]}{state[2, j]}";
                if (build == "XXX") return 1;
                if (build == "OOO") return -1;
            }

            // 3. Check Diagonals
            string diag1 = $"{state[0, 0]}{state[1, 1]}{state[2, 2]}"; // Top-left to bottom-right
            string diag2 = $"{state[0, 2]}{state[1, 1]}{state[2, 0]}"; // Top-right to bottom-left

            if (diag1 == "XXX" || diag2 == "XXX") return 1;
            if (diag1 == "OOO" || diag2 == "OOO") return -1;

            for (int i = 0;i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (state[i,j] == '.')
                    {
                        return 2;
                    }
                }
            }

            return 0;
        }

        public void PrintBoard()
        {
            for(int i = 0;i <= 2;i++)
            {
                for(int j =0;j <= 2; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
