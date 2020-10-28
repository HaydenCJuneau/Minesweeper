using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public class Controller : Node2D
{
   //Fields
   private Game Game;
   private Queue<Vector2> ToReveal;

   public override void _Ready()
   {
      Game = GetParent().GetParent() as Game;
      ToReveal = new Queue<Vector2>();

      Game.State = GameState.NoGame;
      Game.CreateNewGameBoard(GameGlobals.newGameRows, GameGlobals.newGameColumns, GameGlobals.newGameBombs);
      DrawBoard();
   }

   //Process is called every frame, right now it is just in charge of revealing anything in the reveal queue
   public override void _Process(float delta)
   {
      if(ToReveal.Count > 0)
      {
         var tCoords = ToReveal.Dequeue();
         ITile rTile = GetNode($"{tCoords.x},{tCoords.y}") as ITile;
         rTile.Reveal();
      }
   }

   //This Method takes the list of instructions and draws each tile onto the screen
   public void DrawBoard()
   {
      foreach(var tile in Game.BoardInstructions)
      {
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

   //This Method is called when empty tiles are clicked, it searches for other empty or number tiles around itself, then adds them to a queue to be revealed
   //This creates the effect of clearing a bunch of empty tiles at once. 
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

   //This Method is called at the end of a game, when a bomb is clicked, revealing all bombs
   //This could be moved to the games logic file. 
   public void EndGameLose()
   {
      Game.State = GameState.GameLost;
      //Query out all Bomb tiles in instructions list
      var bombTiles = 
      from tile in Game.BoardInstructions
      where tile.Type == TileType.Bomb
      select tile;
   
      foreach(var bTile in bombTiles)
      {
         var tile = GetNode($"{bTile.Index.x},{bTile.Index.y}") as IBomb;
         tile.EndGameReveal();
      }
   }
}