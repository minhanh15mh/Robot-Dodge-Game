using System;
using System.Reflection.Metadata;
using SplashKitSDK;

public class Player
{
    public int Lives = 5;
    private Bitmap _PlayerBitmap;
    public double X { get; private set; }
    public double Y { get; private set; }
    public bool Quit { get; private set; } 
    const int gap = 10;
    const int speed = 5;
    public int Width { get{return _PlayerBitmap.Width;}}

    public int Height { get{return _PlayerBitmap.Height;}}
    public Player(Window Windowgame)
    {
        _PlayerBitmap = new Bitmap("Player", "Player.png");
        X = (Windowgame.Width - Width) / 2;
        Y = (Windowgame.Height - Height) / 2;
        Quit = false;
    }
    public void Draw()
    {
        SplashKit.ProcessEvents();
        SplashKit.DrawBitmap(_PlayerBitmap, X, Y);
    }
     public void HandleInput()
    {
        
        if (SplashKit.KeyDown(KeyCode.RightKey))
        {
            X = X + speed;
        }
        if (SplashKit.KeyDown(KeyCode.LeftKey))
        {
            X = X - speed;
        }
        if (SplashKit.KeyDown(KeyCode.DownKey))
        {
            Y = Y + speed;
        }
        if (SplashKit.KeyDown(KeyCode.UpKey))
        {
            Y = Y - speed;
        }
        if (SplashKit.KeyTyped(KeyCode.EscapeKey))
        {
            Quit = true;
        }
         if (Lives <= 0)
        {
            SplashKit.ClearScreen(Color.Black);
            SplashKit.DrawText("GAME OVER! ", Color.White, 300, 300);
        }
    }
    public void StayOnWindow(Window Windowgame)
    {
        if (X < gap)
        {
            X = gap;
        }
        if (Y < gap)
        {
            Y = gap;
        }
        if (X > Windowgame.Width - gap - Width)
        {
            X = Windowgame.Width - gap - Width;
        }
        if (Y > Windowgame.Height - gap - Height)
        {
            Y = Windowgame.Height - gap - Height;
        }
    }
    public bool CollideWith(Robot other)
    {
        return _PlayerBitmap.CircleCollision( X, Y, other.CollisionCircle);
    }

}

