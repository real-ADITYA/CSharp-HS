using System;

namespace ScoredArraySampler {
    class Program {
        static void Main(string[] args) {
            Crdnt userCo = new Crdnt();
            NTA userNu = new NTA();
            AntiAliasing userAA = new AntiAliasing();
            CC clrDynm = new CC();
            //Say the intro and start the program
            SayTheThing();
            ConsoleKey userKey = Console.ReadKey().Key;
            while (userKey != ConsoleKey.Escape) {
                if(userKey == ConsoleKey.A) { Console.Clear(); userCo.RunCoordinatedCoordinates(); userKey = Console.ReadKey().Key; }
                else if (userKey == ConsoleKey.M) { Console.Clear(); userNu.RunNumberToArray(); userKey = Console.ReadKey().Key; }
                else if (userKey == ConsoleKey.S) { Console.Clear(); userAA.AskUserMain(); userKey = Console.ReadKey().Key; }
                else if (userKey == ConsoleKey.H) { Console.Clear(); SayTheThing(); userKey = Console.ReadKey().Key; }
                else { Console.Clear();  SayTheThing();  userKey = Console.ReadKey().Key; }
            }
        }
        private static void SayTheThing() {
            Console.WriteLine("Welcome to the Array Sampler program! At any time in the program, you can use the following commands.\n   " +
                "Pressing 'AA': Program of coordinates\n   Pressing 'MM': Large number to an array of single digits\n   Pressing 'SS': Enlarge an image's resolution.\n" +
                "   Pressing 'HH': Access this help menu again\n   Pressing 'Esc': Exit the program\n . . . . . . . . . . . . . . . . . .");
        }
    }
    class AntiAliasing {
        /*Psuedocode
             -Ask for user array size
             -Double array size and copy the values in index*2
                -Ex: array[2, 1] to newArray[2*2, 1*2]
             -Print it!
             */
        CC clrDynm = new CC();
        Random rndDynm = new Random();
        int[] imageRes = new int[2]; //Horizontal res, Vertical res
        int[,] mainImage; //Order: Vertical, Horizontal
        int[,] enlargedImage;

        public void AskUserMain() {
            //Ask user for resolution and save it to imageRes var
            clrDynm.ChangeColorTxt("yellow", "\nEnter the size of the image you'd like to use (Preferably a small one):\n" +
                "[Seperate with an x](Ex: 19x10)\n");
            string[] temp = Console.ReadLine().Split("x");
            imageRes[0] = Convert.ToInt32(temp[0]);
            imageRes[1] = Convert.ToInt32(temp[1]);
            //Create new image & fill with random values
            mainImage = new int[imageRes[1], imageRes[0]];
            for (int col = 0; col < imageRes[0]; col++) {
                for (int row = 0; row < imageRes[1]; row++) { mainImage[row, col] = rndDynm.Next(0, 10); }
            }
            //Print out image
            Console.Clear();
            PrintImage(mainImage);
            //Loading
            clrDynm.ChangeColorTxt("red", "\nCreating larger image.");  System.Threading.Thread.Sleep(500);
            clrDynm.ChangeColorTxt("red", "."); System.Threading.Thread.Sleep(500);
            clrDynm.ChangeColorTxt("red", "."); System.Threading.Thread.Sleep(250);
            clrDynm.ChangeColorTxt("red", ".\n"); System.Threading.Thread.Sleep(250);
            //Do the main ting to enlarge the image
            enlargedImage = new int[imageRes[1]*2, imageRes[0]*2];
        }
        private void PrintImage(int[,] array) {
            for (int col = 0; col < array.GetLength(0); col++) {
                for (int row = 0; row < array.GetLength(1); row++) { clrDynm.ChangeColorTxt("default", Convert.ToString(array[col, row])); }
                Console.WriteLine();
            }
        }
    }
    class NTA {
        CC clrDynm = new CC();
        int[] singleDigits;
        public void RunNumberToArray() {
            Console.WriteLine("\n");
            clrDynm.ChangeColorTxt("yellow", "Enter the HUGE number. (Ex: 748003621)\n");
            string userBN = Console.ReadLine(); Console.Clear();
            if (userBN.Length > 0) {
                singleDigits = new int[userBN.Length];
                clrDynm.ChangeColorTxt("yellow", "Your number: " + userBN);
                clrDynm.ChangeColorTxt("yellow", "\nArray of Digits: ");
                for (int i = 0; i < userBN.Length; i++) {
                    singleDigits[i] = userBN[i];
                    Console.Write(userBN[i] + ", ");
                }
                Console.WriteLine();
            }
            else { clrDynm.ChangeColorTxt("red", "You have entered an invaid number. Terminating program..."); }
        }
    }
    class Crdnt {
        CC clrDynm = new CC();
        string[] rawXC, rawYC;
        float[] xCoords, yCoords;
        float[] allCoords;
        public void RunCoordinatedCoordinates() {
            clrDynm.ChangeColorTxt("blue", "Enter the desired x coordinates.\n(Seperate them with a COMMA and SPACE - Ex: 5, 4, 3, 4)\n");
            rawXC = Console.ReadLine().Split(", "); Console.Clear();
            xCoords = new float[rawXC.Length];
            for (int i = 0; i < rawXC.Length; i++) { xCoords[i] = Convert.ToInt32(rawXC[i]); }
            PrintX();
            clrDynm.ChangeColorTxt("blue", "Enter the desired y coordinates.\n(Seperate them with a COMMA and SPACE - Ex: 5, 4, 3, 4)\n");
            rawYC = Console.ReadLine().Split(", "); Console.Clear();
            yCoords = new float[rawYC.Length];
            for (int i = 0; i < rawYC.Length; i++) { yCoords[i] = Convert.ToInt32(rawYC[i]); }
            allCoords = new float[xCoords.Length + yCoords.Length];
            int temp = 0;
            for (int i = 0; i < allCoords.Length; i = i + 2) { allCoords[i] = xCoords[temp]; allCoords[i + 1] = yCoords[temp]; temp++; }
            PrintX(); PrintY(); clrDynm.ChangeColorTxt("yellow", "---------\n");
            int tempNum = 1;
            for (int i = 0; i < allCoords.Length; i = i + 2) {
                clrDynm.ChangeColorTxt("blue", Convert.ToString(tempNum) + ": " + Convert.ToString(allCoords[i])
+ ", " + Convert.ToString(allCoords[i + 1]) + "\n"); tempNum++;
            }
        }
        private void PrintX() {
            Console.Write("X Values: ");
            for (int i = 0; i < xCoords.Length; i++) { clrDynm.ChangeColorTxt("yellow", Convert.ToString(xCoords[i]) + " "); }
            Console.WriteLine();
        }
        private void PrintY() {
            Console.Write("Y Values: ");
            for (int i = 0; i < yCoords.Length; i++) { clrDynm.ChangeColorTxt("yellow", Convert.ToString(yCoords[i]) + " "); }
            Console.WriteLine();
        }
    }
    class CC {
        public void ChangeColor(string color) {
            switch (color) {
                case ("y"):
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case ("r"):
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case ("w"):
                case ("default"):
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
        public void ChangeColorTxt(string color, string text) {
            switch (color) {
                case ("yellow"):
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ("red"):
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ("blue"):
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ("default"):
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(text);
                    break;
            }
        }
    }




}


