using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinFragment : UI_Base
{
    CombinPop _pop;
    ItemCombinScriptable _data;
    
   enum Texts
    {
        ItemName,
    }

    public override bool Init()
    {
        if(base.Init() == false )
            return false;

        BindText(typeof(Texts));
        GetText((int)Texts.ItemName).text = _data.itemName;

        BindEvent(gameObject, () =>
        {
            if(!_pop.CheckMixPop())
                _pop.DeleteMinPop();

            Manager.UI.ShowPopUI<MixturePop>(parent: _pop.trans, callback: (pop) =>
            {
                pop.GetComponent<RectTransform>().localPosition = Vector3.zero;
                pop.GetComponent<RectTransform>().sizeDelta = new Vector2(720, 600);
                pop.SetInfo(_data);
                _pop.SetMixPop(pop);
            });
        });
        return true;
    }
    public void SetInfo(ItemCombinScriptable data, CombinPop pop)
    {
        _pop = pop;
        _data = data;
    }
}
