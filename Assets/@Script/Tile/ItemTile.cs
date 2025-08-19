using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemTile : Tile
{
    protected PlayerController player;
    private const float DISTANCE = 10f;
    private bool isInRange = false;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        player = FindObjectOfType<PlayerController>();
        return true;
    }

    private void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.transform.position, transform.position);

        if (!isInRange && dist < DISTANCE)
        {
            player.ItemAction += ItemAblity;
            isInRange = true;
        }
        else if (isInRange && dist >= DISTANCE)
        {
            player.ItemAction -= ItemAblity;
            isInRange = false;
        }
    }

    protected abstract void ItemAblity();
}
