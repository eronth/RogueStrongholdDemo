using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldGrid
{
    public int Height = 41;
    public int Width = 41;
    public Vector2Int Center;
    private WorldCell[,] Grid;

    public WorldGrid()
    {
        Grid = new WorldCell[Height, Width];
        Center = new Vector2Int(Height/2, Width/2);
        AssignCoordinates();
        ConnectNeighbors();
    }

    #region Initializers
    private void AssignCoordinates()
    {
        for(int h = 0; h < Height; h++)
        {
            for(int w = 0; w < Width; w++)
            {
                Grid[h,w] = new WorldCell();
                Grid[h,w].Coordinates.x = h;
                Grid[h,w].Coordinates.y = w;
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
            
            // If the y (vertical) value is EVEN, then the "adjacent" horizontal cells (cells located at x, y-1 and x, y+1)
            // will be shifted slightly NORTH of the center cell.
            // If the y (vertical) value is ODD, then the "adjacent" horizontal cells (cells located at x, y-1 and x, y+1)
            // will be shifted slightly SOUTH of the center cell.
            ConnectDiagonalNeighbors(cell, (cell.Coordinates.y % 2 == 0));
        }
    }

    private void ConnectDiagonalNeighbors(WorldCell cell, bool isYValueEven)
    {
        // If the y (vertical) value is EVEN, then the "adjacent" horizontal cells (cells located at x, y-1 and x, y+1)
        // will be shifted slightly NORTH of the center cell.
        // If the y (vertical) value is ODD, then the "adjacent" horizontal cells (cells located at x, y-1 and x, y+1)
        // will be shifted slightly SOUTH of the center cell.
        int adjustment = (isYValueEven) ? 0 : 1;
        int northX = cell.Coordinates.x + adjustment;
        int southX = northX-1;
        int eastY = cell.Coordinates.y+1;
        int westY = cell.Coordinates.y-1;
        WorldCell nwholder, swholder, neholder, seholder;

        int h = Height;
        int w = Width;
        try {
            // Handle westward directions.
            if (westY >= 0)
            {
                // TODO REDO ALL OF THIS LOGIC TO WORK RIGHT.
                // TODO IT IS ALL WRONG; GOTTA REDO
                // Northwest
                nwholder
                    = (northX < Height)
                    ? GetCell(
                            northX,
                            westY
                        )
                    : null;
                cell.Neighbor[(int)HexDirection.NW] = nwholder;

                // Southwest
                swholder
                    = (southX >= 0)
                    ? GetCell(
                            southX,
                            westY
                        )
                    : null;
                cell.Neighbor[(int)HexDirection.SW] = swholder;
            }

            // Handle eastward directions.
            if (eastY < Width)
            {
                // Northeast
                neholder
                    = (northX < Height)
                    ? GetCell(
                            northX,
                            eastY
                        )
                    : null;
                cell.Neighbor[(int)HexDirection.NE] = neholder;

                // Southeast
                seholder
                    = (southX >= 0)
                    ? GetCell(
                            southX,
                            eastY
                        )
                    : null;
                cell.Neighbor[(int)HexDirection.SE] = seholder;
            }
        }
        catch (System.Exception e)
        {

        }
    }
    #endregion

    public WorldCell GetCell(Vector3Int Coordinates) { return GetCell(Coordinates.x, Coordinates.y); }
    public WorldCell GetCell(Vector2Int Coordinates) { return GetCell(Coordinates.x, Coordinates.y); }
    public WorldCell GetCell(int x, int y)
    {
        // TODO Use LInq?
        int gridXIndex = x;
        int gridYIndex = y;
        string exceptionMessage = "Cell Boundry Exception due to the following:";
        // This is used as a flag instead of simply throwing the exception so we can ensure all issues are put in the message.
        bool throwException = false;

        if (0 > gridXIndex || gridXIndex >= Height)
        {
            exceptionMessage += $" X value ({x}) out of bounds.";
            throwException = true;
        }
        
        if (0 > gridYIndex || gridYIndex >= Width)
        {
            exceptionMessage += $" Y value ({y}) out of bounds.";
            throwException = true;
        }
        
        if (throwException)
        {
            throw new System.Exception(exceptionMessage);
        }

        return Grid[gridXIndex, gridYIndex];
    }

}
