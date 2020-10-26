using Godot;
using System;

public class BombTile : Tile, IBomb, ITile
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
        State = TileState.Clicked;
        Frame = 4;
        SpawnerNode.EndGameLose();
    }

    public void EndGameReveal()
    {
        State = TileState.Clicked;
        Frame = 4;
    }
}
