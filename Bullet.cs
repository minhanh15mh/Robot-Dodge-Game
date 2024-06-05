using System;
using SplashKitSDK;
public class Bullet
{ 
    private double X { get; set; }
    private double Y { get; set; }
   
    private Vector2D Velocity{ get; set; }
    private Circle CollisionCircle
    {
        get
        {
            return SplashKit.CircleAt(X, Y, 10);
        }
    }
    public Bullet( Player player)
    {
        const int speed = 8;
        
        X = player.X + player.Width/2;
        Y = player.Y + player.Height/2;

        Point2D fromPt = new Point2D()
        {
            X = X, Y = Y
        };
        Point2D mousePt = SplashKit.MousePosition();
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, mousePt));
        Velocity = SplashKit.VectorMultiply(dir, speed);

    }
    public void Update()
    {
        X = X + Velocity.X;
        Y = Y + Velocity.Y;
    }
    
    public void Draw()
    {
        SplashKit.FillCircle(Color.Black, X, Y, 10);
        
    }
    public bool CollideWith(Robot robot)
    {
        
        if (SplashKit.CirclesIntersect(CollisionCircle, robot.CollisionCircle))
        {
            return true;
        }
        else return false;
    }
}