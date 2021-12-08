using Godot;
using System;

//This is the base class for all tiles, it stores its fields and frame cycling. 
public abstract class Tile : AnimatedSprite
{
    //Data for each tile
    public TileState State { get; private set; }
    public TileType Type { get; private set; }
    private Game GameNode { get; set; }
    public Vector2 BoardPosition { get; private set; }
    private bool MouseOnTile { get; set; }

    // Initializer methods
    public override void _Ready()
    {
        base._Ready();

        MouseOnTile = false;
    }

    public void Initialize(Game gameNode, Vector2 boardPosition, TileType type, TileState state = 0)
    {
        GameNode = gameNode;
        BoardPosition = boardPosition;
        Type = type;
        State = state;
    }

    //Input
    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if(Input.IsActionJustPressed("input_leftclick") 
            && Input.IsActionJustPressed("input_rightclick"))
        {

        }
        else if(Input.IsActionJustPressed("input_leftclick"))
        {

        }
        else if(Input.IsActionJustPressed("input_rightclick"))
        {

        }
    }

    //Methods to cycle the tile frame
    public void SetFrame()
    {
        // 0 - Covered *
        // 1 - Empty Revealed 
        // 2 - Bomb Grey Revealed
        // 3 - Bomb Red Revealed
        // 4 - Flagged *
        // 5 - Question *
        // 6-13 - One-Eight Revealed
        switch((int)State)
        {
            case 0:
                Frame = 0;
                break;
            case 1:
                Frame = 5;
                break;
            case 2:
                Frame = 4;
                break;
            default:
                //If tile is revealed
                if ((int)Type == -1) { Frame = 3; }
                else if ((int)Type == 0) { Frame = 1; }
                else
                {
                    //Number tile
                    Frame = 5 + (int)Type;
                }
                break;
        }
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

    // Signals
    public void OnMouseEntered() => MouseOnTile = true;
    public void OnMouseExited() => MouseOnTile = false;
}