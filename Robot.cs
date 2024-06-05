using System;
using SplashKitSDK;

public class Robot
{   public double X { get; private set; }
    public double Y { get; private set; }
    public Color MainColor { get; set; }
    public Circle CollisionCircle
    {
        get { return SplashKit.CircleAt(X , Y , 20); }
    }
    private Vector2D Velocity{ get; set; }
    public int width{get{return 50;}}
    public int height{get{return 50;}}
    public Robot(Window window, Player player)
    {
        
        const int speed = 4;
        if (SplashKit.Rnd() < 0.5)
        {
           
            X = SplashKit.Rnd(window.Width);

            
            if (SplashKit.Rnd() < 0.5)
            {
                Y = -height; 
            }
            else
            {
                Y = window.Height; 
            }
        }
        else
        {
            Y = SplashKit.Rnd(window.Height);

            if (SplashKit.Rnd() < 0.5)
            {
                X = -width;
            }
            else
            {
                X = window.Width;
            }
        }

        
        Point2D fromPt = new Point2D()
        {
            X = X, Y = Y
        };
        Point2D toPt = new Point2D()
        {
            X = player.X, 
            Y = player.Y
        };
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
        Velocity = SplashKit.VectorMultiply(dir,speed);
        MainColor = Color.RandomRGB(200);
    }
    public void Draw()
    {
        double leftX, rightX;
        double eyeY, mouthY;
        leftX = X + 12;
        rightX = X + 27;
        eyeY = Y + 10;
        mouthY = Y + 30;
        SplashKit.FillRectangle(Color.Gray, X, Y, 50, 50);
        SplashKit.FillRectangle(MainColor, leftX, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, rightX, eyeY, 10, 10);
        SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
        SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
    }  
    public void Update()
    {
        X = X + Velocity.X;
        Y = Y + Velocity.Y;
    }
    public bool IsOffscreen(Window window)
    {
        return (X < -width || X > window.Width || Y < -height || Y > window.Height);

    }
}