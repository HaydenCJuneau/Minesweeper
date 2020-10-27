using Godot;
using System;
using System.Collections.Generic;

//This Singleton file holds the Global class for the game, tracking wins/ lossses.
//It also takes care of passing data from one script to another.

public static class GameGlobals
{
    public static int GamesWon;
    public static int GamesLost;
    //New Game settings to pass
    public static int newGameRows;
    public static int newGameColumns;
    public static int newGameBombs;
    

    public static Dictionary <int, TileType> TypeMap = new Dictionary<int, TileType> ()
    {
        {-1, TileType.Bomb},
        {0, TileType.Empty},
        {1, TileType.One},
        {2, TileType.Two},
        {3, TileType.Three},
        {4, TileType.Four},
        {5, TileType.Five},
        {6, TileType.Six},
        {7, TileType.Seven},
        {8, TileType.Eight}
    };

    public static Dictionary <TileType, int> NumFrameMap = new Dictionary<TileType, int> ()
    {
        {TileType.One, 4},
        {TileType.Two, 5},
        {TileType.Three, 6},
        {TileType.Four, 7},
        {TileType.Five, 8},
        {TileType.Six, 9},
        {TileType.Seven, 10},
        {TileType.Eight, 11}
    };

    public static Dictionary <TileType, string> ScenePathMap = new Dictionary<TileType, string> ()
    {
        {TileType.Bomb, "res://Scenes/Tiles/BombTile.tscn"},
        {TileType.Empty, "res://Scenes/Tiles/EmptyTile.tscn"},
        {TileType.One, "res://Scenes/Tiles/NumberTile.tscn"},
        {TileType.Two, "res://Scenes/Tiles/NumberTile.tscn"},
        {TileType.Three, "res://Scenes/Tiles/NumberTile.tscn"},
        {TileType.Four, "res://Scenes/Tiles/NumberTile.tscn"},
        {TileType.Five, "res://Scenes/Tiles/NumberTile.tscn"},
        {TileType.Six, "res://Scenes/Tiles/NumberTile.tscn"},
        {TileType.Seven, "res://Scenes/Tiles/NumberTile.tscn"},
        {TileType.Eight, "res://Scenes/Tiles/NumberTile.tscn"}
    };
}

public enum GameState {Current, GameWon, GameLost, NoGame}