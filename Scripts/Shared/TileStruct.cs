using Godot;
using System;

public struct TileStruct
{
    public Vector2 Position {get; private set;}
    public Vector2 Index {get; private set;}
    public TileType Type {get; private set;}

    public TileStruct(int x, int y, int type)
    {
        Type = GameGlobals.TypeMap[type];
        Position = new Vector2(x * 32, y * 32);
        Index = new Vector2(x,y);
    }
}
