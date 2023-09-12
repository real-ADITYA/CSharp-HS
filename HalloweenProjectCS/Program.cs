using System;

namespace HalloweenProjectCS {
    class Program {
        static void Main(string[] args) {
                Console.WriteLine("-----October 31,  2029-----\n You wake up. As you get up from your bed, you notice that it is unusually dark outside. What do you do?");
                Console.WriteLine("(A)Check the clock\n(B)Go back to sleep\n(C)Go downstairs");
                string userAnswer1 = Console.ReadLine();
                int answer1 = StoryBegin(userAnswer1);
                //PART 1
                switch (answer1) {
                    case 0:
                    case 2:
                        break;
                    case 1:
                    case -1:
                        Environment.Exit(0);
                        break;
                }
                //PART 2
                Console.WriteLine("\n\nEverywhere around you is silent, and no lights are on. What do you do?");
                Console.WriteLine("(A)Find the nearest light switch.\n(B)Find a flashlight.\n(C)Run back upstairs and sleep.");
                string userAnswer2 = Console.ReadLine();
                int answer2 = StoryPT2(userAnswer2);
                switch (answer2) {
                    case 0:
                    case 1:
                        break;
                    case -1:
                    case 2:
                        Environment.Exit(0);
                        break;
                }
                //PART 3 + 4END
                Console.WriteLine("\n\nUnfortunately, you cannot find a light source. You walk to your front door and go outside.\nAs you step in the crisp, cold fall weather, you notice something that makes you feel uneasy.");
                Console.WriteLine("All of your neighbors' houses look deserted and not one car is seen on the street.\nHowever, within this silence, you hear leaves shaking violently in the opposite direction. What do you do?");
                Console.WriteLine("(A)TURN AROUND!\n(B)CLIMB UP A TREE!\n(C)RUN ACROSS THE STREET TO YOUR NEIGHBORS HOUSE!");
                string userAnswer3 = Console.ReadLine();
                int answer3 = StoryPT3(userAnswer3);
            switch (answer3) {
                case 0:
                case 1:
                    Console.WriteLine("(Enter 'Q')");
                    char userPressQ = Convert.ToChar(Console.ReadLine());
                    if (userPressQ == 'q' || userPressQ == 'Q') {
                        for (int hahaSpam = 0; hahaSpam < 10; hahaSpam++) {
                            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxGAMExxxxxxxxxOVERxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        }
                    }
                    else {
                    }
                    break;
                case 2:
                    Console.WriteLine("(Enter 'E')");
                    char userPressE = Convert.ToChar(Console.ReadLine());
                    if (userPressE == 'e' || userPressE == 'E') {
                        Console.WriteLine("The sound increases and approaches closer to you.\n\nWHAT DO YOU DO?");
                    }
                    else {
                    }
                    break;
                case -1:
                    Environment.Exit(0);
                    break;
            }
            Console.WriteLine("(A)TURN AROUND!\n(B)SCREAM AND POUND ON THE NEIGHBOR'S DOOR!\n(C)Do nothing. Pull out your phone and watch a twitch stream.");
            char userAnswer4 = Convert.ToChar(Console.ReadLine());
            switch (userAnswer4) {
                case 'a':
                case 'A':
                    Console.WriteLine("(Enter 'Q')");
                    char userPressQ = Convert.ToChar(Console.ReadLine());
                    if (userPressQ == 'q' || userPressQ == 'Q') {
                        for (int hahaSpam = 0; hahaSpam < 10; hahaSpam++) {
                            Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxGAMExxxxxxxxxOVERxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                        }
                    }
                    else {
                    }
                    break;
                case 'b':
                case 'B':
                    Console.WriteLine("You try and get the neighbors to open thier door so you can escape from the unknown thing following you.");
                    Console.WriteLine("Unfortunately, it is too late and you feel a heavy tap on your right shoulder.");
                    break;
                case 'c':
                case 'C':
                    Console.WriteLine("\n\n\nYou pull out your [iPhone 34 Pro Max Ultra S] with your [airPods 39 Gen C Pros] and tune in to twitch.tv");
                    Console.WriteLine("Suddenly, you feel overwhelmingly hot.....");
                    break;
                default:
                    Console.WriteLine("You have entered an invalid answer.");
                    Environment.Exit(0);
                    break;
            }

            //END
            Console.WriteLine("You jolt awake, not realizing you had been asleep on your desk this whole time.\n");
            Console.WriteLine("But you get up from your chair, only to realize you aren't in your house anymore......");
            Console.WriteLine("(Enter 'R')");
            char userPressR = Convert.ToChar(Console.ReadLine());
            if (userPressR == 'r' || userPressR == 'R') {
                Environment.Exit(0);
            }
            else {
            }

        }

        public static int StoryPT3(string answerTQ) {
            int passFailNum;
            switch (answerTQ) {
                case "A":
                case "a":
                    Console.WriteLine("You quickly turn around and see this...");
                    passFailNum = 0;
                    return passFailNum;
                case "B":
                case "b":
                    Console.WriteLine("Although you have never climbed a tree before, you attempt climbing it.\n*CRACK* *CRACK*");
                    passFailNum = 1;
                    return passFailNum;
                case "C":
                case "c":
                    Console.WriteLine("You bolt to your neighbors house and furiously knock on the door.\n");
                    Console.WriteLine("*BOOM* *BOOM*\n");
                    passFailNum = 2;
                    return passFailNum;
                default:
                    Console.WriteLine("You have entered an invalid answer.");
                    passFailNum = -1;
                    return passFailNum;
            }
        }

        public static int StoryPT2(string answerTQ) {
            int passFailNum;
            switch (answerTQ) {
                case "A":
                case "a":
                    Console.WriteLine("You stumble around, trying to put your hand on the nearest light switch in your kitchen");
                    passFailNum = 0;
                    return passFailNum;
                case "B":
                case "b":
                    Console.WriteLine("As you swiftly open cabninets around the kitchen, you can't seem to find the flashlight that was there before...");
                    passFailNum = 1;
                    return passFailNum;
                case "C":
                case "c":
                    Console.WriteLine("Cmon..... -_-\nRETRY!!");
                    passFailNum = 2;
                    return passFailNum;
                default:
                    Console.WriteLine("You have entered an invalid answer.");
                    passFailNum = -1;
                    return passFailNum;
            }
        }
        public static int StoryBegin(string answerToQuestion) {
            int failOPass;
            switch (answerToQuestion) {
                case "A":
                case "a":
                    Console.WriteLine("You check the clock. The time is 7:30AM. Confused, you head downstairs to your kitchen.");
                    failOPass = 0;
                    return failOPass;
                case "B":
                case "b":
                    Console.WriteLine("WOOOOW very lazy.... -_-\n RETRY!!");
                    failOPass = 1;
                    return failOPass;
                case "C":
                case "c":
                    Console.WriteLine("You head downstairs to your kitchen.");
                    failOPass = 2;
                    return failOPass;
                default:
                    Console.WriteLine("You have entered an invalid answer.");
                    failOPass = -1;
                    return failOPass;
            }
        }
    }
}
