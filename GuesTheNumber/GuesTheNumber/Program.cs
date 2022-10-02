using Pastel;
using System.Diagnostics;
using System.Runtime.InteropServices;

static void OpenUrl(string url)
{
    try
    {
        Process.Start(url);
    }
    catch
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Process.Start("xdg-open", url);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            Process.Start("open", url);
        }
        else
        {
            throw;
        }
    }
}

static void NewGame()
{
    Console.Title = "Guess The Number";
    string[] diffs = { "easy", "medium", "hard", "extreme", "custom" };
    Console.WriteLine("Choose between: " + "easy".Pastel("#55FF55") + " medium".Pastel("#00AA00") +"[recommended]" + " hard".Pastel("#FFAA00") + " extreme".Pastel("#AA0000") + " custom".Pastel("#FF55FF"));
    Console.Write("Enter the games difficulty:".Pastel("#5555FF"));
    string diffInput = Console.ReadLine();

    while (!diffs.Contains(diffInput.ToLower()))
    {
        Console.WriteLine("Invalid input!".Pastel("#AA0000"));
        Console.Write("Enter the games difficulty:".Pastel("#5555FF"));
        diffInput = Console.ReadLine();
    }

    int endNum = 100;

    int startNum = 1;

    int numGuesses = 15;

    switch (diffInput)
    {
        case "easy":
            startNum = 1;
            endNum = 50;
            numGuesses = 20;
            break;
        case "medium":
            startNum = 1;
            endNum = 100;
            numGuesses = 15;
            break;
        case "hard":
            startNum = -1000;
            endNum = 1000;
            numGuesses = 10;
            break;
        case "extreme":
            startNum = -10000;
            endNum = 10000;
            numGuesses = 10;
            break;
        case "custom":
            Console.Write("Please, enter a start number:".Pastel("#00AAAA"));
            startNum = int.Parse(Console.ReadLine());
            Console.Write("Please, enter the end number:".Pastel("#AA00AA"));
            endNum = int.Parse(Console.ReadLine());
            Console.Write("Please, enter the number of desired guesses:".Pastel("#FF55FF"));
            numGuesses = int.Parse(Console.ReadLine());
            while (numGuesses <= 0)
            {
                if(numGuesses <= 0)
                {
                    Console.WriteLine("Invalid guess amount!".Pastel("#AA0000"));
                }
                Console.Write("Please, enter the number of desired guesses:".Pastel("#FF55FF"));
                numGuesses = int.Parse(Console.ReadLine());
            }
            break;
    }

    Console.WriteLine();

    Random randNum = new();
    int selectedNum = randNum.Next(1, endNum + 1);

    Console.WriteLine($"Please enter an integer between {startNum} and {endNum}!".Pastel("#FF5555"));
    Console.WriteLine($"You have {numGuesses} guesses - be careful not to throw in invalid numbers as it counts as a guess!".Pastel("#FFAA00"));

    while (numGuesses > 0)
    {
        Console.Write("Enter a number:");
        string userInput = Console.ReadLine();
        bool isValid = int.TryParse(userInput, out int userNum);
        if(userNum > endNum || userNum < startNum) isValid = false;


        if (isValid)
        {
            if (selectedNum == userNum)
            {
                Console.WriteLine("You guessed the number!");
                break;
               
            }
            else if (userNum > selectedNum)
            {
                Console.WriteLine("Too High");
            }
            else if (selectedNum > userNum)
            {
                Console.WriteLine("Too Low");
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input!");
            Console.ResetColor();
        }
        numGuesses--;
        if(numGuesses <= 0)
        {
            Console.WriteLine("You are out of guesses!".Pastel("#AA0000"));
            Console.WriteLine($"The number was {selectedNum}.".Pastel("#FFAA00"));
        }
        else Console.WriteLine($"You have {numGuesses} guesses left.".Pastel("#00AAAA"));
    }

    while (true)
    {
        Console.Write("Want to play again?[" + "yes".Pastel("#00AA00") + "/" + "no".Pastel("#AA0000") + "]:");
        string input = Console.ReadLine().ToLower();
        if (input == "yes") NewGame();
        else if (input == "no") break;
        else Console.WriteLine("Invalid input!");
    }

    Console.WriteLine();
    Console.WriteLine("Hope you enjoyed the game!".Pastel("#FFAA00"));
    Console.Write("Press [O] if you want to visit me on GitHub or press [ENTER] to finish the program!".Pastel("#FFFFFF"));

    if (Console.ReadKey().Key == ConsoleKey.O)
    {
        OpenUrl("https://github.com/PavelStoyanov06/Guess-The-Number");
    }
}

NewGame();

