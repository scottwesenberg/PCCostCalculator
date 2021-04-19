using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {


            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }


        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
        }

        #region MAIN MENU
        /// 
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// 
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            

            do
            {
                DisplayScreenHeader("Main Menu For The Computer Cost Calculator");


                Console.WriteLine("\ta) Set Theme For Application");
                Console.WriteLine("\tb) How Much Will You Gaming Rig Cost You?");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();


                switch (menuChoice)
                {
                    case "a":
                        DisplaySetTheme();
                        break;

                    case "b":
                        ;
                        break;

                    case "c":
                        ;
                        break;

                    case "q":
                        ;
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.Write("\tPlease enter a letter for the menu choice:");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }


        static void DisplaySetTheme()
        {
            (ConsoleColor foregroundColor, ConsoleColor backgroundColor) themeColors;
            bool themeChosen = false;

            themeColors = ReadThemeData();

            Console.ForegroundColor = themeColors.foregroundColor;
            Console.BackgroundColor = themeColors.backgroundColor;
            Console.Clear();

            DisplayScreenHeader("Set Theme for Application");

            Console.WriteLine($"\tCurrent foreground color: {Console.ForegroundColor}");
            Console.WriteLine($"\tCurrent background color: {Console.BackgroundColor}");
            Console.WriteLine();
            Console.WriteLine("Would you like to change the current theme of the application? [yes | no]");
            if (Console.ReadLine().ToLower() == "yes")
            {
                do
                {
                    themeColors.foregroundColor = GetConsoleColorFromUser("foreground");
                    themeColors.backgroundColor = GetConsoleColorFromUser("background");

                    Console.ForegroundColor = themeColors.foregroundColor;
                    Console.BackgroundColor = themeColors.backgroundColor;
                    Console.Clear();

                    DisplayScreenHeader("Set Theme for Application");
                    Console.WriteLine($"\tNew foreground color: {Console.ForegroundColor}");
                    Console.WriteLine($"\tNew background color: {Console.BackgroundColor}");
                    Console.WriteLine();

                    Console.Write("\tIs this the theme you would like to use?");
                    if (Console.ReadLine().ToLower() == "yes")
                    {
                        themeChosen = true;
                        WriteThemeData(themeColors.foregroundColor, themeColors.backgroundColor);

                    }
                } while (!themeChosen);

            }

            DisplayContinuePrompt();

        }

        static ConsoleColor GetConsoleColorFromUser(string property)
        {
            ConsoleColor consoleColor;
            bool validConsoleColor;

            do
            {
                Console.Write($"Enter a value for the {property}:");
                validConsoleColor = Enum.TryParse<ConsoleColor>(Console.ReadLine(), true, out consoleColor);

                if (!validConsoleColor)
                {
                    Console.WriteLine("\n\tPlease try again. Enter a valid console color.");
                }
                else
                {
                    validConsoleColor = true;
                }
            } while (!validConsoleColor);

            return consoleColor;
        }

        static (ConsoleColor foregroundColor, ConsoleColor backgroundColor) ReadThemeData()
        {
            string dataPath = @"Data/Theme.txt";
            string[] themeColors;

            ConsoleColor foregroundColor;
            ConsoleColor backgroundColor;

            themeColors = File.ReadAllLines(dataPath);

            Enum.TryParse(themeColors[0], true, out foregroundColor);

            Enum.TryParse(themeColors[1], true, out backgroundColor);

            return (foregroundColor, backgroundColor);

        }

        static void WriteThemeData(ConsoleColor foreground, ConsoleColor background)
        {
            string dataPath = @"Data/Theme.txt";

            File.WriteAllText(dataPath, foreground.ToString() + "\n");
            File.WriteAllText(dataPath, background.ToString());
        }

        static (ConsoleColor foregroundColor, ConsoleColor backgroundColor) ReadThemeDataExceptions(out string fileIOStatusMessage)
        {
            string dataPath = @"Data/Theme.txt";
            string[] themeColors;

            ConsoleColor foregroundColor = ConsoleColor.White;
            ConsoleColor backgroundColor = ConsoleColor.Black;

            try
            {
                themeColors = File.ReadAllLines(dataPath);
                if (Enum.TryParse(themeColors[0], true, out foregroundColor) &&
                    Enum.TryParse(themeColors[1], true, out backgroundColor))
                {
                    fileIOStatusMessage = "Complete";
                }

                else
                {
                    fileIOStatusMessage = "Data file was not formatted correctly.";
                }
            }
            catch (DirectoryNotFoundException)
            {
                fileIOStatusMessage = "Unable to locate folder for the data file.";
            }
            catch (Exception)
            {
                fileIOStatusMessage = "Unable to read the data file.";
            }
            return (foregroundColor, backgroundColor);
        }
        static string WriteThemeDataExceptions(ConsoleColor foreground, ConsoleColor background)
        {
            string dataPath = @"Data/Theme.txt";
            string FileIOStatusMessage = "";

            try
            {
                File.WriteAllText(dataPath, foreground.ToString() + "\n");
                File.AppendAllText(dataPath, background.ToString());
                FileIOStatusMessage = "Complete";

            }

            catch (DirectoryNotFoundException)
            {
                FileIOStatusMessage = "Unable to to locate the folder for the data file.";
            }
            catch (Exception)
            {
                FileIOStatusMessage = "Unable to write to data file.";
            }
            return FileIOStatusMessage;
        }
        #endregion
        #region INTERFACE
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tPC Cost Calculator");
            Console.WriteLine();

            DisplayContinuePrompt();
        }
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using PC Cost Calculator!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }
        #endregion
    }
}
