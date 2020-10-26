 using Godot;
using System;

public class DifficultyScreen : Control
{
    //Control Nodes
    Control customLayer;
    Button easyButton;
    Button mediumButton;
    Button hardButton;
    Button customButton;
    Button playButton;
    Label selLabel;

    Button currentlyPressed;

    //Value fields
    public int columnsSelected {get; private set;}
    public int rowsSelected {get; private set;}
    public int bombsSelected {get; private set;}

    public override void _Ready()
    {
        customLayer = GetNode("CustomEntryBoxes") as Control;
        customLayer.Hide();

        easyButton = GetNode("EasySelect") as Button;
        mediumButton = GetNode("MediumSelect") as Button;
        hardButton = GetNode("HardSelect") as Button;
        customButton = GetNode("CustomSelect") as Button;
        playButton = GetNode("PlayButton") as Button;
        currentlyPressed = null;
        selLabel = GetNode("SelectionLabel") as Label;
        selLabel.Text = "Choose a difficulty";

        columnsSelected = 15;
        rowsSelected = 15;
        bombsSelected = 15;
    }

    public override void _Process(float delta)
    {
        if(currentlyPressed != null)
            playButton.Show();
        else
            playButton.Hide();
    }

    //Button signals

    public void Play()
    {
        SetGameGlobals();
        GetTree().ChangeScene("res://Scenes/Game.tscn");
    }

    public void BackButton()
    {
        GetTree().ChangeScene("res://Scenes/Menu.tscn");
    }

    public void EasyPress(bool buttonPressed)
    {
        if(buttonPressed)
        {
            ClearSelections();
            currentlyPressed = easyButton;
            selLabel.Text = "Selected: Easy";
            columnsSelected = 15;
            rowsSelected = 15;
            bombsSelected = 30;
        }
        else
            currentlyPressed = null;
    }

    public void MediumPress(bool buttonPressed)
    {
        if(buttonPressed)
        {
            ClearSelections();
            currentlyPressed = mediumButton;
            selLabel.Text = "Selected: Medium";
            columnsSelected = 30;
            rowsSelected = 30;
            bombsSelected = 100;
        }
        else
            currentlyPressed = null;
    }

    public void HardPress(bool buttonPressed)
    {
        if(buttonPressed)
        {
            ClearSelections();
            currentlyPressed = hardButton;
            selLabel.Text = "Selected: Hard";
            columnsSelected = 40;
            rowsSelected = 40;
            bombsSelected = 400;
        }
        else
            currentlyPressed = null;
    }

    public void CustomPress(bool buttonPressed)
    {
        if(buttonPressed)
        {
            ClearSelections();
            currentlyPressed = customButton;
            customLayer.Show();
            selLabel.Text = "Selected: Custom";
        }
        else
        {
            currentlyPressed = null;
            customLayer.Hide();
        }
    }

    //Custombox signals
    public void RowEntryChanged(string v)
    {
        int value = customParse(3,100,v);
        GD.Print($"Rows Selected:{value}");
        rowsSelected = Convert.ToInt32(value);
    }

    public void ColumnEntryChanged(string v)
    {
        int value = customParse(3,100,v);
        GD.Print($"Columns Selected:{value}");
        columnsSelected = Convert.ToInt32(value);
    }

    public void BombEntryChanged(string v)
    {
        int value = customParse(1,1000,v);
        GD.Print($"Bombs Selected:{value}");
        if(value  >= columnsSelected * rowsSelected)
            bombsSelected = 15;
        else
            bombsSelected = Convert.ToInt32(value);
    }

    int customParse(int min, int max, string line)
    {
        int value;
        if(int.TryParse(line,out value))
            if(value >= min && value <= max)
            
                return value;
        //
        return min;
    }
    
    //Method for clearing selected when changing choice
    public void ClearSelections()
    {
        if(currentlyPressed != null)
        {
            currentlyPressed.Pressed = false;
            customLayer.Hide();
        } 
    }

    //Method for passing selection into the singleton
    public void SetGameGlobals()
    {
        if(bombsSelected > (columnsSelected*rowsSelected)-1)
            bombsSelected = (columnsSelected*rowsSelected)-1;

        GameGlobals.newGameBombs = bombsSelected;
        GameGlobals.newGameColumns = columnsSelected;
        GameGlobals.newGameRows = rowsSelected;
    }
}
