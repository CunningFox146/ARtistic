using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    public class GameLauncher : EditorWindow
    {
        public void OnGUI()
        {
            EditorSceneManager.playModeStartScene = (SceneAsset)EditorGUILayout.ObjectField(new GUIContent("Start Scene"), EditorSceneManager.playModeStartScene, typeof(SceneAsset), false);
        }
        
        [MenuItem("Tools/Startup scene")]
        private static void Open()
        {
            GetWindow<GameLauncher>();
        }
    }
}