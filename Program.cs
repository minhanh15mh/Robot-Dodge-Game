using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        Window gameWindow = new Window("Robot dodge game", 800, 420);
        RobotDodge gameplayer = new RobotDodge(gameWindow);
        
        

        while (!gameWindow.CloseRequested && !gameplayer.Quit)
        {
            SplashKit.ProcessEvents();
            gameplayer.HandleInput();
            gameplayer.Draw();
            gameplayer.Update();
        }
    }
}


