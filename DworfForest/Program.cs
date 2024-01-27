using System;
using System.Timers;

namespace DwarfForest {
    class Program {
        static void Main(string[] args) {
            UIBin elementsUI = new UIBin();
            PlayerSplashScreens splashScreen = new PlayerSplashScreens();
            Console.Clear();
            splashScreen.WelcomeScreen();
            if (splashScreen.GetPlayerPLAY() == false) { Environment.Exit(0); }
            else { }
            //-----   Create all entities     ---//
            WORLD MAP = new WORLD();   //Initialize world
            Debug("Loading world");
            MAP.SetMapSize(MAP.PlayerMapSize());
            MAP.FillMap();
            Debug("Setting world size");

            Dwarf PLAYER = new Dwarf();
            Debug("Loading player");

            PlayerHUD PLAYERstats = new PlayerHUD();
            PLAYERstats.SetWorldSize(MAP.GetMapSize());
            Debug("Loading player statistics");
            PLAYERstats.SetPlayerName();
            Debug("Loading player name");
            PLAYER.GeneratePlayerStart(MAP.GetMapSize());
            Debug("Loading player name");
            Debug("Loading HUD");

            Enemies enemy1 = new Enemies();
            MAP.SetFullMap(enemy1.EnemiesSpawn("Enemy1", MAP.GetFullMap(), PLAYER.GetPlayerX(), PLAYER.GetPlayerY()));
            Debug("Loading enemy_1");
            Enemies enemy2 = new Enemies();
            MAP.SetFullMap(enemy2.EnemiesSpawn("Enemy2", MAP.GetFullMap(), PLAYER.GetPlayerX(), PLAYER.GetPlayerY()));
            Debug("Loading enemy_2");
            Enemies enemy3 = new Enemies();
            MAP.SetFullMap(enemy3.EnemiesSpawn("Enemy3", MAP.GetFullMap(), PLAYER.GetPlayerX(), PLAYER.GetPlayerY()));
            Debug("Loading enemy_3");

            Console.Clear();
            Debug("Press 'T' to continue.");
            //-----   Instructions     ---//
            splashScreen.Instructions();

            Console.Clear();
            PLAYERstats.OutputHUDClr();
            
            MAP.PrintPLAYERView(PLAYER.GetPlayerX(), PLAYER.GetPlayerY());
            //-- The actual game loop --//
            ConsoleKey userInput = ConsoleKey.Spacebar;
            while (userInput != ConsoleKey.D2) {
                userInput = Console.ReadKey().Key;
                System.Threading.Thread.Sleep(10);
                if (userInput == ConsoleKey.W || userInput == ConsoleKey.A || userInput == ConsoleKey.S || userInput == ConsoleKey.D
                    || userInput == ConsoleKey.UpArrow || userInput == ConsoleKey.LeftArrow || userInput == ConsoleKey.DownArrow || userInput == ConsoleKey.RightArrow) {
                    if(userInput == ConsoleKey.W || userInput == ConsoleKey.A || userInput == ConsoleKey.S || userInput == ConsoleKey.D) { PLAYER.Move(userInput, MAP.GetFullMap()); }
                    if(userInput == ConsoleKey.UpArrow || userInput == ConsoleKey.LeftArrow || userInput == ConsoleKey.DownArrow || userInput == ConsoleKey.RightArrow) {
                        MAP.SetFullMap(PLAYER.BreakTree(userInput, MAP.GetFullMap())); 
                        MAP.SetFullMap(PLAYER.FightEnemy(userInput, MAP.GetFullMap(), PLAYERstats.GetPlayerName()));
                        PLAYERstats.SetPlayerEXP(PLAYER.GetPlayerEXP());
                        Console.Clear();     PLAYERstats.OutputHUDClr();
                        MAP.PrintPLAYERView(PLAYER.GetPlayerX(), PLAYER.GetPlayerY());
                        if (PLAYERstats.GetPlayerEXP() == 10) { splashScreen.LevelUp(); System.Threading.Thread.Sleep(5000); }
                        else { }
                    }
                }
                else { }
                PLAYERstats.SetPlayerEXP(PLAYER.GetPlayerEXP());
                PLAYERstats.OutputHUDClr();
                MAP.PrintPLAYERView(PLAYER.GetPlayerX(), PLAYER.GetPlayerY());
            }
        }
        static void Debug(string text) { Console.WriteLine(text + "..."); System.Threading.Thread.Sleep(100); }

    }
    class WORLD {
        int[,] worldMap;    //The FULL game map
        Random randNum = new Random();  //RNG
        UIBin elementsUI = new UIBin();   //Things for the UI
        int mapSize; int mapViewSize = 7;   //Map sizes
        int[,] worldMapMessed;  //The FULL map, but has boundaries
        int savedPX, savedPY;   //The saved numbers for player movement

