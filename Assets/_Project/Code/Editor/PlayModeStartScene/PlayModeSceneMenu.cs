using UnityEditor;

namespace _Project.Code.Editor.PlayModeStartScene
{
    public static class PlayModeSceneMenu
    {
        [MenuItem("Tools/Play Mode Start Scene…", false, 0)]
        private static void Open() => PlayModeStartSceneController.ShowSelectionMenu();
    }
}
