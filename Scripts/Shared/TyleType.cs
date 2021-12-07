using Godot;
using System;

//This file holds the enums that represent different types and states of tiles
/// <summary>
/// The type of tile
/// </summary>
public enum TileType 
{
    Bomb = -1, 
    Empty = 0, 
    One = 1, 
    Two = 2, 
    Three = 3, 
    Four = 4, 
    Five = 5, 
    Six = 6, 
    Seven = 7, 
    Eight = 8
}
/// <summary>
/// The state of the tile
/// </summary>
public enum TileState 
{
    Covered = 0,
    Questioned = 1,
    Flagged = 2,
    Revealed = 3
}