        /*Setters and Getters for dynamic variables*/
        public int[,] GetFullMap() { return worldMap; }
        public void SetFullMap(int[,] newWM) { worldMap = newWM; }
        public int[,] GetMessedMap() { return worldMapMessed; }
        public int GetMapSize() { return mapSize; }
        public void SetMapSize(int userSz) { mapSize = userSz; worldMap = new int[mapSize, mapSize]; }
        public int GetSavedPX() { return savedPX; }
        public int GetSavedPY() { return savedPY; }
        /*Methods for functionality*/
        public int PlayerMapSize() {
            UIBin elementsUI = new UIBin();
            elementsUI.ColoredText("blue", "Enter a world size [Between 50 and 999]... ");
            int reply = Convert.ToInt32(Console.ReadLine());
            if (reply >= 50 && reply <= 999) { return reply; }
            else { Console.Write("Invlaid value, world size is set to 50...\n"); System.Threading.Thread.Sleep(750); return 50; }
        }
        public void FillMap() {
            for (int e = 0; e < worldMap.GetLength(0); e++) {
                for (int q = 0; q < worldMap.GetLength(1); q++) {
                    worldMap[e, q] = randNum.Next(1, 6);
                }
            }
        }
        public void PrintPLAYERView(int playerX, int playerY) {
            //Create messed map in order to accommodate for player viewer
            CreateMessedMAP();
            //Change player coords beucase of map change and save them
            playerX += (mapViewSize); playerY += (mapViewSize);
            SavePlayerX(playerX); SavePlayerY(playerY);
            //Display the map (player view) on the console
            for (int e = playerY - mapViewSize / 2; e <= playerY + mapViewSize / 2; e++) {
                for (int q = playerX - mapViewSize / 2; q <= playerX + mapViewSize / 2; q++) {
                    if (e != playerY || q != playerX) {
                        //switch statement
                        switch (worldMapMessed[e, q]) {
                            case (0):   //The void or enemy
                                elementsUI.ColoredText("red", "  .  ");
                                break;
                            case (1):   //Water
                                elementsUI.ColoredText("blueL", " ~~~ ");
                                break;
                            case (2):   //Tree
                                elementsUI.ColoredText("green", " {#} ");
                                break;
                            default:    //Ground
                                elementsUI.ColoredText("white", " ... ");
                                break;
                        }
                        //swtich statemnt
                    }
                    else {  //PLAYER
                        elementsUI.ColoredText("red", " -o- ");
                    }
                }
                Console.WriteLine();
            }
        }
        private void CreateMessedMAP() {
            //Create the worldMessed map, which is the size of the regular map plus two times the view count (safety)
            worldMapMessed = new int[worldMap.GetLength(0) + (mapViewSize * 2), worldMap.GetLength(1) + (mapViewSize * 2)];
            //Fill in all the same values from the old map in the middle of the new one
            for (int e = 0; e < worldMap.GetLength(0); e++) { for (int q = 0; q < worldMap.GetLength(1); q++) { worldMapMessed[e + mapViewSize, q + mapViewSize] = worldMap[e, q]; } }
            //If there are any null values, then replace them with 0.
            for (int e = 0; e < worldMapMessed.GetLength(0); e++) {
                for (int q = 0; q < worldMapMessed.GetLength(1); q++) {
                    if (worldMapMessed[e, q] != 1 && worldMapMessed[e, q] != 2 && worldMapMessed[e, q] != 3 && worldMapMessed[e, q] != 4 && worldMapMessed[e, q] != 5 && worldMapMessed[e, q] != 6) { worldMapMessed[e, q] = 0; }
                }
            }
        }
        private void SavePlayerX(int plyX) { savedPX = plyX; }
        private void SavePlayerY(int plyY) { savedPY = plyY; }

    }


    /// <summary>
    ///     BETWEEN     CLASSES
    /// </summary>


    class Dwarf {
        Random randNum = new Random();   //RNG
        int playerX, playerY;   //X and Y positions of player
        int expAdd; //EXP to add

