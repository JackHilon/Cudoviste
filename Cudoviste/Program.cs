using System;

namespace Cudoviste
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cudoviste 
            // https://open.kattis.com/problems/cudoviste 
            // Map processing (string - mask)

            var parameters = EnterRowCol();
            int row = parameters[0];
            int col = parameters[0];

            var map = GetMap(row, col);


            int[] accumulator = new int[5] { 0, 0, 0, 0, 0 };
            try
            {
                accumulator = GetAccumulator(map, row, col);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " || " + ex.GetType().ToString());
            }

            finally
            {
                PrintList(accumulator);
            }
        }
        private static int[] GetAccumulator(string[] map, int row, int col)
        {
            int temp;
            int[] accumulator = new int[5] { 0, 0, 0, 0, 0 };
            for (int r = 0; r < row - 1; r++)
            {
                for (int c = 0; c < col - 1; c++)
                {
                    temp = MaskCheck(map[r][c], map[r][c+1], map[r + 1][c], map[r + 1][c + 1]);
                    MyInc(accumulator, temp);
                }
            }
            return accumulator;
        }

        private static void PrintList(int[] arr)
        {
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }
        private static void MyInc(int[] acc, int flag)
        {
            if (flag == 0)
                acc[0]++;
            if (flag == 1)
                acc[1]++;
            if (flag == 2)
                acc[2]++;
            if (flag == 3)
                acc[3]++;
            if (flag == 4)
                acc[4]++;
        }
        private static int MaskCheck(char a, char b, char c, char d)
        {
            int sum = XNum(a, b, c, d);

            if (HashFree(a, b, c, d) == false)
                return 5; // contine situation

            else if (HashFree(a, b, c, d) == true && sum == 0)
                return 0;
            else if (HashFree(a, b, c, d) == true && sum == 1)
                return 1;
            else if (HashFree(a, b, c, d) == true && sum == 2)
                return 2;
            else if (HashFree(a, b, c, d) == true && sum == 3)
                return 3;
            else
                //else if (HashFree(a, b, c, d) == true && sum == 4)
                return 4;
        }
        private static int XNum(char a, char b, char c, char d)
        {
            int sum = 0;
            if (a == 'X')
                sum++;
            if (b == 'X')
                sum++; 
            if (c == 'X')
                sum++; 
            if (d == 'X')
                sum++;
            return sum;
        }
        private static bool HashFree(char a, char b, char c, char d)
        {
            if (a == '#' || b == '#' || c == '#' || d == '#')
                return false;
            else
                return true;
        }
        private static string[] GetMap(int row, int col)
        {
            string[] rows = new string[row];
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = EnterOneRow(col);
            }
            return rows;
        }

        private static string EnterOneRow(int col)
        {
            string str = "";
            try
            {
                str = Console.ReadLine();
                if (str.Length != col)
                    throw new ArgumentException();
                if (StringCheck(str) == false)
                    throw new FormatException();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return EnterOneRow(col);
            }
            return str;
        }
        private static int[] EnterRowCol()
        {
            var res = new int[2];
            var arr = new string[2];
            try
            {
                arr = Console.ReadLine().Split(' ', 2);
                for (int i = 0; i < arr.Length; i++)
                {
                    res[i] = int.Parse(arr[i]);
                    if (res[i] < 2 || res[i] > 50)
                        throw new ArgumentException();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return EnterRowCol();
            }
            return res;
        }
        private static bool StringCheck(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] != '#' && str[i] != '.' && str[i] != 'X')
                    return false;
            }
            return true;
        }
    }
}
