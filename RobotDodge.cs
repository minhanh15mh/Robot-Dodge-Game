using System;
using SplashKitSDK;

public class RobotDodge
{
    private SplashKitSDK.Timer gameTimer;
    private Bitmap HeartBitmap = new Bitmap("Heart", "heart.png");
    private Player _player;
    private List<Robot> _Robots = new List<Robot>();
    private List<Robot> _removedRobots = new List<Robot>();
    private List<Bullet> _Bullets = new List<Bullet>();
    private List<Bullet> _removedBullets = new List<Bullet>();
    private Window _gamewindow;
    public bool Quit{get{return _player.Quit;}}
    public RobotDodge(Window window)
    {
        _gamewindow = window;
        _player = new Player(_gamewindow);
        gameTimer = new SplashKitSDK.Timer("Game Timer");
        gameTimer.Start();
    }
    public void HandleInput()
    {
        _player.HandleInput();
        _player.StayOnWindow(_gamewindow);
        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            _Bullets.Add(AddBullet());
        } 
    }
    public void Draw()
    {
        _gamewindow.Clear(Color.LightPink);
        foreach (Robot robot in _Robots)
        {
            robot.Draw();
        }

        _player.Draw();

        foreach (Bullet bullet in _Bullets)
        {
            bullet.Draw();
        }
        DrawHearts(_player.Lives);

        SplashKit.DrawText("SCORE: " + (gameTimer.Ticks/1000), Color.Black, 0, 40);
        if (_player.Lives <= 0)
        {
            _gamewindow.Clear(Color.Black);
            Bitmap _GameOver = new Bitmap("Game Over", "gamover.png");
            SplashKit.DrawBitmap(_GameOver, 200, 100);
        }
        _gamewindow.Refresh(60);

    }
    public void Update()
    {
        
        foreach (Robot robot in _Robots)
        {
            robot.Update();
        }

        double randomNumber = SplashKit.Rnd(1000);
        if (randomNumber < 5)
        {
            _Robots.Add(RandomRobot());
        }

        foreach (Bullet bullet in _Bullets)
        {
            bullet.Update();
        }
        CheckCollisions();
    }
    
    public Robot RandomRobot()
    {
        Robot _TestRobot = new Robot(_gamewindow, _player);
        return _TestRobot;
    }
    public Bullet AddBullet()
    {
        Bullet _RandomBullet = new Bullet(_player);
        return _RandomBullet;
    }
    private void CheckCollisions()
    {
        
        foreach (Robot robot in _Robots)
        {
            if (_player.CollideWith(robot) & _player.Lives > 0)
            {
                _player.Lives = _player.Lives - 1;
            }
            if (_player.CollideWith(robot) || robot.IsOffscreen(_gamewindow))
            {
                _removedRobots.Add(robot);
            }
            foreach (Bullet bullet in _Bullets)
            {
                if (bullet.CollideWith(robot) == true)
                {
                    _removedBullets.Add(bullet);
                    _removedRobots.Add(robot);
                }
            }

        }
        
        foreach (Robot robot in _removedRobots)
        {
            _Robots.Remove(robot);
        }
        
        foreach (Bullet bullet in _removedBullets)
        {
            _Bullets.Remove(bullet);
        }
        
    }
    public void DrawHearts(int numberofLives)
    {
        int heart = 0;
        for (int i = 0; i < numberofLives; i ++ )
        {
            if (heart < 300)
            {
                SplashKit.DrawBitmap(HeartBitmap, heart, 0);
                heart = heart + 40;
            }
        }
    }
}