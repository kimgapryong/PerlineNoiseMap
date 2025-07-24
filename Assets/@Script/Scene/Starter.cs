using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour
{
    public ItemCombinScriptable[] datas;
    public MapGenerator generator;
    private void Start()
    {
        generator.CraeteMapGenerator();
        Manager.UI.ShowSceneUI<MainCanvas>(callback: (main) =>
        {
            main.SetInfo(datas);
        });
    }
}
