using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class DebugSettings
{
    public static bool DebugZone { get; private set; } = true;
    public static TileBase DebugZoneTile = Resources.Load<Tile>("Tiles/HexTilemaps/HexGround/DebugZoneTile");

    public static bool DebugZoneSurrounding { get; private set; } = true;
    public static TileBase DebugZoneSurroundingTile = Resources.Load<Tile>("Tiles/HexTilemaps/HexGround/DebugZoneSurroundingTile");
    public static bool DebugPath { get; private set; } = true;
    public static TileBase DebugPathTile = Resources.Load<Tile>("Tiles/HexTilemaps/HexGround/DebugPathTile");
}