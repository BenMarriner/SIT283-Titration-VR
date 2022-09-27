#if UNITY_EDITOR

using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public bool keepSceneViewWhenPlayButtonPressed = false;

    private void Awake()
    {
        if (keepSceneViewWhenPlayButtonPressed)
        {
            UnityEditor.EditorWindow.FocusWindowIfItsOpen<UnityEditor.SceneView>();
        }
    }
}
#endif