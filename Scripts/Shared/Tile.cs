using Godot;
using System;

public abstract class Tile : AnimatedSprite
{
    public TileState State {get; set;}
    public TileType Type {get; set;}
    public Vector2 Coordinates {get; set;}
    public Controller SpawnerNode {get; set;}

    public override void _Ready()
    {
        SpawnerNode = GetParent() as Controller;
        State = TileState.Default;
        Frame = 0;
    }

    public void CycleFrame()
    {
        switch(State)
        {
            case TileState.Default:
            State = TileState.Flagged;
            Frame = 2;
            break;

            case TileState.Flagged:
            State = TileState.Questioned;
            Frame = 3;
            break;

            case TileState.Questioned:
            State = TileState.Default;
            Frame = 0;
            break;
        }
    }
}
