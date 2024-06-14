using System;
using System.Collections.Generic;
using System.Linq;

namespace SodokuValid
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] validSoduku = new string[9, 9]
            {
                {"5","3",".", ".","7",".", ".",".","."},
                {"6",".",".", "1","9","5", ".",".","."},
                {".","9","8", ".",".",".", ".","6","."},

                {"8",".",".", ".","6",".", ".",".","3"},
                {"4",".",".", "8",".","3", ".",".","1"},
                {"7",".",".", ".","2",".", ".",".","6"},

                {".","6",".", ".",".",".", "2","8","."},
                {".",".",".", "4","1","9", ".",".","5"},
                {".",".",".", ".","8",".", ".","7","9"}
            };
            string[,] invalidSoduku = new string[9, 9]
           {
                {"8","3",".", ".","7",".", ".",".","."},
                {"6",".",".", "1","9","5", ".",".","."},
                {".","9","8", ".",".",".", ".","6","."},

                {"8",".",".", ".","6",".", ".",".","3"},
                {"4",".",".", "8",".","3", ".",".","1"},
                {"7",".",".", ".","2",".", ".",".","6"},

                {".","6",".", ".",".",".", "2","8","."},
                {".",".",".", "4","1","9", ".",".","5"},
                {".",".",".", ".","8",".", ".","7","9"}
           };
            string[,] solvedSoduku = new string[9, 9]
           {
                {"5","3","4", "6","7","8", "9","1","2"},
                {"6","7","2", "1","9","5", "3","4","8"},
                {"1","9","8", "3","4","2", "5","6","7"},

                {"8","5","9", "7","6","1", "4","2","3"},
                {"4","2","6", "8","5","3", "7","9","1"},
                {"7","1","3", "9","2","4", "8","5","6"},

                {"9","6","1", "5","3","7", "2","8","4"},
                {"2","8","7", "4","1","9", "6","3","5"},
                {"3","4","5", "2","8","6", "1","7","9"}
           };


            //Console.WriteLine(isValid(validSoduku));
            //Console.WriteLine(isValid(invalidSoduku));
            //Console.WriteLine(isValid(solvedSoduku));
            //PrintSoduku(solvedSoduku);
            PrintSoduku(Solve(validSoduku));
        }

        static bool checkNine(string[] nines)
        {
            var checkedNumbers = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (nines[i] != ".")
                {
                    checkedNumbers.Add(Convert.ToInt32(nines[i]));
                    if (Convert.ToInt32(nines[i]) <= 0 || Convert.ToInt32(nines[i]) > 9)
                    {
                        return false;
                    }
                }
            }
            if (!IsDistinct(checkedNumbers))
            {
                return false;
            }
            return true;
        }

        static bool isValid(string[,] input)
        {
            bool isValid = false;
            if (input == null) { return false; }
            var checkedNumbers = new List<int>();

            // Test ligne par ligne
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (input[j, i] != ".")
                    {
                        checkedNumbers.Add(Convert.ToInt32(input[j, i]));
                        if (Convert.ToInt32(input[j, i]) <= 0 || Convert.ToInt32(input[j, i]) > 9)
                        {
                            return false;
                        }
                    }
                }
                isValid = IsDistinct(checkedNumbers);
                if (!isValid)
                {
                    return false;
                }
                checkedNumbers.Clear();
            }
            // Test colonne par colonne
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (input[j, i] != ".")
                    {
                        checkedNumbers.Add(Convert.ToInt32(input[j, i]));
                        if (Convert.ToInt32(input[j, i]) <= 0 || Convert.ToInt32(input[j, i]) > 9)
                        {
                            return false;
                        }
                    }
                }
                isValid = IsDistinct(checkedNumbers);
                if (!isValid)
                {
                    return false;
                }
                checkedNumbers.Clear();
            }

            // Test par case 3x3
            for (int o = 0; o < 9; o += 3)
            {
                for (int p = 0; p < 9; p += 3)
                {
                    for (int i = o; i < o + 3; i++)
                    {
                        for (int j = p; j < p + 3; j++)
                        {
                            if (input[i, j] != ".")
                            {
                                checkedNumbers.Add(Convert.ToInt32(input[i, j]));
                                if (Convert.ToInt32(input[i, j]) <= 0 || Convert.ToInt32(input[i, j]) > 9)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    isValid = IsDistinct(checkedNumbers);
                    if (!isValid)
                    {
                        return false;
                    }
                    checkedNumbers.Clear();
                }
            }
            return true;
        }

        static bool IsDistinct(List<int> list)
        {
            if (list.Distinct().Count() != list.Count) { return false; }
            else { return true; }
        }

        static string[,] Solve(string[,] soduku)
        {
            int[] validNumber = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] reverseValidNumber = { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            for (int s = 0; s < 1; s++)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (soduku[i, j] == ".")
                        {
                            int indexValid = 0;
                            foreach (var n in validNumber)
                            {
                                soduku[i, j] = $"{n}";

                                Console.WriteLine(indexValid);
                                //Console.WriteLine(soduku[i, j]);
                                if (!isValid(soduku))
                                {
                                    soduku[i, j] = ".";
                                }
                                else
                                {
                                    indexValid++;
                                }
                            }
                        }
                    }
                }
                //PrintSoduku(soduku);
            }
            return soduku;
        }
        static void PrintSoduku(string[,] soduku)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(soduku[i, j] + " ");

                }
                Console.WriteLine();
            }

        }

    }
}
