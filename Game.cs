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
        public int houseCredits = 0;
        private int count = 0;
        private int randomNum = 0;
        private int counter;
        private string[] options;
        private string prompt;
        private const string waiting = ".";
        private const string creator = "Mitaka7923";
        private const string creditsPath = "Credits.txt";
        private const string houseCreditsPath = "HouseCredits.txt";         //TODO: Create this file and manage house credits. They are equal to whatever credits
                                                                            //      the user spends and initial value is 0.
                                                                            //      File should be created in the initial boot right with the Credits.txt and should
                                                                            //      be on the same method.........
        private const int multiplier = 2;

        public void Start()
        {
            Title = "БИНГО - ИГРАТА!";
            FileCheck();
            SetValueFromFile();
            RunMainMenu();
        }

        private void FileCheck()
        {
            if (!File.Exists(creditsPath))
            {
                using (StreamWriter createFile = File.CreateText(creditsPath)) { }
                SetFileValue();
            }
            FileIsEmpty();
        }   //Here I check if the file for the credits exists - if not sets the initial credits(20)

        private void FileIsEmpty()
        {
            bool isFileEmpty = new FileInfo(creditsPath).Length == 0;
            if (isFileEmpty)
                SetFileValue();
        }

        private void SetFileValue()
        {
            File.WriteAllText(creditsPath, "20");
        }

        private void SetValueFromFile()
        {
            credits = Convert.ToInt32(File.ReadAllText(creditsPath));
        }

        private void SaveFileValue()
        {
            File.WriteAllText(creditsPath, Convert.ToString(credits));
        }

        private void RunMainMenu()
        {
            ForegroundColor = ConsoleColor.White;
            CursorVisible = false;
            OutputEncoding = Encoding.Unicode;
            string prompt = @$"                                                                 

                      ██████╗ ██╗███╗   ██╗ ██████╗  ██████╗      █████╗ ███╗   ██╗███████╗██╗     
                      ██╔══██╗██║████╗  ██║██╔════╝ ██╔═══██╗    ██╔══██╗████╗  ██║██╔════╝██║     
                      ██████╔╝██║██╔██╗ ██║██║  ███╗██║   ██║    ███████║██╔██╗ ██║█████╗  ██║     
                      ██╔══██╗██║██║╚██╗██║██║   ██║██║   ██║    ██╔══██║██║╚██╗██║██╔══╝  ██║     
                      ██████╔╝██║██║ ╚████║╚██████╔╝╚██████╔╝    ██║  ██║██║ ╚████║███████╗███████╗
                      ╚═════╝ ╚═╝╚═╝  ╚═══╝ ╚═════╝  ╚═════╝     ╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝
                                       

          ДОБРЕ ДОШЛИ в БИНГОТО! Имате невероятната възможност да спечелите кредити! Започвате с 20 CR подарък.
                        (Използвай стрелките за да СЕЛЕКТИРАШ опция и ENTER за да я ИЗБЕРЕШ)" + Environment.NewLine + Environment.NewLine;
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
            WriteLine($"{credits} CR");
            ForegroundColor = ConsoleColor.White;
            WriteLine("Натисни някое копче за да се върнеш към менюто...");
            ReadKey(true);
            RunMainMenu();
        }

        private void RunFirstChoise()
        {
            count = 0;
            string prompt = @"  

                      ██████╗ ██╗███╗   ██╗ ██████╗  ██████╗      █████╗ ███╗   ██╗███████╗██╗     
                      ██╔══██╗██║████╗  ██║██╔════╝ ██╔═══██╗    ██╔══██╗████╗  ██║██╔════╝██║     
                      ██████╔╝██║██╔██╗ ██║██║  ███╗██║   ██║    ███████║██╔██╗ ██║█████╗  ██║     
                      ██╔══██╗██║██║╚██╗██║██║   ██║██║   ██║    ██╔══██║██║╚██╗██║██╔══╝  ██║     
                      ██████╔╝██║██║ ╚████║╚██████╔╝╚██████╔╝    ██║  ██║██║ ╚████║███████╗███████╗
                      ╚═════╝ ╚═╝╚═╝  ╚═══╝ ╚═════╝  ╚═════╝     ╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝

На какво ще играете?";
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
            string prompt = @"
              
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
                    ChargeCredits();
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
                        randomNum = rnd.Next(1, 10);    //TODO: SCAM WITH Next() 
                        ForegroundColor = ConsoleColor.White;
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
        /// Improved Betting system
        /// Each integer to 56 belongs to a numbered bingo ball (ascii drawings)
        /// Bingo algorithm, random sequence of numbers generated by the computer at a set time

        ////TODO WININGS:
        /// Minimum 4 numbers guessed for a profit
        /// 1 number - lost the bet
        /// 2 numbers - 30% of the bet
        /// 3 numbers - 80% of the bet 
        /// 4 numbers - 110% of the bet 
        /// 5 numbers - 150% of the bet
        /// 6 numbers - 200% of the initial bet placed

        ////TODO TASKS: 
        /// House never stays behind and always wins
        /// Translate to Bulgarian
        /// Record a demo with a voiceover
        /// Comment the parts of the code that are hard to understand so future reviewers see them

        {
            Clear();
            //int equalCount = 0;
            //string warning = "Невалидни данни!";
            string prompt = $@"
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
                    if (credits >= 20)
                    {
                        credits -= 20;
                        SaveFileValue();
                        RunFirstChoise();
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
                        Thread.Sleep(3000);
                        RunFirstChoise();
                    }
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
                    Write("1CR");
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
                        credits++;
                        Write($"Ти си изработи ");
                        ForegroundColor = ConsoleColor.DarkYellow;
                        WriteLine($"1 CR{Environment.NewLine}");
                        ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(1500);
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
                    SaveFileValue();
                    continue;
                }
            }
        }

        private void WinCredits()
        {
            credits += 7;
            SaveFileValue();
        }

        private void ChargeCredits()
        {

            if (credits >= 2)
            {
                credits -= 2;
                SaveFileValue();
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

        private void ReturnCredits()
        {
            credits += 2;
            SaveFileValue();
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
            WinCredits();
            SaveFileValue();
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