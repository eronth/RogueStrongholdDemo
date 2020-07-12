using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum HexDirection
{
    N, North = N,
    NE, NorthEast = NE,
    SE, SouthEast = SE,
    S, South = S,
    SW, SouthWest = SW,
    NW, NorthWest = NW,
    Undefined = -1
}

static class Direction
{
    public static HexDirection GetDirection(Vector3Int p1, Vector3Int p2)
    {
        double rad = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x);

        if (rad < 0)
            rad = rad + (2 * Mathf.PI);

        var deg = rad * (180 / Mathf.PI);

        HexDirection retVal = HexDirection.Undefined;
        
        if ((000 <= deg && deg < 060) || deg == 360) 
        {
            retVal = HexDirection.NorthEast;
        }
        else if (060 <= deg && deg < 120) 
        {
            retVal = HexDirection.North;
        }
        else if (120 <= deg && deg < 180)
        {
            retVal = HexDirection.NorthWest;
        }
        else if (180 <= deg && deg < 240)
        {
            retVal = HexDirection.SouthWest;
        }
        else if (240 <= deg && deg < 300)
        {
            retVal = HexDirection.South;
        }
        else if (300 <= deg && deg < 360)
        {
            retVal = HexDirection.SouthEast;
        }
        return retVal;
        
        // return deg switch
        // {
        //     _ when (000 <= deg && deg < 060) || deg == 360 => HexDirection.NorthEast,
        //     _ when (060 <= deg && deg < 120) => HexDirection.North,
        //     _ when (120 <= deg && deg < 180) => HexDirection.NorthWest,
        //     _ when (180 <= deg && deg < 240) => HexDirection.SouthWest,
        //     _ when (240 <= deg && deg < 300) => HexDirection.South,
        //     _ when (300 <= deg && deg < 360) => HexDirection.SouthEast,
        //     _ => HexDirection.Undefined,
        // };
    }

    public static HexDirection Opposite(HexDirection dir)
    {
        HexDirection retVal = HexDirection.Undefined;

        switch (dir) 
        {
            // North and South opposites
            case HexDirection.N:
                retVal = HexDirection.S;
                break;
            case HexDirection.S:
                retVal = HexDirection.N;
                break;
            // Northeast and Southwest opposites
            case HexDirection.NE:
                retVal = HexDirection.SW;
                break;
            case HexDirection.SW:
                retVal = HexDirection.NE;
                break;
            // Northwest and southeast opposites
            case HexDirection.NW:
                retVal = HexDirection.SE;
                break;
            case HexDirection.SE:
                retVal = HexDirection.NW;
                break;
        };
        
        return retVal;

        // return dir switch
        // {
        //     // North and South opposites
        //     _ when dir == HexDirection.N => HexDirection.S,
        //     _ when dir == HexDirection.S => HexDirection.N,

        //     // Northeast and Southwest opposites
        //     _ when dir == HexDirection.NE => HexDirection.SW,
        //     _ when dir == HexDirection.SW => HexDirection.NE,

        //     // Northwest and southeast opposites
        //     _ when dir == HexDirection.NW => HexDirection.SE,
        //     _ when dir == HexDirection.SE => HexDirection.NW,

        //     // If direction is undefined, just return undefined as opposite.
        //     _ => HexDirection.Undefined,
        // };
    }
}