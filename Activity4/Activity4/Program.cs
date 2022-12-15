// See https://aka.ms/new-console-template for more information
using static GameDeclaringService;

interface MyInterface
{
    string Name { get; set; } //Properties
    string GameName();//Instance Methods
}

public class Game
{
    public string Item;
};

class GameInfo : MyInterface
{
    private string name;
    //instance method GameName()
    public string GameName()
    {
        return ("The game has a name of: " + name);
    }
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
}

class GameDeclaringService
{
    // delegate
    public delegate void GameDeclareEventHandler(object source, EventArgs args);

    //event
    public event GameDeclareEventHandler GameDeclared;

    public void DeclareGame(GameInfo game)
    {
        Console.WriteLine($"Declaring your game '{game.Name}', please wait...");
        Thread.Sleep(4000);

        OnGameCreated();
    }

    protected virtual void OnGameCreated()
    {
        if (GameDeclared != null)
            GameDeclared(this, null);
    }
}

class AppService
{
    public void OnGameDeclared(object source, EventArgs eventArgs)
    {
        Console.WriteLine("AppService: your game is declared.");
    }
}
class Program
{
    static void Main()
    {
        //instance method and properties
        GameInfo myGame = new GameInfo();
        myGame.Name = "Final Fantasy";
        Console.WriteLine(myGame.GameName());

        //event portion
        var game = new GameInfo();
        game.Name = "Elden Ring";
        var declaringService = new GameDeclaringService();
        var appService = new AppService();
        declaringService.GameDeclared += appService.OnGameDeclared;
        declaringService.DeclareGame(game);
        Console.ReadKey();
    }
}