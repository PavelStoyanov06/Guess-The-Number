using Pastel;

static void NewGame()
{
    Random randNum = new Random();
    int selectedNum = randNum.Next(1, 101);

    Console.WriteLine("Please enter an integer between 1 and 100!".Pastel("#FF5555"));

    while (true)
    {
        Console.Write("Enter a number:");
        string userInput = Console.ReadLine();
        bool isValid = int.TryParse(userInput, out int userNum);
        if(userNum > 100 || userNum < 0) isValid = false;


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

    Console.WriteLine("Hope you enjoyed the game!".Pastel("#FFAA00"));
}

NewGame();

