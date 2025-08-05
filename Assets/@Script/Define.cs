using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
  
   public enum TileType
    {
        None,
        Water,
        Ground,
        Rock,
        Tree,
        Leaf,
        Stick,
        Board,
        Crafting,
    }
    public enum State
    {
        Walk,
        Run,
        Dig,
        Idle,
        Attack,
        Jump
    }

    public enum UIEvent
    {
        Click,
        Press,
    }

    public static bool CheckTileType(TileType type)
    {
        switch (type)
        {
            default:
            case TileType.Ground:
            case TileType.Rock:
            case TileType.Tree:
            case TileType.Leaf:
            case TileType.Board:
                return true;

            case TileType.None:
            case TileType.Stick:
            case TileType.Water:
                return false;
        }

    }
}
