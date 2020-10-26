using Godot;
using System;

public struct Grid 
{
    public int[,] grid {get; private set;}
    public int Rows;
    public int Columns;

    public Vector2 Size {get; private set;}

    public Grid(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        Size = new Vector2(columns,rows);

        grid = new int[rows,columns]; 
        //These arrays act in y,x 

        //These loops fill the grid full of zeroes
        for(var r = 0; r < rows; r++)
        {
            for(var c = 0; c < columns; c++)
            {
                Write(c,r,0);
            }
        }
        GD.Print($"Created grid with dimentions {columns},{rows}");
    }

    public int Read(int x, int y)
    {
        if(!OutOfBounds(x,y))
            return grid[y,x];
        return -2; //For now, negaive two will serve as an out of bounds exception
    }

    public void Write(int x, int y, int value)
    {
        if(!OutOfBounds(x,y))
            grid[Convert.ToInt32(y),Convert.ToInt32(x)] = value;
    }

    public void Add(int x, int y, int value)
    {
        if(!OutOfBounds(x,y))
            grid[Convert.ToInt32(y),Convert.ToInt32(x)] += value;
    }

    public bool OutOfBounds(int x, int y)
    {
        //Returns true if the value will fall out of bounds of the array
        if(x >= Columns || x < 0)
            return true;
        if(y >= Rows || y < 0)
            return true;
        return false;
    }
}
