using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinPop : UI_Pop
{
    ItemCombinScriptable[] _datas;
    MixturePop curMix;
    public Transform trans { get; set; }
   enum Objects
    {
        CombinContent,
        MixtureContent,
    }
    public override bool Init()
    {
        if(base.Init() == false) 
            return false;

        BindObject(typeof(Objects));
        trans = GetObject((int)Objects.MixtureContent).transform;
        for(int i = 0; i < _datas.Length; i++)
        {
            Manager.UI.MakeSubItem<CombinFragment>(GetObject((int)Objects.CombinContent).transform, callback: (fragment) =>
            {
                fragment.SetInfo(_datas[i], this);
            });
        }
        return true;
    }
    public void SetInfo(ItemCombinScriptable[] datas)
    {
        _datas = datas;
    }
    public void SetMixPop(MixturePop mixPop)
    {
        curMix = mixPop;
    }
    public bool CheckMixPop()
    {
        if (curMix == null) 
            return true;

        return false;
    }
    public void DeleteMinPop()
    {
        curMix = null;
        Manager.UI.ClosePopUI(curMix);
    }
}
