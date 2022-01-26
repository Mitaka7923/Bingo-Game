using System;
using System.IO;
using System.Text;
using System.Threading;
using static System.Console;

namespace BingoGame
{
    class Game
    {
        public int credits = 0;
        public int houseCredits = 50;
        private int workedCR = 1;
        private int count = 0;
        //private int[] bingoBall = new int[5];
        private int userBet = 0;
        private int randomNum = 0;
        private int counter;
        private string[] options;
        private string prompt;
        private bool isSecret = false;
        private string bingoName = "Bingo Anel";
        private const string waiting = ".";
        private const string creator = "Mitaka7923";
        private const string playerCreditsPath = "PlayerCredits.txt";
        private const string houseCreditsPath = "HouseCredits.txt";
        private const string isSecretPath = "IsSecret.txt";
        private const int multiplier = 2;

        //TODO: Save 'playerCredits','houseCredits' and 'isSecret' in one method    -----HARD (think)
        //TODO: Optimize the methods and the classes - too many methods in 'Game' class
           
        public void Start()
        {
            Title = "БИНГО - ИГРАТА!";
            //BingoInside();
            //BingoTestNums();    // CAlll bingoballs and userguesses
            FileCheck();
            SetCreditsFromFile();
            SetSecretFromFile();
            SetHouseCreditsFromFIle();
            RunMainMenu();
        }

        //TODO: Continue this logic about the bingo algorithm
        //private void BingoTestNums(int[] bingoBalls, int[] userNumbers)
        //{
        //    int guess = 0;
        //    int counter = 0;
        //    for (int i = 0; i < 10; i++)
        //    {
        //        if (bingoBalls[i] == userNumbers[i + 1])
        //        {
        //            if (bingoBalls[i] == bingoBalls.Length)
        //            {
        //                bingoBalls[i] = 0;
        //                counter++;
        //                if (counter == 2)
        //                {
        //                    break;
        //                }
        //            }
        //            guess++;
        //        }
        //    }
        //}

        //private void BingoInside()
        //{
        //    Clear();
        //    Random bingoBallNum = new Random();
        //    for (int i = 0; i < 5; i++)
        //    {
        //        bingoBall[i] = bingoBallNum.Next(1, 75);
        //    }
        //}

        private void FileCheck()
        {
            if (!File.Exists(playerCreditsPath))
            {
                using (StreamWriter createFile = File.CreateText(playerCreditsPath)) { }
                SetInitialCreditsValue();
            }
            if (!File.Exists(isSecretPath))
            {
                using (StreamWriter createFile = File.CreateText(isSecretPath)) { }
                SetSecretValue();
            }
            if (!File.Exists(houseCreditsPath))
            {
                using (StreamWriter createFile = File.CreateText(houseCreditsPath)) { }
                SetInitialHouseCreditsValue();
            }
            FileIsEmpty();
        }

        private void FileIsEmpty()
        {
            bool isCreditsEmpty = new FileInfo(playerCreditsPath).Length == 0;
            bool isHouseCreditsEmpty = new FileInfo(houseCreditsPath).Length == 0;
            bool isSecretEmpty = new FileInfo(isSecretPath).Length == 0;
            if (isCreditsEmpty)
                SetInitialCreditsValue();
            if (isHouseCreditsEmpty)
                SetInitialHouseCreditsValue();
            if (isSecretEmpty)
                SetSecretValue();
        }

        private void SetInitialCreditsValue()
        {
            File.WriteAllText(playerCreditsPath, "20");    
        }

        private void SetInitialHouseCreditsValue()
        {
            File.WriteAllText(houseCreditsPath, "50");
        }

        private void SetSecretValue()
        {
            if (!isSecret)
            {
                File.WriteAllText(isSecretPath, "false");
            }
            else
            {
                File.WriteAllText(isSecretPath, "true");
            }
        }

        private void SetCreditsFromFile()
        {
            credits = Convert.ToInt32(File.ReadAllText(playerCreditsPath));
        }

        private void SetHouseCreditsFromFIle()
        {
            houseCredits = Convert.ToInt32(File.ReadAllText(houseCreditsPath));
        }

        private void SaveHouseCredits()
        {
            File.WriteAllText(houseCreditsPath, Convert.ToString(houseCredits));
        }

        private void SetSecretFromFile()
        {
            bool isSecretEmpty = new FileInfo(isSecretPath).Length == 0;    // TODO: Optimize multiple usings of this line
            if (isSecretEmpty)
                SetSecretValue();
            string readSecret = File.ReadAllText(isSecretPath);
            if (readSecret == "true")   // -> Here we don't have an else statement because the default value of the isSecret is 'false'
                isSecret = true;
        }

