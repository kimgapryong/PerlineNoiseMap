using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance = null;
    public static Manager Instance { get { Init(); return _instance; } }

    private ResourcesManager _resources = new ResourcesManager();
    public static ResourcesManager Resources { get { return Instance._resources; } }

    private BagManager _bag = new BagManager();
    public static BagManager Bag { get { return Instance._bag; } }

    private UIManager _ui = new UIManager();
    public static UIManager UI { get { return Instance._ui; } }

    private CreateManager _create = new CreateManager();
    public static CreateManager Create { get { return Instance._create; } }
    public static void Init()
    {
        if (_instance != null)
            return;

        GameObject go = GameObject.Find("@Manager");
        if (go == null)
        {
            go = new GameObject("@Manager");
            go.AddComponent<Manager>();
        }
        _instance = go.GetComponent<Manager>();
        DontDestroyOnLoad(go);
    }
}
