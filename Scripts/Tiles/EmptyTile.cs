using Godot;
using System;

public class EmptyTile : Tile, ITile
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
        Frame = 1;
        SpawnerNode.Game.TileClicked();
        //Reveals any empty tiles around this one
        SpawnerNode.QueueEmptyNeighbors(Position/32);
        }
    }
}
    