        //---   Set or get player stuff   ---//
        public void SetPlayerX(int newX) { playerX = newX; }
        public void SetPlayerY(int newY) { playerY = newY; }
        public void SetPlayerEXP(int newEX) { expAdd = newEX; }
        public int GetPlayerX() { return playerX; }
        public int GetPlayerY() { return playerY; }
        public int GetPlayerEXP() { return expAdd; }
        //---   Player Movement   ---//
        public void Move(ConsoleKey uInput, int[,] worldMap) {
            UIBin uiBin = new UIBin();
            if (uInput == ConsoleKey.W) { playerY = MoveUP(worldMap, playerX, playerY, uiBin); }
            else if (uInput == ConsoleKey.A) { playerX = MoveLEFT(worldMap, playerX, playerY, uiBin); }
            else if (uInput == ConsoleKey.S) { playerY = MoveDOWN(worldMap, playerX, playerY, uiBin); }
            else if (uInput == ConsoleKey.D) { playerX = MoveRIGHT(worldMap, playerX, playerY, uiBin); }
            //** Functions for da movement **//
            static int MoveDOWN(int[,] worldMap, int playerX, int playerY, UIBin uiBin) {
                if (playerY + 1 < worldMap.GetLength(0)) {  //Checks to see if player is not going to go in the ranges of the array
                    if (worldMap[playerY + 1, playerX] == 0 || worldMap[playerY + 1, playerX] == 1 || worldMap[playerY + 1, playerX] == 2) { PlayerShake(uiBin); return playerY; }
                    else { return playerY + 1; }
                }
                else { PlayerShake(uiBin); return playerY; }
            }
            static int MoveUP(int[,] worldMap, int playerX, int playerY, UIBin uiBin) {
                if (playerY - 1 >= 0) {  //Checks to see if player is not going to go in the ranges of the array
                    if (worldMap[playerY - 1, playerX] == 0 || worldMap[playerY - 1, playerX] == 1 || worldMap[playerY - 1, playerX] == 2) { PlayerShake(uiBin); return playerY; }
                    else { return playerY - 1; }
                }
                else { PlayerShake(uiBin); return playerY; }
            }
            static int MoveRIGHT(int[,] worldMap, int playerX, int playerY, UIBin uiBin) {
                if (playerX + 1 < worldMap.GetLength(1)) {  //Checks to see if player is not going to go in the ranges of the array
                    if (worldMap[playerY, playerX + 1] == 0 || worldMap[playerY, playerX + 1] == 1 || worldMap[playerY, playerX + 1] == 2) { PlayerShake(uiBin); return playerX; }
                    else { return playerX + 1; }
                }
                else { PlayerShake(uiBin); return playerX; }
            }
            static int MoveLEFT(int[,] worldMap, int playerX, int playerY, UIBin uiBin) {
                if (playerX - 1 >= 0) {  //Checks to see if player is not going to go in the ranges of the array
                    if (worldMap[playerY, playerX - 1] == 0 || worldMap[playerY, playerX - 1] == 1 || worldMap[playerY, playerX - 1] == 2) { PlayerShake(uiBin); return playerX; }
                    else { return playerX - 1; }
                }
                else { PlayerShake(uiBin); return playerX; }
            }
            static void PlayerShake(UIBin uiBin) {
                int defaultCursorX = Console.CursorLeft;
                int defaultCursorY = Console.CursorTop;
                Console.SetCursorPosition(14, 6);
                uiBin.PlayerShakeL(); System.Threading.Thread.Sleep(25);
                Console.SetCursorPosition(14, 6);
                uiBin.PlayerShakeM(); System.Threading.Thread.Sleep(25);
                Console.SetCursorPosition(14, 6);
                uiBin.PlayerShakeR(); System.Threading.Thread.Sleep(25);
                Console.SetCursorPosition(defaultCursorX, defaultCursorY);
            }
        }
        public int[,] BreakTree(ConsoleKey uInput, int[,] worldMap) {
            UIBin uiBin = new UIBin();
            if (uInput == ConsoleKey.UpArrow) { return BreakUp(worldMap, playerX, playerY); }
            else if (uInput == ConsoleKey.LeftArrow) { return BreakLEFT(worldMap, playerX, playerY); }
            else if (uInput == ConsoleKey.DownArrow) { return BreakDown(worldMap, playerX, playerY); }
            else if (uInput == ConsoleKey.RightArrow) { return BreakRight(worldMap, playerX, playerY); }
            else { return worldMap; }
            //** Functions for da movement **//
            static int[,] BreakDown(int[,] worldMap, int playerX, int playerY) {
                if (worldMap[playerY + 1, playerX] == 2) { worldMap[playerY + 1, playerX] = 3; return worldMap; } //If there is a tree, replace with ground
                else { return worldMap; }
            }
            static int[,] BreakUp(int[,] worldMap, int playerX, int playerY) {
                if (worldMap[playerY - 1, playerX] == 2) { worldMap[playerY - 1, playerX] = 3; return worldMap; } //If there is a tree, replace with ground
                else { return worldMap; }
            }
            static int[,] BreakRight(int[,] worldMap, int playerX, int playerY) {
                if (worldMap[playerY, playerX + 1] == 2) { worldMap[playerY, playerX + 1] = 3; return worldMap; } //If there is a tree, replace with ground
                else { return worldMap; }
            }
            static int[,] BreakLEFT(int[,] worldMap, int playerX, int playerY) {
                if (worldMap[playerY, playerX - 1] == 2) { worldMap[playerY, playerX - 1] = 3; return worldMap; } //If there is a tree, replace with ground
                else { return worldMap; }
            }
        }
        public int[,] FightEnemy(ConsoleKey uInput, int[,] worldMap, string playerName) {
            UIBin uiBin = new UIBin();
            if (uInput == ConsoleKey.UpArrow) { return BreakUp(worldMap, playerX, playerY, playerName, expAdd); }
            else if (uInput == ConsoleKey.LeftArrow) { return BreakLEFT(worldMap, playerX, playerY, playerName, expAdd); }
            else if (uInput == ConsoleKey.DownArrow) { return BreakDown(worldMap, playerX, playerY, playerName, expAdd); }
            else if (uInput == ConsoleKey.RightArrow) { return BreakRight(worldMap, playerX, playerY, playerName, expAdd); }
            else { return worldMap; }
            //** Functions for da movement **//
            static int[,] BreakDown(int[,] worldMap, int playerX, int playerY, string playerName, int expAdd) {
                PlayerSplashScreens splashScrn = new PlayerSplashScreens();
                Enemies enemy = new Enemies();
                if (worldMap[playerY + 1, playerX] >= 50 && worldMap[playerY + 1, playerX] <= 52) {
                    if (worldMap[playerY + 1, playerX] == 50) { splashScrn.Enemy1(); expAdd = enemy.FightEnemy1(playerName); }
                    else if (worldMap[playerY + 1, playerX] == 51) { splashScrn.Enemy2(); expAdd = enemy.FightEnemy2(playerName); }
                    else if (worldMap[playerY + 1, playerX] == 52) { splashScrn.Enemy3(); expAdd = enemy.FightEnemy3(playerName); }
                    worldMap[playerY + 1, playerX] = 3; return worldMap; } //If there is enemy, delet
                else { return worldMap; }
            }
            static int[,] BreakUp(int[,] worldMap, int playerX, int playerY, string playerName, int expAdd) {
                PlayerSplashScreens splashScrn = new PlayerSplashScreens();
                Enemies enemy = new Enemies();
                if (worldMap[playerY - 1, playerX] >= 50 && worldMap[playerY - 1, playerX] <= 52) {
                    if (worldMap[playerY + 1, playerX] == 50) { splashScrn.Enemy1(); expAdd = enemy.FightEnemy1(playerName); }
                    else if (worldMap[playerY + 1, playerX] == 51) { splashScrn.Enemy2(); expAdd = enemy.FightEnemy2(playerName); }
                    else if (worldMap[playerY + 1, playerX] == 52) { splashScrn.Enemy3(); expAdd = enemy.FightEnemy3(playerName); }
                    worldMap[playerY - 1, playerX] = 3; return worldMap; } //If there is enemy, delet
                else { return worldMap; }
            }
            static int[,] BreakRight(int[,] worldMap, int playerX, int playerY, string playerName, int expAdd) {
                PlayerSplashScreens splashScrn = new PlayerSplashScreens();
                Enemies enemy = new Enemies();
                if (worldMap[playerY, playerX + 1] >= 50 && worldMap[playerY, playerX + 1] >= 52) {
                    if (worldMap[playerY + 1, playerX] == 50) { splashScrn.Enemy1(); expAdd = enemy.FightEnemy1(playerName); }
                    else if (worldMap[playerY + 1, playerX] == 51) { splashScrn.Enemy2(); expAdd = enemy.FightEnemy2(playerName); }
                    else if (worldMap[playerY + 1, playerX] == 52) { splashScrn.Enemy3(); expAdd = enemy.FightEnemy3(playerName); }
                    worldMap[playerY, playerX + 1] = 3; return worldMap; } //If there is enemy, delet
                else { return worldMap; }
            }
            static int[,] BreakLEFT(int[,] worldMap, int playerX, int playerY, string playerName, int expAdd) {
                PlayerSplashScreens splashScrn = new PlayerSplashScreens();
                Enemies enemy = new Enemies();
                if (worldMap[playerY, playerX - 1] >= 50 && worldMap[playerY, playerX - 1] >= 52) {
                    if (worldMap[playerY + 1, playerX] == 50) { splashScrn.Enemy1(); expAdd = enemy.FightEnemy1(playerName); }
                    else if (worldMap[playerY + 1, playerX] == 51) { splashScrn.Enemy2(); expAdd = enemy.FightEnemy2(playerName); }
                    else if (worldMap[playerY + 1, playerX] == 52) { splashScrn.Enemy3(); expAdd = enemy.FightEnemy3(playerName); }
                    worldMap[playerY, playerX - 1] = 3; return worldMap; } //If there is enemy, delet
                else { return worldMap; }
            }
        }

