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
        Leaf
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
}