        private void SaveCreditValue()
        {
            File.WriteAllText(playerCreditsPath, Convert.ToString(credits));
        }

        private void RunMainMenu()
        {
            ForegroundColor = ConsoleColor.White;
            CursorVisible = false;
            OutputEncoding = Encoding.Unicode;
            if(!isSecret)
            {
                prompt = @$"                                                                 

                      ██████╗ ██╗███╗   ██╗ ██████╗  ██████╗      █████╗ ███╗   ██╗███████╗██╗     
                      ██╔══██╗██║████╗  ██║██╔════╝ ██╔═══██╗    ██╔══██╗████╗  ██║██╔════╝██║     
                      ██████╔╝██║██╔██╗ ██║██║  ███╗██║   ██║    ███████║██╔██╗ ██║█████╗  ██║     
                      ██╔══██╗██║██║╚██╗██║██║   ██║██║   ██║    ██╔══██║██║╚██╗██║██╔══╝  ██║     
                      ██████╔╝██║██║ ╚████║╚██████╔╝╚██████╔╝    ██║  ██║██║ ╚████║███████╗███████╗
                      ╚═════╝ ╚═╝╚═╝  ╚═══╝ ╚═════╝  ╚═════╝     ╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝
                                       

          ДОБРЕ ДОШЛИ в БИНГОТО! Имате невероятната възможност да спечелите кредити! Започвате с 20 CR подарък.
                        (Използвай стрелките за да СЕЛЕКТИРАШ опция и ENTER за да я ИЗБЕРЕШ)" + Environment.NewLine + Environment.NewLine;
            }
            else
            {
                prompt = @$"                                                                 
                      ██████╗ ██╗███╗   ██╗ ██████╗  ██████╗      █████╗ ███╗   ██╗ █████╗ ██╗     
                      ██╔══██╗██║████╗  ██║██╔════╝ ██╔═══██╗    ██╔══██╗████╗  ██║██╔══██╗██║     
                      ██████╔╝██║██╔██╗ ██║██║  ███╗██║   ██║    ███████║██╔██╗ ██║███████║██║     
                      ██╔══██╗██║██║╚██╗██║██║   ██║██║   ██║    ██╔══██║██║╚██╗██║██╔══██║██║     
                      ██████╔╝██║██║ ╚████║╚██████╔╝╚██████╔╝    ██║  ██║██║ ╚████║██║  ██║███████╗
                      ╚═════╝ ╚═╝╚═╝  ╚═══╝ ╚═════╝  ╚═════╝     ╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚══════╝
                                       

          ДОБРЕ ДОШЛИ в БИНГОТО! Имате невероятната възможност да спечелите кредити! Започвате с 20 CR подарък.
                        (Използвай стрелките за да СЕЛЕКТИРАШ опция и ENTER за да я ИЗБЕРЕШ)" + Environment.NewLine + Environment.NewLine;
            }
            string[] options = { "Вход", "Информация", "Работа", "Изход от залата" };
            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Beep();
                    RunFirstChoise();
                    break;
                case 1:
                    Beep();
                    DisplayAboutInfo();
                    break;
                case 2:
                    WorkCredits();
                    break;
                case 3:
                    Beep();
                    ExitGame();
                    break;
            }
        }

