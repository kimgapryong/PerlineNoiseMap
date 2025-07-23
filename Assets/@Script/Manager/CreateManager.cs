using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager
{
    private Vector3 createPos;
    private GameObject curObj;
    private Define.TileType curType;
    public void GetPosition(Vector3 vec, bool check, Define.TileType type)
    {
        if(createPos == vec) 
            return;

        if (!check || type == Define.TileType.None)
        {
            DeleteData();
            return;
        }

        if(curType != type)
            DeleteData();
            
       
        if(curObj != null)
        {
            createPos = vec;
            curObj.transform.position = createPos;
            
            return;
        }
            
        BagItem item = Manager.Bag.GetItem(type);
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.GetComponent<MeshRenderer>().materials[0].color = item.color;
        cube.transform.localScale = Vector3.one * 4;
        cube.transform.position = vec;
        curObj = cube;

        createPos = vec;
        curType = type;
    }

    private void DeleteData()
    {
        if(curObj == null)
            return;

        curObj = null;
        UnityEngine.Object.Destroy(curObj);
    }

    
}
