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
    Console.WriteLine("Choose between: " + "easy".Pastel("#55FF55") + " medium".Pastel("#00AA00") +"[default]" + " hard".Pastel("#FFAA00") + " extreme".Pastel("#AA0000") + " custom".Pastel("#FF55FF"));
    Console.Write("Enter the games difficulty:".Pastel("#5555FF"));
    string diffInput = Console.ReadLine();

    while (!diffs.Contains(diffInput.ToLower()))
    {
        Console.WriteLine("Invalid input!".Pastel("#AA0000"));
        Console.Write("Enter the games difficulty:".Pastel("#5555FF"));
        diffInput = Console.ReadLine();
    }

    int endNum = 100;

    switch (diffInput)
    {
        case "easy":
            endNum = 50;
            break;
        case "medium":
            endNum = 100;
            break;
        case "hard":
            endNum = 1000;
            break;
        case "extreme":
            endNum = 10000;
            break;
        case "custom":
            Console.Write("Please enter a number:".Pastel("#AA00AA"));
            endNum = int.Parse(Console.ReadLine());
            break;
    }

    Console.WriteLine();

    Random randNum = new();
    int selectedNum = randNum.Next(1, endNum + 1);

    Console.WriteLine($"Please enter an integer between 1 and {endNum}!".Pastel("#FF5555"));

    while (true)
    {
        Console.Write("Enter a number:");
        string userInput = Console.ReadLine();
        bool isValid = int.TryParse(userInput, out int userNum);
        if(userNum > endNum || userNum < 0) isValid = false;


        if (isValid)
        {
            if (selectedNum == userNum)
            {
                Console.WriteLine("You guessed the number!");

                bool exit = false;
                while (!exit)
                {
                    Console.Write("Want to play again?["+"yes".Pastel("#00AA00")+"/"+"no".Pastel("#AA0000") +"]:");
                    string input = Console.ReadLine().ToLower();
                    if (input == "yes") NewGame();
                    else if (input == "no") exit = true;
                    else Console.WriteLine("Invalid input!");
                }
                if (exit) break;
               
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
    }

    Console.WriteLine();
    Console.WriteLine("Hope you enjoyed the game!".Pastel("#FFAA00"));
    Console.Write("Press [O] if you want to visit me on githib or press [ENTER] to finish the program!".Pastel("#FFFFFF"));

    if (Console.ReadKey().Key == ConsoleKey.O)
    {
        OpenUrl("https://github.com/PavelStoyanov06/Guess-The-Number");
    }
}

NewGame();

