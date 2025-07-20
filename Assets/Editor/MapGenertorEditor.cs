using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGenertorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator m_Generator = (MapGenerator)target;

        if (DrawDefaultInspector())
        {
            if (m_Generator.update)
                m_Generator.CreateMapTextureGenerator();
        }

        if (GUILayout.Button("Generate"))
        {
            m_Generator.CreateMapTextureGenerator();
        }
    }
    
}
