using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGrid
{
    public int Height = 21;
    public int Width = 21;
    private WorldCell[,] Grid;

    public WorldGrid()
    {
        Grid = new WorldCell[Height, Width];
        AssignCoordinates();
        ConnectNeighbors();
    }

    private void AssignCoordinates()
    {
        for(int h = 0; h < Height; h++)
        {
            for(int w = 0; w < Width; w++)
            {
                Grid[h,w] = new WorldCell();
                Grid[h,w].Coordinates.x = h;
                Grid[h,w].Coordinates.y = w/2;
            }
        }
    }

    private void ConnectNeighbors()
    {
        foreach(WorldCell cell in Grid)
        {
            cell.Neighbor[(int)HexDirection.N]
                = (cell.Coordinates.x+1 < Height)
                    ? GetCell(cell.Coordinates.x+1, cell.Coordinates.y)
                    : null;
            
            cell.Neighbor[(int)HexDirection.S] 
                = (cell.Coordinates.x-1  >= 0)
                    ? GetCell(cell.Coordinates.x-1, cell.Coordinates.y)
                    : null;
            
            // If the y value is even, we do diagonals downward.
            // If the y value is odd, we do diagonals upward.
            ConnectDiagonalNeighbors(cell, (cell.Coordinates.y % 2 == 0));
        }
    }

    private void ConnectDiagonalNeighbors(WorldCell cell, bool isYValueEven)
    {
        // This adjustment acts to set the diagonals the right direction depending on if y is even or not.
        // If the y value is even, we do diagonals downward.
        // If the y value is odd, we do diagonals upward.
        int adjustment = (isYValueEven) ? 0 : 1;

        // Handle westward directions.
        if (cell.Coordinates.y-1 + (Width/2) >= 0)
        {
            // Northwest
            cell.Neighbor[(int)HexDirection.NW] 
                = (cell.Coordinates.x + adjustment < Height)
                ? GetCell(
                        cell.Coordinates.x + adjustment,
                        cell.Coordinates.y-1
                    )
                : null;

            // Southwest
            cell.Neighbor[(int)HexDirection.SW] 
                = (cell.Coordinates.x-1 + adjustment >= 0)
                ? GetCell(
                        cell.Coordinates.x-1 + adjustment,
                        cell.Coordinates.y-1
                    )
                : null;
        }

        // Handle eastward directions.
        if (cell.Coordinates.y+1 + (Width/2) < Width)
        {
            // Northeast
            cell.Neighbor[(int)HexDirection.NE]
                = (cell.Coordinates.x + adjustment < Height)
                ? GetCell(
                        cell.Coordinates.x + adjustment,
                        cell.Coordinates.y+1
                    )
                : null;

            //Southeast
            cell.Neighbor[(int)HexDirection.SE]
                = (cell.Coordinates.x-1 + adjustment >= 0)
                ? GetCell(
                        cell.Coordinates.x-1 + adjustment,
                        cell.Coordinates.y+1
                    )
                : null;
        }
    }

    WorldCell GetCell(int x, int y)
    {
        // TODO Use LInq?
        int gridXIndex = x;
        int gridYIndex = y+(Width/2);
        string exceptionMessage = "Exception due to the following:";
        bool throwException = false;

        if (0 > gridXIndex || gridXIndex >= Height)
        {
            exceptionMessage += " X value out of bounds.";
            throwException = true;
        }
        
        if (0 > gridYIndex || gridYIndex >= Width)
        {
            exceptionMessage += " Y value out of bounds.";
            throwException = true;
        }
        
        if (throwException)
        {
            throw new System.Exception(exceptionMessage);
        }

        return Grid[gridXIndex, gridYIndex];
    }

}
