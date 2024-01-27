using System;

/*  Multidimensional Arrays (TableMaker3000)*/

namespace MultidimentionalArrays1Basics {
    class Program {
        static void Main(string[] args) {
            PrintTable(FillTable(CreateArrTab()));
        }

        static int[,] CreateArrTab() {
            Console.Write("Enter the amount of rows: ");
            int tableWidth = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the amount of columns: ");
            int tableLength = Convert.ToInt32(Console.ReadLine());
            return new int[tableWidth, tableLength];
        }
        static int[,] FillTable(int[,] multArr) {
            Random rndm = new Random();
            for (int q = 0; q < multArr.GetLength(1); q++) {
                for (int e = 0; e < multArr.GetLength(0); e++) {
                    multArr[e, q] = rndm.Next(0, 9);
                }
            }
            return multArr;
        }
        static void PrintTable(int[,] multArr) {
            Console.WriteLine("---------");
            for (int q = 0; q < multArr.GetLength(1); q++) {
                for (int e = 0; e < multArr.GetLength(0); e++) {
                    Console.Write(multArr[e, q] + "  ");
                }
                Console.WriteLine();
            }
        }

    }
}