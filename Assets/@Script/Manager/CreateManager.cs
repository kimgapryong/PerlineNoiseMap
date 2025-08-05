using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager
{
    private Vector3 createPos;
    private GameObject curObj;
    private Define.TileType curType = Define.TileType.None;
    private InvenFragment curInven;

    public void GetPosition(Vector3 vec, bool check, InvenFragment inven)
    {
        if (inven == null)
            return;

        Define.TileType type = inven._type;
        curType = type;
        
        if (!check || type == Define.TileType.None)
        {
            DeleteData();
            return;
        }
       
        if (curType != type)
            DeleteData();


        if (curObj != null)
        {
            createPos = vec;
            curObj.transform.position = createPos;
            
            return;
        }
        if (!Define.CheckTileType(curType))
            return;


        BagItem item = Manager.Bag.GetItem(type);
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        Material[] newMat = new Material[] { Manager.Resources.Load<Material>($"Material/{item.itemName}Mat") };
        cube.GetComponent<MeshRenderer>().materials = newMat;
        
        cube.transform.localScale = Vector3.one * 4;
        cube.transform.position = vec;
        curObj = cube;

        createPos = vec;
        curInven = inven;
    }
    public void UseItem()
    {
        if(curObj == null)
            return;

       
        CreateTile();
        //curInven.UseItem(CreateTile);
    }
    private void CreateTile()
    {
        Debug.Log(curType);
        BagItem item = Manager.Bag.GetItem(curType);

        Material[] newMat = new Material[] { Manager.Resources.Load<Material>($"Material/{item.itemName}") };
        curObj.GetComponent<MeshRenderer>().materials = newMat;

        Tile tile = curObj.AddComponent<Tile>();
        tile.GetSetTile(item.itemName, item.type, 100);

        curObj = null;
        curInven.UseItem();
    }
    public void DeleteData()
    {
        if(curObj == null)
            return;

        UnityEngine.Object.DestroyImmediate(curObj);
        curObj = null;
    }
  
    
}