        //---   Generate player location (only at the beginning of the game)   ---//
        public void GeneratePlayerStart(int worldSize) { playerX = randNum.Next(0, worldSize); playerY = randNum.Next(0, worldSize); }
    }

    class Enemies {
        PlayerSplashScreens playerSplash = new PlayerSplashScreens();
        UIBin uiBin = new UIBin();
        Random randNum = new Random();
        int expToAdd = 0;
        int enX1, enY1;
        int enX2, enY2;
        int enX3, enY3;

        public int[,] EnemiesSpawn(string enemyName, int[,] wrldMap, int playerX, int playerY) {
            switch (enemyName) {
                case ("Enemy1"):    //Spawn enemy one
                    enX1 = randNum.Next(0, wrldMap.GetLength(1));
                    enY1 = randNum.Next(0, wrldMap.GetLength(0));
                    if (enX1 == playerX && enY1 == playerY) { //Checks if the enemy is on the same spot as player
                        enX1 = randNum.Next(0, wrldMap.GetLength(1));
                        enY1 = randNum.Next(0, wrldMap.GetLength(0));
                    } else { }
                    wrldMap[enY1, enX1] = 50;
                    return wrldMap;
                case ("Enemy2"):    //Spawn enemy two
                    enX2 = randNum.Next(0, wrldMap.GetLength(1));
                    enY2 = randNum.Next(0, wrldMap.GetLength(0));
                    if (enX2 == playerX && enY2 == playerY) { //Checks if the enemy is on the same spot as player
                        enX2 = randNum.Next(0, wrldMap.GetLength(1));
                        enY2 = randNum.Next(0, wrldMap.GetLength(0));
                    } else { }
                    wrldMap[enY2, enX2] = 51;
                    return wrldMap;
                case ("Enemy3"):    //Spawn enemy three
                    enX3 = randNum.Next(0, wrldMap.GetLength(1));
                    enY3 = randNum.Next(0, wrldMap.GetLength(0));
                    if (enX3 == playerX && enY3 == playerY) { //Checks if the enemy is on the same spot as player
                        enX3 = randNum.Next(0, wrldMap.GetLength(1));
                        enY3 = randNum.Next(0, wrldMap.GetLength(0));
                    } else { }
                    wrldMap[enY3, enX3] = 52;
                    return wrldMap;
                default:
                    return wrldMap;
            }
            }

