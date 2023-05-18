using Flashcards_csm.stough;
using Flashcards_csm.stough.DTOs;
using Microsoft.IdentityModel.Tokens;
using Spectre.Console;
using static ConsoleUtilities.PaginatedMenu;

InitApplication();
MainMenu();

void InitApplication()
{
    Console.Title = $"Flashcards Application : Version {System.Configuration.ConfigurationManager.AppSettings.Get("ApplicationVersion")}";
    Database.Init();
}

void MainMenu()
{
    AnsiConsole.Clear();

    UIFactory.BetterMenu("Main Menu", new List<MenuOption>()
    {
        new MenuOption("View Flashcard Stacks", () => {StacksMenu(); }),
        new MenuOption("Create New Stack", () => { CreateNewStack(); }),
        new MenuOption("Quit Program", () => {Environment.Exit(0); })
    });
}

void StacksMenu()
{
    AnsiConsole.Clear();

    List<MenuOption> options = StacksAccessor.Get().ConvertAll(
            stack => { return new MenuOption(stack.Name, () => { StackDetails(stack); }); }
    ).Concat(new List<MenuOption>() {
        new MenuOption("Return To Main Menu", () => { MainMenu(); }),
    }).ToList();

    UIFactory.BetterMenu("Select An Available Stack Or Select An Option", options);
}

void StackDetails(Stack stack)
{
    AnsiConsole.Clear();

    AnsiConsole.Write(UIFactory.GetFlashcardsTable(stack));
    AnsiConsole.Write('\n');

    UIFactory.BetterMenu($"{stack.Name} Menu", new List<MenuOption>
    {
        new MenuOption("Study Session", () => { StudySession(stack); }),
        new MenuOption("Delete This Stack", () => {
            UIFactory.YesNo("Are you sure you want to delete this stack?", () => { StacksAccessor.Delete(stack); StacksMenu(); }, () => { StackDetails(stack); });
        }),
        new MenuOption("Return To All Stacks", () => { StacksMenu(); })
    });
}

void CreateNewStack()
{
    AnsiConsole.Clear();

    string stackName = Input.InputString("Please enter this new stacks name: ");

    while(StacksAccessor.GetStackByName(stackName) != null)
    {
        Console.WriteLine($"Stack {stackName} already exists. Please choose a different name!");
        stackName = Input.InputString("Please enter this new stacks name: ");
    }

    Stack newStack = StacksAccessor.Insert(stackName);

    InsertFlashcards(newStack);
}

void InsertFlashcards(Stack stack)
{
    AnsiConsole.Clear();

    AnsiConsole.WriteLine($"Stack: {stack.Name}\n");

    AnsiConsole.Write(UIFactory.GetFlashcardsTable(stack));

    UIFactory.YesNo($"Would you like to add a flashcard to {stack.Name}?", () => { CreateNewFlashcard(stack); }, () => { StackDetails(stack); });
}

void CreateNewFlashcard(Stack stack)
{
    AnsiConsole.Clear();

    string question = Input.InputString("Please enter the flashcards question");
    string answer = Input.InputString("Please enter the flashcards answer");

    FlashcardsAccessor.Insert(question, answer, stack);

    InsertFlashcards(stack);
}

void StudySession(Stack stack)
{
    //Get a list of all flashcards in this stack

    //randomize the list order

    //display the flashcards one at a time until they're all done

    //display only the question side, and listen for any key

    //then display the answer side separately, and listen for any key

    //then display the next flashcard question side...

    //perform report functionality
}