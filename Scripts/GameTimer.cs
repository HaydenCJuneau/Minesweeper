using Godot;
using System;

public class GameTimer : Label
{
    //This class controls the timer durng a game
    Timer Timer;
    int Seconds;
    int Minutes;
    int Hours;
    
    //System Methods
    public override void _Ready()
    {
        Timer = GetNode("GameTimer") as Timer;
        Zero();
    }

    public void SecondElapsed()
    {
        Seconds += 1;

        if(Seconds == 60)
        {
            Minutes += 1;
            Seconds = 0;
        }

        if(Minutes == 60)
        {
            Hours += 1;
            Minutes = 0;
        }

        if(Hours > 0)
            Text = $"Time: {Hours}:{Minutes}:{Seconds}";
        else
            Text = $"Time: {Minutes}:{Seconds}";
    }

    public void Start()
    {
        Timer.Start();
    }

    public void Stop()
    {
        Timer.Stop();
    }

    public void Zero()
    {
        Text = "Time: 0";
        Seconds = 0;
        Minutes = 0;
        Hours = 0;
    }
}
