using Godot;
using System;
using System.Collections.Generic;

public class Game : Node2D
{
   public Vector2[] Neighbors = new Vector2[8]
   {new Vector2(-1,-1),new Vector2(-1,0),new Vector2(-1,1),new Vector2(0,-1),new Vector2(0,1),new Vector2(1,-1),new Vector2(1,0),new Vector2(1,1)};

   //Fields - (first node instances, then data)
   public GameTimer GameTimer;
   Controller SpawnerScript;
   Node2D SpawnerNode;
   Label UILabel;
   Button ReplayButton;
   public GameState State { get; set; }
   public Vector2 BoardSize { get; private set; }
   public Queue<TileStruct> BoardInstructions { get; private set; }
   public Grid BoardGrid { get; private set; }
   public int CurrentBombs { get; set; }
   public int UnclearTiles { get; set; }

   //System Methods
   public override void _Ready()
   {
      SpawnerScript = GetNode("Game/Controller") as Controller;
      SpawnerNode = GetNode("Game/Controller") as Node2D;
      UILabel = GetNode("UI/UILabel") as Label;
      GameTimer = GetNode("UI/GameTimeLabel") as GameTimer;
      ReplayButton = GetNode("UI/Replay") as Button;
      ReplayButton.Hide();
   }

   public override void _Process(float delta)
   {
      Navigation();

      switch(State)
      {
         case GameState.GameLost:
         GameTimer.Stop();
         ReplayButton.Show();
         State = GameState.NoGame;
         GD.Print("Game Lost!");
         UILabel.Text = "Game Lost!";
         break;

         case GameState.GameWon:
         GameTimer.Stop();
         ReplayButton.Show();
         State = GameState.NoGame;
         GD.Print("Game Won!");
         UILabel.Text = "Game Won!";
         break;

         case GameState.Current:
         if (UnclearTiles == CurrentBombs)
            State = GameState.GameWon;
         break;
      }
   }

   public void BackButton()
   {
      State = GameState.GameLost;
      GetTree().ChangeScene("res://Scenes/Menu.tscn");
   }

   public void TileClicked()
   {
      UnclearTiles--;
   }

   public void ReplayPressed()
   {
      GetTree().ChangeScene("res://Scenes/DifficultyScreen.tscn");
   }

   //Called to Move board around the screen
   void Navigation()
   {
      if(Input.IsActionJustPressed("ScrollUp"))
      {
         SpawnerNode.Position = new Vector2(SpawnerNode.Position.x, SpawnerNode.Position.y + 32);
      }
      if(Input.IsActionJustPressed("ScrollDown"))
      {
         SpawnerNode.Position = new Vector2(SpawnerNode.Position.x, SpawnerNode.Position.y - 32);
      }
      if(Input.IsActionJustPressed("ScrollLeft"))
      {
         SpawnerNode.Position = new Vector2(SpawnerNode.Position.x + 32, SpawnerNode.Position.y);
      }
      if(Input.IsActionJustPressed("ScrollRight"))
      {
         SpawnerNode.Position = new Vector2(SpawnerNode.Position.x - 32, SpawnerNode.Position.y);
      }
   }

   //Creates a grid datastructure, and a list of tile instructions
   public void CreateNewGameBoard(int rows, int columns, int bombs)
   {
      GD.Print($"Starting Create New Game Board. r{rows},c{columns},b{bombs}");

      var rng = new Random();
      BoardGrid = new Grid(rows, columns);
      
      //Step 1 is to initialize the list of bomb locations
      var BombList = new HashSet<Vector2>();

      while (BombList.Count < bombs)
      {
         BombList.Add(new Vector2(rng.Next(1, rows - 1), rng.Next(1, columns - 1)));
      }

      //Step 2 is to create list of bomb neighbors
      var BombNeighbors = new List<Vector2>();
      foreach (Vector2 bombTile in BombList)
      {
         foreach (Vector2 neighbor in Neighbors)
         {
            BombNeighbors.Add(new Vector2(bombTile + neighbor));
         }
      }

      //Step 3 is to populate the board - neighbors first, bombs second
      foreach (var neighbor in BombNeighbors)
      {
         BoardGrid.Add(Convert.ToInt32(neighbor.x), Convert.ToInt32(neighbor.y), 1);
      }

      foreach (var bombTile in BombList)
      {
         BoardGrid.Write(Convert.ToInt32(bombTile.x), Convert.ToInt32(bombTile.y), -1);
      }

      //Step 4 is to fill in created data
      BoardInstructions = new Queue<TileStruct> ();

      for(var y = 0; y < rows; y++)  
      {
         for(var x = 0; x < columns; x++)
         {
            BoardInstructions.Enqueue(new TileStruct(x,y,BoardGrid.Read(x,y)));
         }
      }
      
      CurrentBombs = bombs;
      UnclearTiles = rows * columns;
      BoardSize = new Vector2(columns, rows);

      GameTimer = GetNode("UI/GameTimeLabel") as GameTimer; //This shouldnt have to be here, but when its not, it breaks.

      GD.Print("Finished Creation of new Board");
   }
}