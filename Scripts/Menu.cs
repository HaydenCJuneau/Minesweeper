using Godot;
using System;

public class Menu : Control
{
    public void Play()
    {
        GetTree().ChangeScene("res://Scenes/DifficultyScreen.tscn");
    }

    public void Settings()
    {

    }

    public void Quit()
    {
        GetTree().Quit();
    }
}
