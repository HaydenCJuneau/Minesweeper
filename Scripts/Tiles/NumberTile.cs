using Godot;
using System;


public class NumberTile : Tile, ITile
{
    public void TilePressed()
    {
        if(SpawnerNode.Game.State == GameState.Current)
        {
            if(Input.IsActionJustPressed("LeftClick"))
            {   if(State == TileState.Default)
                 Reveal();
            }
            
            if(Input.IsActionJustPressed("RightClick"))
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
            SpawnerNode.Game.TileClicked();
            Frame = GameGlobals.NumFrameMap[Type];
        }
    }
}