            public int FightEnemy1(string playerName) {
                Console.Clear();    playerSplash.Enemy1();
                Console.Clear();    playerSplash.Enemy1();
                expToAdd = ahhENEMY(playerName);
                return expToAdd;
        }
        public int FightEnemy2(string playerName) {
            Console.Clear();    playerSplash.Enemy2();
            Console.Clear();    playerSplash.Enemy2();
            expToAdd = ahhENEMY(playerName);
            return expToAdd;
        }
        public int FightEnemy3(string playerName) {
            Console.Clear();    playerSplash.Enemy3();
            Console.Clear();    playerSplash.Enemy3();
            expToAdd = ahhENEMY(playerName);
            return expToAdd;
        }
        private int ahhENEMY(string playerName) {
            Console.SetCursorPosition(45, 5);  uiBin.ColoredText("yellow", playerName + "- ''OH GOD OH NO AN ENEMY!!!!''");
            System.Threading.Thread.Sleep(100);
            Console.SetCursorPosition(45, 11);  uiBin.ColoredText("yellowD", "(!) Keep pressing 'Q' until the damage");
            Console.SetCursorPosition(45, 12);  uiBin.ColoredText("yellowD", "    bar is full!");
            Console.SetCursorPosition(45, 13);  uiBin.ColoredText("yellowD", "(!) Remember, pressing 'Q' too much");
            Console.SetCursorPosition(45, 14);  uiBin.ColoredText("yellowD", "     will make you faint. ");
            int playerSpamQ = 0, tempCurrentCursorPos = 50;
            uiBin.ColoredText("blueBLOCK", "");
            while (playerSpamQ < 7) {
                Console.SetCursorPosition(tempCurrentCursorPos, 16);
                if (Console.ReadKey().Key == ConsoleKey.Q) {
                    playerSpamQ++;  tempCurrentCursorPos += 4;
                    uiBin.BlockedColor("blueBLOCK", playerSpamQ + "  ");
                    Console.SetCursorPosition(tempCurrentCursorPos, 16);
                }
                else { }
                }
            Console.SetCursorPosition(45, 18);
            uiBin.BlockedColor("black", "YOU DID IT! +3EXP");
            System.Threading.Thread.Sleep(1500);
            return 3;
            }
            static void AwwwLoss(UIBin uiBin) { uiBin.ColoredText("red", "you have lost the game and all data has been deleted...."); }

        }



