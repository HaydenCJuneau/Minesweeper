using Godot;
using System;
using System.Collections.Generic;

public class Controller : Node2D
{
   //Fields
   public Game Game;
   Queue<Vector2> ToReveal = new Queue<Vector2>();

   public override void _Ready()
   {
      Game = GetParent().GetParent() as Game;
      Game.State = GameState.NoGame;

      Game.CreateNewGameBoard(GameGlobals.newGameRows, GameGlobals.newGameColumns, GameGlobals.newGameBombs);
      DrawBoard();
   }

   public override void _Process(float delta)
   {
      if(ToReveal.Count > 0)
      {
         var tCoords = ToReveal.Dequeue();
         ITile rTile = GetNode($"{tCoords.x},{tCoords.y}") as ITile;
         rTile.Reveal();
      }
   }

   //Board Visualisation Method
   public void DrawBoard()
   {
      var instructions = Game.BoardInstructions;
      GD.Print("Before loop test");
      GD.Print(instructions.Count);

      while(instructions.Count > 0)
      {
         var tile = instructions.Dequeue();
         var scene = GD.Load<PackedScene>(GameGlobals.ScenePathMap[tile.Type]).Instance();
         AddChild(scene);
         var node = GetNode("NewTile") as AnimatedSprite;
         var script = GetNode("NewTile") as Tile;
         script.Type = tile.Type;
         node.Position = tile.Position;
         node.Name = $"{tile.Index.x},{tile.Index.y}";
      }

      Game.State = GameState.Current;
      Game.GameTimer.Start();
      GD.Print("Finished Drawing New Board");
   }

   public void QueueEmptyNeighbors(Vector2 pos)
   {
      var vectorQueue = new Vector2();

      //If tile is numbered or empty, queue its neighbors to be revealed
      if(Game.BoardGrid.Read((int)pos.x,(int)pos.y) > -1)
      {
         foreach(Vector2 neighbor in Game.Neighbors)
         {
            vectorQueue = new Vector2(pos.x + neighbor.x, pos.y + neighbor.y);
            if(!Game.BoardGrid.OutOfBounds((int)vectorQueue.x, (int)vectorQueue.y))
            {
               ToReveal.Enqueue(vectorQueue);
            }
         }
      }
   }

   public void EndGameLose()
   {
      Game.State = GameState.GameLost;

      for(var y = 0; y < Game.BoardSize.y-1; y++)
      {
         for(var x = 0; x < Game.BoardSize.x-1; x++)
         {
            var Tile = GetNode($"{x},{y}") as Tile;
            if(Tile is IBomb)
            {
               IBomb bT = Tile as IBomb;
               bT.EndGameReveal();
            }
         }
      }
   }
}
