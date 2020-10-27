using Godot;
using System;

//This is the base class for all tiles, it stores its fields and frame cycling. 
public abstract class Tile : AnimatedSprite
{
    public TileState State {get; set;}
    public TileType Type {get; set;}
    public Controller ControllerNode {get; private set;}
    public Game GameNode {get; private set;}

    public override void _Ready()
    {
        ControllerNode = GetParent() as Controller;
        GameNode = ControllerNode.GetParent().GetParent() as Game;
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