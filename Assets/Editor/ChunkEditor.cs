using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChunkLoader))]
public class ChunkEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ChunkLoader loader = target as ChunkLoader;

        if (GUILayout.Button("Reload")) {
            loader.Reload();
        }
    }
}
