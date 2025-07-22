using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string itemName;
    private Define.TileType type;
    public void SetType(Define.TileType type, string name)
    {
        itemName = name;
        this.type = type;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() == null)
            return;

        // bagManager¿Í ¿¬µ¿
        Manager.Bag.SetItem(type, itemName);
    }
}