    /// <summary>
    ///     BETWEEN     CLASSES
    /// </summary>

    class UIBin {
        public void ColoredText(string color, string text) {
            switch (color) {
                case ("green"):
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ("greenD"):
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ("blue"):
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ("blueL"):
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ("red"):
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ("yellow"):
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ("gray"):
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(text);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ("white"):
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(text);
                    break;
            }
        } //Method that changes text color
        public void BlockedColor(string color, string text) {
            switch (color) {
                case ("green"):
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(text);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case ("greenD"):
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write(text);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case ("blue"):
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write(text);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case ("blueBLOCK"):
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(text);
                    break;
                case ("blueL"):
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Write(text);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case ("red"):
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(text);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case ("yellow"):
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(text);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case ("yellowD"):
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Write(text);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case ("gray"):
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(text);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case ("grayD"):
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(text);
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case ("black"):
                default:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(text);
                    break;
            }
        } //Method that changes text color
        //---   Shake Player Animation    ---//
        public void PlayerShakeM() { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("  -o- "); Console.ForegroundColor = ConsoleColor.White; }
        public void PlayerShakeR() { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("   -o- "); Console.ForegroundColor = ConsoleColor.White; }
        public void PlayerShakeL() { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(" -o- "); Console.ForegroundColor = ConsoleColor.White; }
        //---   Sample Text    ---//

    }
    class PlayerHUD {
        UIBin uiElmnt = new UIBin();
        int worldSize = 50;
        public void SetWorldSize(int newWS) { worldSize = newWS; }
        /*This should be the info for a small box which displays the current stats of the player.*/
        int playerHP = 100;
        public void SetPlayerHP(int newPHP) { playerHP = newPHP; }
        public int GetPlayerHP() { return playerHP; }

        int playerEXP = 0;
        public void SetPlayerEXP(int newPEXP) { playerEXP = newPEXP; }
        public int GetPlayerEXP() { return playerEXP; }

        string playerName = "Player";
        public void SetPlayerName() { uiElmnt.ColoredText("blue", "Enter your name: "); playerName = Console.ReadLine(); }
        public string GetPlayerName() { return playerName; }

