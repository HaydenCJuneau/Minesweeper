using Godot;
using System;

//This file holds types and interfaces
public enum TileType {Bomb, Empty, One, Two, Three, Four, Five, Six, Seven, Eight}

public enum TileState {Default, Clicked, Flagged, Questioned, Empty}

public interface IBomb
{
    void EndGameReveal();
}

public interface ITile
{
    void Reveal();
}