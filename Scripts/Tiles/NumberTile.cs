using Godot;
using System;


public class NumberTile : Tile, ITile
{
    public void TilePressed()
    {
        if(GameNode.State == GameState.Current)
        {
            if(Input.IsActionJustPressed("LeftClick") && Input.IsActionJustPressed("RightClick"))
            {
                //TODO: If both left and right clicks then reveal all unflagged tiles around this one
            }
            else if(Input.IsActionJustPressed("LeftClick"))
            {   if(State == TileState.Default)
                 Reveal();
            }
            else if(Input.IsActionJustPressed("RightClick"))
            {    
                CycleFrame();
            }
        }
    }

    public void Reveal()
    {
        if(State != TileState.Clicked)
        {
            State = TileState.Clicked;
            GameNode.TileClicked();
            Frame = GameGlobals.NumFrameMap[Type];
        }
    }
}