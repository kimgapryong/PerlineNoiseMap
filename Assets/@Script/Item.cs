using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string itemName;
    private Define.TileType type;
    private PlayerController player;

    private float distance = 8f;
    private float itemSpeed = 4.5f;

    
    public void SetType(Define.TileType type, string name)
    {
        itemName = name;
        this.type = type;
        player = FindObjectOfType<PlayerController>();

    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < distance)
            transform.position = Vector3.Lerp(transform.position, player.transform.position, itemSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() == null)
            return;

        // bagManager¿Í ¿¬µ¿
        Manager.Bag.SetItem(type, itemName);

        MainCanvas canvas = Manager.UI.SceneUI as MainCanvas;
        
        InvenFragment inven = canvas.CheckItem(type);
        
        inven.SetBagItem(type, Manager.Bag.GetItem(type));
        
        Destroy(gameObject);

    }
   
}