        private void DisplayAboutInfo()
        {
            if (!isSecret)
            {
                Clear();
                ForegroundColor = ConsoleColor.White;
                Write("Тази игра е направена от ");
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"{creator}.");
                ForegroundColor = ConsoleColor.White;
                WriteLine("Използва картинки от https://patorjk.com/software/taag");
                WriteLine("Това все още не е пълната игра...ще има още. Бингото е все още в разработка.");
                Write($"{Environment.NewLine}Баланс на кредити: ");
                ForegroundColor = ConsoleColor.DarkYellow;
                Write($"{credits} CR");
                ForegroundColor = ConsoleColor.White;
                if (credits < 0)
                {
                    ForegroundColor = ConsoleColor.Red;
                    Write(" (на лъжата краката са къси...)");
                    ForegroundColor = ConsoleColor.White;
                }
                WriteLine($"{Environment.NewLine}Натисни някое копче за да се върнеш към менюто...");
                ReadKey(true);
                RunMainMenu();
            }
            else
            {
                Clear();
                ForegroundColor = ConsoleColor.White;
                Write("Тази игра е направена от ");
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"{creator}.");
                ForegroundColor = ConsoleColor.White;
                WriteLine("Използва картинки от https://patorjk.com/software/taag");
                WriteLine("Това все още не е пълната игра...ще има още. Бингото е все още в разработка.");
                Write($"{Environment.NewLine}Баланс на хазна: ");
                ForegroundColor = ConsoleColor.DarkYellow;
                WriteLine($"{houseCredits} CR");
                ForegroundColor = ConsoleColor.White;
                if (credits >= 0)
                {
                    Write("Баланс на кредити: ");
                    ForegroundColor = ConsoleColor.DarkYellow;
                    WriteLine($"{credits} CR");
                    ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Write("Баланс на кредити: ");
                    ForegroundColor = ConsoleColor.DarkYellow;
                    Write($"{credits} CR");
                    ForegroundColor = ConsoleColor.White;
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine(" (на лъжата краката са къси...)");
                    ForegroundColor = ConsoleColor.White;
                }
                WriteLine("Натисни някое копче за да се върнеш към менюто...");
                ReadKey(true);
                RunMainMenu();
            }
        }

        private void WorkCredits()
        {
            while (true)
            {
                count = 0;
                Clear();
                CreditCounter();

                Write("За колко CR искаш да работиш сега: ");
                ForegroundColor = ConsoleColor.Cyan;
                int.TryParse(ReadLine(), out int workCredits);
                ForegroundColor = ConsoleColor.White;
                while (true)
                {
                    workedCR = 1;
                    if (isSecret)
                    {
                        workedCR = 2;
                    }
                    Clear();
                    string genStr = Guid.NewGuid().ToString("n").Substring(0, 8);

                    if (count == workCredits)
                    {
                        Clear();
                        ForegroundColor = ConsoleColor.White;
                        WriteLine("Имате достатъчно кредити! Можете да отидете да играете...");
                        Thread.Sleep(2500);
                        RunMainMenu();
                    }
                    Clear();

                    CreditCounter();

                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine("Имаме задача да освободим символите от клетката. За целта трябва да ги изкараш един по един точно. ");
                    Write($"Ще спечелиш ");
                    ForegroundColor = ConsoleColor.DarkYellow;
                    Write($"{workedCR} CR");
                    ForegroundColor = ConsoleColor.DarkGray;
                    WriteLine($", ако ги освободиш.{Environment.NewLine}");
                    Write(@"Напиши ");
                    ForegroundColor = ConsoleColor.DarkGreen;
                    Write(@"""leave""");
                    ForegroundColor = ConsoleColor.DarkGray;
                    Write(" за отказ.");
                    ForegroundColor = ConsoleColor.White;
                    Write($"{Environment.NewLine}Заключените символи: ");
                    ForegroundColor = ConsoleColor.Cyan;
                    WriteLine($"{genStr}");
                    ForegroundColor = ConsoleColor.White;
                    Write("В процес на освобождаване... ");
                    ForegroundColor = ConsoleColor.DarkCyan;
                    string inputStr = ReadLine();
                    ForegroundColor = ConsoleColor.White;

                    if (inputStr == genStr)
                    {
                        count++;
                        credits += workedCR;

                        Write($"Ти си изработи ");
                        ForegroundColor = ConsoleColor.DarkYellow;
                        WriteLine($"{workedCR} CR{Environment.NewLine}");
                        ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(1500);
                    }
                    else if (inputStr == "secret menu")
                    {
                        isSecret = true;
                        SetSecretValue();
                        ForegroundColor = ConsoleColor.DarkMagenta;
                        WaitForResult();
                        WriteLine("сте секретни - напишете [exit secret menu], за да го махнете. < -------------- > SHHHHHH.....");
                        ForegroundColor = ConsoleColor.Gray;
                        Thread.Sleep(3500);
                        RunMainMenu();
                    }
                    else if (inputStr == "exit secret menu")
                    {
                        isSecret = false;
                        SetSecretValue();
                        ForegroundColor = ConsoleColor.DarkMagenta;
                        WaitForResult();
                        ForegroundColor = ConsoleColor.Gray;
                        Write("SHHH..");
                        Thread.Sleep(1000);
                        RunMainMenu();
                    }
                    else if (inputStr == "leave")
                    {
                        ForegroundColor = ConsoleColor.Cyan;
                        WriteLine($"{Environment.NewLine}Ти се отказа...връщане назад.");
                        Thread.Sleep(1500);
                        RunMainMenu();
                    }
                    else
                    {
                        ForegroundColor = ConsoleColor.DarkRed;
                        Write($"{Environment.NewLine}You FAILED...try again.");
                        Thread.Sleep(1500);
                        Clear();
                        ForegroundColor = ConsoleColor.Gray;
                        continue;
                    }
                    SaveCreditValue();
                    continue;
                }
            }
        }

        private void RunFirstChoise()
        {
            count = 0;
            if (!isSecret)
            {
                prompt = @"  

                      ██████╗ ██╗███╗   ██╗ ██████╗  ██████╗      █████╗ ███╗   ██╗███████╗██╗     
                      ██╔══██╗██║████╗  ██║██╔════╝ ██╔═══██╗    ██╔══██╗████╗  ██║██╔════╝██║     
                      ██████╔╝██║██╔██╗ ██║██║  ███╗██║   ██║    ███████║██╔██╗ ██║█████╗  ██║     
                      ██╔══██╗██║██║╚██╗██║██║   ██║██║   ██║    ██╔══██║██║╚██╗██║██╔══╝  ██║     
                      ██████╔╝██║██║ ╚████║╚██████╔╝╚██████╔╝    ██║  ██║██║ ╚████║███████╗███████╗
                      ╚═════╝ ╚═╝╚═╝  ╚═══╝ ╚═════╝  ╚═════╝     ╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝


На какво ще играете?";
            }
            else
            {
                prompt = @$"                                                                 
                      ██████╗ ██╗███╗   ██╗ ██████╗  ██████╗      █████╗ ███╗   ██╗ █████╗ ██╗     
                      ██╔══██╗██║████╗  ██║██╔════╝ ██╔═══██╗    ██╔══██╗████╗  ██║██╔══██╗██║     
                      ██████╔╝██║██╔██╗ ██║██║  ███╗██║   ██║    ███████║██╔██╗ ██║███████║██║     
                      ██╔══██╗██║██║╚██╗██║██║   ██║██║   ██║    ██╔══██║██║╚██╗██║██╔══██║██║     
                      ██████╔╝██║██║ ╚████║╚██████╔╝╚██████╔╝    ██║  ██║██║ ╚████║██║  ██║███████╗
                      ╚═════╝ ╚═╝╚═╝  ╚═══╝ ╚═════╝  ╚═════╝     ╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚══════╝
 

На какво ще играете?";
            }
            string[] options = { "Познай числото", "БИНГО", "Назад" };
            Menu miniGameMenu = new Menu(prompt, options);
            int selectedIndex = miniGameMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    GuessingGame();
                    break;
                case 1:
                    BingoGame();
                    break;
                case 2:
                    RunMainMenu();
                    break;
            }
        }

        private void GuessingGame()
        {
            Clear();
            ForegroundColor = ConsoleColor.Gray;
            prompt = @"
              
                               ██████╗ ██╗   ██╗███████╗███████╗███████╗    ██╗████████╗
                              ██╔════╝ ██║   ██║██╔════╝██╔════╝██╔════╝    ██║╚══██╔══╝
                              ██║  ███╗██║   ██║█████╗  ███████╗███████╗    ██║   ██║   
                              ██║   ██║██║   ██║██╔══╝  ╚════██║╚════██║    ██║   ██║   
                              ╚██████╔╝╚██████╔╝███████╗███████║███████║    ██║   ██║   
                               ╚═════╝  ╚═════╝ ╚══════╝╚══════╝╚══════╝    ╚═╝   ╚═╝   
                                                        
ЗДРАВЕЙТЕ! ТОВА Е ПОЗНАЙ ЧИСЛОТО!
- ЧИСЛО OT ЕДНО ДО ДЕСЕТ (1-10) ЩЕ БЪДЕ ИЗТЕГЛЕНО ПРОИЗВОЛНО И ВИЕ ЩЕ ТРЯБВА ДА ГО ПОЗНАЕТЕ ТОЧНО. 
  АКО ГО ПОЗНАЕТЕ ПЕЧЕЛИТЕ 5 КРЕДИТА.
- ЗА ДА ИГРАЕШ ЩЕ ИМАШ НУЖДА ОТ 2 КРЕДИТА.
  (ИЗПОЛЗВАЙ клавиатурата с цифрите за да се опиташ да го познаеш)" + Environment.NewLine + Environment.NewLine + "Готови ли сте?";


            string[] options = { "Да", "Не", "Изход от залата" };
            Menu guessMenu = new Menu(prompt, options);
            int selectedIndex = guessMenu.Run();

            if (selectedIndex == 0)
            {
                while (true)
                {
                    ChargeCreditsGuess();
                    Clear();
                    Write("Въведете число: ");
                    ForegroundColor = ConsoleColor.Cyan;
                    string userInput = ReadLine();
                    ForegroundColor = ConsoleColor.Gray;
                    bool parsed = int.TryParse(userInput, out int parsedUserInput);
                    if (parsed != true || parsedUserInput is < 0 || parsedUserInput > 10)
                    {
                        count++;
                        Clear();
                        if (count > 3)
                            ManyInvalidNumbers();
                        else
                            InvalidNumber();
                        continue;
                    }
                    else
                    {
                        Random rnd = new Random();
                        ForegroundColor = ConsoleColor.Cyan;
                        randomNum = rnd.Next(1, 10);
                        ForegroundColor = ConsoleColor.White;
                        Write("Вие");
                        WaitForResult();
                        if (parsedUserInput != randomNum)
                        {
                            WrongGuessMenu();
                            if (selectedIndex == 0)
                                continue;
                        }
                        else
                        {
                            CorrectGuessMenu();
                            if (selectedIndex == 0)
                                continue;
                        }
                    }
                    break;
                }
            }
            else if (selectedIndex == 1)
            {
                ForegroundColor = ConsoleColor.Yellow;
                Write($"{Environment.NewLine}Явно не сме готови, нека се върнем към началото");
                Thread.Sleep(1200);
                RunMainMenu();
            }
            else if (selectedIndex == 2)
                ExitGame();
        }

        private void BingoGame()
        ////TODO FEATURES:
        /// Bingo algorithm, random sequence of numbers generated by the computer at a set time
        /// Each integer to 56 belongs to a numbered bingo ball (ascii drawings)    ----HARD

        ////TODO WININGS:
        /// Minimum 4 numbers guessed for a profit
        /// 1 number - lost the bet
        /// 2 numbers - 30% of the bet
        /// 3 numbers - 80% of the bet 
        /// 4 numbers - 110% of the bet 
        /// 5 numbers - 150% of the bet
        /// 6 numbers - 200% of the initial bet placed

        {
            Clear();
            prompt = $@"
                                               _     _     _     _     _  
                                              / \   / \   / \   / \   / \ 
                                             ( Б ) ( И ) ( Н ) ( Г ) ( О )
                                              \_/   \_/   \_/   \_/   \_/ 

Трябва да имате минимум 20 CR за да играете. 
Имате ли ги?
            ";
            string[] options = { "Да", "Не" };

            Menu bingoMenu = new Menu(prompt, options);
            int selectedIndex = bingoMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Beep();
                    ChargeCreditsBingo();
                    Betting();
                    break;
                case 1:
                    Beep();
                    Clear();
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("Окей, значи си беден....ходи да работиш тогава!");
                    ForegroundColor = ConsoleColor.Gray;
                    Thread.Sleep(3000);
                    RunFirstChoise();
                    break;
            }
        }

        /// <summary>
        /// bingo
        /// </summary>
        
        private void Betting()
        {
            if (isSecret)
            {
                bingoName = "Bingo Anal";
            }
            while (true)
            {
                Clear();
                WriteLine($"Здравейте, Вие избрахте да играете в {bingoName}. Вие сте заложили 20CR за тази игра. " +
                $"Можете да удвоите залога си, ако въведете 'x2' и да го утроите с 'x3' както и да въведете 'x1', ако не желаете да го пипате.");
                string userInputBet = ReadLine();
                if (userInputBet == "get back" && isSecret)
                {
                    ForegroundColor = ConsoleColor.Magenta;
                    Write($"You hacker!! You stole ");
                    ForegroundColor = ConsoleColor.DarkYellow;
                    Write($"{userBet} CR");
                    ForegroundColor = ConsoleColor.White;
                    WriteLine($" Back....");
                    houseCredits -= userBet;
                    credits += userBet;
                    SaveCreditValue();
                    SaveHouseCredits();
                    Thread.Sleep(3000);
                }
                else if (userInputBet == "x1")
                {
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("Ти не си пипна залога...нека започваме.");
                    ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(4000);
                }
                else if (userInputBet == "x2")
                {
                    userBet *= 2;
                    credits -= userBet - 20;
                    houseCredits += userBet - 20;
                    SaveCreditValue();
                    SaveHouseCredits();
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("Ти си удвои залога - да започваме тогава!");
                    ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(4000);
                }
                else if (userInputBet == "x3")
                {
                    userBet *= 3;
                    credits -= userBet - 20;
                    houseCredits += userBet - 20;
                    SaveCreditValue();
                    SaveHouseCredits();
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("Ти си УТРОИ залога - късмет! Нека започнем тегленето!");
                    ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(4000);
                }
                else
                {
                    Clear();
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Try again!");
                    ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(3000);
                    continue;
                }
                break;
            }
        }

        private void ManyInvalidNumbers()
        {
            ReturnCredits();
            Clear();
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("ТВЪРДЕ МНОГО НЕВАЛИДНИ ЧИСЛА!");
            Thread.Sleep(1500);
            ExitGame();
        }

        private void InvalidNumber()
        {
            ReturnCredits();
            ForegroundColor = ConsoleColor.DarkGray;
            WriteLine("Имате грешка, това е НЕВАЛИДНО число. ОПИТАЙTE ПАК!!");
            ForegroundColor = ConsoleColor.White;
            Thread.Sleep(3000);
        }

        private void WaitForResult()
        {
            for (int i = 0; i < 4; i++)
            {
                string dotsWait = string.Join(waiting, new string[multiplier + i]);
                Write($"\rВие{dotsWait}");
                Thread.Sleep(500);
            }
        }

        private void CreditCounter()
        {
            Clear();
            SetCursorPosition(0, 39);
            Write($"\rCredits: ");
            ForegroundColor = ConsoleColor.DarkYellow;
            Write($"{credits}");
            ForegroundColor = ConsoleColor.White;
            SetCursorPosition(WindowLeft, WindowTop);
        }

        private void WinCreditsGuess()
        {
            houseCredits -= 7;
            credits += 7;
            SaveCreditValue();
        }

        private void ChargeCreditsGuess()
        {

            if (credits >= 2)
            {
                credits -= 2;
                houseCredits += 2;
                SaveCreditValue();
            }
            else
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"{Environment.NewLine}{Environment.NewLine}Нямаш достатъчно кредити в момента...!");
                ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1200);
                RunMainMenu();
            }
        }

        private void ChargeCreditsBingo()
        {
            if (credits >= 20)
            {
                credits -= 20;
                userBet = 20;
                houseCredits += 20;
                SaveCreditValue();
                SaveHouseCredits();
            }
            else
            {
                Clear();
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"Не ме лъжи!");
                ForegroundColor = ConsoleColor.Gray;
                Write("- ");
                ForegroundColor = ConsoleColor.DarkYellow;
                WriteLine("1 CR");
                ForegroundColor = ConsoleColor.Gray;
                Thread.Sleep(1200);
                --credits;
                SaveCreditValue();
                Thread.Sleep(3000);
                RunFirstChoise();
            }
        }

        private void ReturnCredits()
        {
            credits += 2;
            SaveCreditValue();
        }

        private void WrongGuessMenu()
        {
            string prompt = $"Вие загубихте - числото беше: {randomNum} (Mожеш да видиш баланс кредити в Информация.) {Environment.NewLine}{Environment.NewLine}Още един опит?";
            string[] options = { "Да", "Не" };
            Menu anotherTry = new Menu(prompt, options);
            int selectedIndex = anotherTry.Run();
            if (selectedIndex == 1)
            {
                Clear();
                RunMainMenu();
            }
        }

        private void CorrectGuessMenu()
        {
            WinCreditsGuess();
            SaveCreditValue();
            string prompt = $"ТИ ПОЗНА!!!СПЕЧЕЛИ 5 КРЕДИТА(Mожеш да видиш баланс кредити в Информация.) {Environment.NewLine}{Environment.NewLine}Още един опит?";
            string[] options = { "Да", "Не" };
            Menu anotherTry = new Menu(prompt, options);
            int selectedIndex = anotherTry.Run();
            if (selectedIndex == 1)
            {
                Clear();
                RunMainMenu();
            }
        }

        private void ExitGame()
        {
            Clear();
            ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < 4; i++)
            {
                string dotsWait = string.Join(waiting, new string[multiplier + i]);
                Write($"\rИзлизаш от залата{dotsWait}");
                Thread.Sleep(500);
            }
            WriteLine(Environment.NewLine + Environment.NewLine + @"█▄▄ █▄█ █▀▀   █▄▄ █▄█ █▀▀" +
                       $"{Environment.NewLine}\r" + "█▄█ ░█░ ██▄   █▄█ ░█░ ██▄");
            ForegroundColor = ConsoleColor.White;
            Beep();
            Environment.Exit(0);
        }
    }
}