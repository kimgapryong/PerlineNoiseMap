using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_InventroyFragment : UI_Base, IDropHandler
{
    private Define.TileType _type = Define.TileType.None;
   
    public override bool Init()
    {
        if(base.Init()  == false)   
            return false;

        return true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null)
            return;

        eventData.pointerDrag.transform.SetParent(transform);
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
    public void SetItem(Define.TileType type)
    {
        _type = type;
    }
    public bool CheckType(Define.TileType type)
    {
        if(transform.GetComponentInChildren<UI_BackgroundDrag>() == null)
            return true;

        if(_type == type)
            return true;

        return false;
    }

  

}