        /*Output HUD*/
        public void OutputHUDClr() {
            Console.Clear();
            Console.Write("[ ");   //Top Left Corner
            uiElmnt.ColoredText("green", "    " + playerName);  //Print Name
            uiElmnt.ColoredText("greenD", "     HP: " + playerHP);  //Print HP
            uiElmnt.ColoredText("greenD", "     EXP: " + playerEXP);  //Print EXP
            Console.SetCursorPosition(playerName.Length + 33, 0);   //Top Right corner
            Console.WriteLine(" ]");
            for (int i = 0; i < playerName.Length + 35; i++) { Console.Write("'"); } //Split Bar
            Console.SetCursorPosition((playerName.Length + 23) / 2, 1);
            string spaceDyn = "  ";
            if (worldSize > 10) { spaceDyn = " "; } else { }
            uiElmnt.ColoredText("blue", spaceDyn + "MAP: " + Convert.ToString(worldSize) + "x" + Convert.ToString(worldSize) + spaceDyn);
            Console.WriteLine();    //For my sanity
            Console.SetCursorPosition(0, 3);    //Cursor under HUD
        }

    }
    class PlayerSplashScreens {
        bool ContinueGame = true;
        public void WelcomeScreen() {
            UIBin uiElmnts = new UIBin();
            uiElmnts.ColoredText("white", " Please do not change the window size.");
            /*ASCii art*/
            Console.SetCursorPosition(0, 3);
            uiElmnts.ColoredText("white", "           ^^                   ");
            uiElmnts.ColoredText("yellow", "@@@@@@@@@\n");
            uiElmnts.ColoredText("white", "      ^^       ^^            ");
            uiElmnts.ColoredText("yellow", "@@@@@@@@@@@@@@@\n");
            uiElmnts.ColoredText("yellow", "                           @@@@@@@@@@@@@@@@@@              ");
            uiElmnts.ColoredText("white", "^^\n");
            uiElmnts.ColoredText("yellow", "                          @@@@@@@@@@@@@@@@@@@@\n");
            uiElmnts.ColoredText("blueL", "~~~~ ~~ ~~~~~ ~~~~~~~~ ~~ ");
            uiElmnts.ColoredText("yellow", "&&&&&&&&&&&&&&&&&&&& ");
            uiElmnts.ColoredText("blueL", "~~~~~~~ ~~~~~~~~~~~ ~~~\n");
            uiElmnts.ColoredText("blueL", "~         ~~   ~  ~       ~~~~~~~~~~~~~~~~~~~~ ~       ~~     ~~ ~\n" +
                "  ~      ~~      ~~ ~~ ~~  ~~~~~~~~~~~~~ ~~~~  ~     ~~~    ~ ~~~  ~ ~~ \n" +
                "  ~  ~~     ~         ~      ~~~~~~  ~~ ~~~       ~~ ~ ~~  ~~ ~ \n" +
                "~  ~       ~ ~      ~           ~~ ~~~~~~  ~      ~~  ~             ~~\n" +
                "      ~             ~        ~      ~      ~~   ~             ~");
            /*End of ASCii art*/
            //START
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, 1);
            uiElmnts.ColoredText("white", "[");
            uiElmnts.ColoredText("green", "1");
            uiElmnts.ColoredText("white", "]");
            uiElmnts.ColoredText("white", " PLAY GAME!");
            //QUIT
            Console.SetCursorPosition(Console.WindowWidth / 2 - 5, 2);
            uiElmnts.ColoredText("white", "[");
            uiElmnts.ColoredText("red", "2");
            uiElmnts.ColoredText("white", "]");
            uiElmnts.ColoredText("white", " leave...");
            //ASK
            Console.SetCursorPosition(0, 12);
            uiElmnts.ColoredText("white", "[");
            Console.SetCursorPosition(2, 12);
            uiElmnts.ColoredText("white", "]");
            Console.SetCursorPosition(1, 12);
            ConsoleKey userReply;
            while (true) {
                userReply = Console.ReadKey().Key;
                if (userReply == ConsoleKey.D1) { uiElmnts.ColoredText("white", "]"); Console.WriteLine(); break; }
                else if (userReply == ConsoleKey.D2) { uiElmnts.ColoredText("white", "]"); ContinueGame = false; break; }
                else { uiElmnts.ColoredText("white", "]"); uiElmnts.ColoredText("red", "\nINVALID "); uiElmnts.ColoredText("white", "["); }
            }
        }
        public bool GetPlayerPLAY() { return ContinueGame; }
    public void Instructions() {
            UIBin uiElmnts = new UIBin();
            while (Console.ReadKey().Key != ConsoleKey.E) {
                uiElmnts.ColoredText("yellow", "                                        WELCOME TO DWORF FOREST!\n");
                uiElmnts.ColoredText("yellow", "Before we begin, it is important that you read the instructions and controls for the game.\n");
                uiElmnts.ColoredText("yellowD", "-   -   -   -   -   -   -   -   -   -   -\n");
                uiElmnts.ColoredText("yellowD", "If you would like to change the size of the console window, now would be the time to do so.\n");
                uiElmnts.ColoredText("yellow", "\nCONTROLS\n ----------  \n");
                uiElmnts.ColoredText("yellowD", "\n(WASD) - Move your character\n(Left Arrow Key) - Break tree to your left.\n");
                uiElmnts.ColoredText("yellowD", "(Right Arrow Key) - Break tree to your right.\n(Up Arrow Key) - Break tree above you.\n");
                uiElmnts.ColoredText("yellowD", "(Down Arrow Key) - Break tree below you.\n");
                uiElmnts.ColoredText("red", "  .     < This is an enemy.\n");
                uiElmnts.ColoredText("yellowD", "(Left Arrow Key) - Interact with enemy to your left.\n(Right Arrow Key) - Interact with enemy to your right.\n");
                uiElmnts.ColoredText("yellowD", "(Up Arrow Key) - Interact with enemy above you.\n(Down Arrow Key) - Interact with enemy below you.\n");

                uiElmnts.ColoredText("yellow", "\n\n If you understand and are ready to start, press 'E' now.");
            }
        }
        public void Enemy1() {
            UIBin uiElmnts = new UIBin();
            uiElmnts.ColoredText("red", "              __.......__\n            .-:::::::::::::-.\n          .:::''':::::::''':::.\n        .:::'     `:::'     `:::. \n   .'\\  ::'   ^^^  `:'  ^^^   '::  /`.\n" +
                "  :   \\ ::   _.__       __._   :: /   ;\n :     \\`: .' ___\\     /___ `. :'/     ; \n:       /\\   (_|_)\\   /(_|_)   /\\       ;\n:      / .\\   __.' ) ( `.__   /. \\      ;\n" +
                ":      \\ (        {   }        ) /      ; \n :      `-(     .  ^'' ^  .     ) - '      ;\n  `.       \\  .'<`-._.-'>'.  /       .'\n    `.      \\    \\;`.';/    /      .'\n      `._    `-._       _.-'    _.\n" +
                "       .'`-.__ .'`-._.-'`. __.-'`.\n     .'       `.         .'       `.\n'   .'           `-.   .-'           `.\n");
        }
        public void Enemy2() {
            UIBin uiElmnts = new UIBin();
            uiElmnts.ColoredText("red", "      -. -. `.  / .-' _.'  _\n     .--`. `. `| / __.-- _' `\n    '.-.  \\  \\ |  /   _.' `_\n    .-. \\  `  || |  .' _.-' `.\n" +
                "  .' _ \\ '  -    -'  - ` _.-.\n   .' `. %%%%%   | %%%%% _.-.`-\n .' .-. ><(@)> ) ( <(@)>< .-.`.\n   ((''`(-   | | -   )'''))\n" +
                "  / \\#)\\    (.(_).)    /(#//\\\n ' / ) ((  /   | |   \\  )) (`.`.\n .'  (.) \\ .md88o88bm. / (.) \\)\n   / /| / \\ `Y88888Y' / \\ | \\ \\\n .' / O  / `.   -   .' \\  O \\ \\\\" +
                "\n  / / (O) / /| `.___.' | \\(O) \\\n   / / / / |  |   |  |\\  \\  \\ \\\n   / / // /|  |   |  |  \\  \\ \\    \n _.-- / --/ '( ) ) ( ) ) )`\\-\\-\\-._\n((( )())())()) ) ( ) )\n");
        }
        public void Enemy3() {
            UIBin uiElmnts = new UIBin();
            uiElmnts.ColoredText("red", "        .-''''''''.\n       /       \\\n   __ /   .-.  .\\\n  /  `\\  /   \\/  \\\n  | _ \\/   .==.==.\n  | (   \\  / ____\\__\\\n   \\ \\      (_()(_()\n    \\ \\            '---._\n     \\                   \\_\n" +
                "  /\\ |`       (__)________ /\n /  \\|     /\\___ /\n|    \\     \\|| VV\n|     \\     \\| '''''''',\n|      \\     ______)\n\\       \\  /`\n         \\(");
        }
        public void LevelUp() {
            /*When player has been upgraded to level 2!*/
            UIBin uiElmnts = new UIBin();
            uiElmnts.ColoredText("yellow", "  _        ______  __      __  ______   _      \n | |      |  ____| \\ \\    / / |  ____| | |     \n | |      | |__     \\ \\  / /  | |__    | |     \n | |      |  __|     \\ \\/ /   |  __|   | |     \n" +
                " | |____  | |____     \\  /    | |____  | |____ \n |______| |______|     \\/     |______| |______|\n\n\n          _    _   _____      _                \n         | |  | | |  __ \\    | |\n         | |  | | | |__) |   | |\n" +
                "         | |  | | |  ___/    | | \n         | |__| | | |        |_|\n          \\____/  |_|        (_)  ");
        }

    }

}
