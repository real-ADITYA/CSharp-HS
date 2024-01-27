using System;

namespace ClassPractice3Shapes {
    class Program {
        static void Main(string[] args) {
            Shape triangel = new Shape();
        }
    }

    class Shape {

        int[] arrPoints = new int[0];

        public Shape() {
            InitialUI();
        }

        public int[] GetPoint(int pointNum) {
            int xPnt = arrPoints[(pointNum * 2) - 1];
            int yPnt = arrPoints[pointNum * 2];
            int[] userCoords = new int[2] { xPnt, yPnt };
            return userCoords;
        }
        public int[] SetPoint(int pointNum, int x, int y) {
            arrPoints[(pointNum * 2) - 1] = x;
            arrPoints[pointNum * 2] = y;
            int[] userCoords = new int[2] { arrPoints[(pointNum * 2) - 1], arrPoints[pointNum * 2] };
            return userCoords;
        }
        public void AddPoint(int[] userCoordsToAdd) {
            int[] newArrTemp = new int[arrPoints.Length + 2];
            for (int w = 0; w < arrPoints.Length; w++) {
                newArrTemp[w] = arrPoints[w];
            }
            newArrTemp[^2] = userCoordsToAdd[0];
            newArrTemp[^1] = userCoordsToAdd[1];
            arrPoints = newArrTemp;
        }
        public void RemovePoint(int pointNum) {
            int[] newArrTemp = new int[arrPoints.Length - 2];
            arrPoints[(pointNum * 2) - 2] = 2147483647;
            arrPoints[(pointNum * 2) - 1] = 2147483647;
            for (int i2 = 1; i2 < arrPoints.Length; i2++) {
                for (int i = 1; i < arrPoints.Length; i++) {
                    while (arrPoints[i] < arrPoints[i - 1]) {
                        int temp = arrPoints[i - 1];
                        arrPoints[i - 1] = arrPoints[i];
                        arrPoints[i] = temp;
                    }
                }
            }
            for (int w = 0; w < arrPoints.Length - 2; w++) {
                newArrTemp[w] = arrPoints[w];
            }
            arrPoints = newArrTemp;
        }
        public void PrintPoints() {
            Console.Clear();
            int counterTemp = 1;
            for (int e = 0; e < arrPoints.Length; e+=2) {
                Console.WriteLine("{0}: ({1}, {2})   ", counterTemp , arrPoints[e], arrPoints[e+1]);
                counterTemp++;
            }
        }
        public void InitialUI() {
            Console.WriteLine("Enter one of the commands. [Recommended: Type 'help' for commands]\n---------");
            string userResponse = Console.ReadLine();
            while (userResponse != "done") {
                if (userResponse == "help") {
                    Console.WriteLine(" - After you are done creating points, simply type 'done' to calculate the perimeter of the shape. ");
                    Console.WriteLine(" - If you would like to delete a point, simply type 'delete'. ");
                    Console.WriteLine(" - In 'add' mode, make sure you separate each number with a COMMA and SPACE.");
                    Console.WriteLine(" - To exit this menu and return back to creating points, type 'add'.");
                    Console.WriteLine("---------");
                    userResponse = Console.ReadLine();
                }
                else if (userResponse == "delete") {
                    if (arrPoints.Length > 0) {
                        Console.Clear();
                        PrintPoints();
                        Console.WriteLine("---------\nEnter the corresponding number of the coordinate you would like to delete.");
                        int tempResponse = Convert.ToInt32(Console.ReadLine());
                        RemovePoint(tempResponse);
                        Console.WriteLine("---------\nEnter one of the commands. [Recommended: Type 'help' for commands]\n---------");
                        userResponse = Console.ReadLine();
                    }
                    else {
                        Console.WriteLine("You currently don't have any points!");
                        userResponse = "help";
                    }
                }
                else if (userResponse == "add") {
                    Console.Write("X, Y: ");
                    string userCoords = Console.ReadLine();
                    if (userCoords == "help" || userCoords == "delete" || userCoords == "add" || userCoords == "done") {
                        switch (userCoords) {
                            case ("help"):
                                userResponse = "help";
                                break;
                            case ("done"):
                                userResponse = "done";
                                break;
                            case ("delete"):
                                userResponse = "delete";
                                break;
                            case ("add"):
                                userResponse = "add";
                                break;
                            default:
                                Console.WriteLine("Invalid input...");
                                userResponse = "help";
                                break;
                        }
                    }
                    else {
                        AddPoint(ConvertToIntArr(userCoords));
                    }
                }
            }
            while (userResponse == "done") {
                if (arrPoints.Length <= 0) {
                    Console.WriteLine("You currently don't have any points! Restart the program.");
                    userResponse = null;
                }
                else {
                    PrintPoints();
                    Console.WriteLine("---------");
                    FindPerimeter(FindDist());
                    userResponse = null;
                }
            }
        }
        public double[] FindDist() {
            double distPoint;
            double[] distances = new double[arrPoints.Length/2];
            int counterTemp = 0;
            for (int e = 0; e < distances.Length; e+=2) {
                distPoint = Math.Sqrt(Math.Pow(arrPoints[e] - arrPoints[e + 2], 2) + Math.Pow(arrPoints[e+1] - arrPoints[e + 3], 2));
                distances[counterTemp] = distPoint;
                counterTemp++;
            }
            distPoint = Math.Sqrt(Math.Pow((arrPoints[arrPoints.Length - 2] - arrPoints[0]), 2) + Math.Pow((arrPoints[arrPoints.Length - 1] - arrPoints[1]), 2));
            distances[distances.Length - 1] = distPoint;
            return distances;
        }
        public void FindPerimeter(double[] distanceArr) {
            double userPerimeter = 0;
            for (int q = 0; q < distanceArr.Length; q++) {
                userPerimeter = userPerimeter + distanceArr[q];
            }
            int userPeriSimp = Convert.ToInt32(userPerimeter);
            Console.WriteLine("The perimeter of the shape with the coordinates entered is {0} units.\nThis is approximately {1} units long.", userPerimeter, userPeriSimp);
        }

        public int[] ConvertToIntArr(string userInput) {
            string[] strArr = userInput.Split(", ");
            int[] userCoords = new int[2] { Convert.ToInt32(strArr[0]), Convert.ToInt32(strArr[1]) };
            return userCoords;
        }


    }
}