using UnityEditor;
using UnityEditor.Toolbars;

namespace _Project.Code.Editor.PlayModeStartScene
{
    [InitializeOnLoad]
    public static class PlayModeSceneToolbar
    {
        private const string ElementName = "PlayModeStartScene/Selector";

        static PlayModeSceneToolbar()
        {
            PlayModeStartSceneController.Changed += () => MainToolbar.Refresh(ElementName);
        }

        [MainToolbarElement(ElementName, defaultDockPosition = MainToolbarDockPosition.Right)]
        private static MainToolbarElement CreateSelector()
        {
            return new MainToolbarDropdown(
                new MainToolbarContent("▶ " + PlayModeStartSceneController.CurrentLabel, "Scene loaded when entering Play Mode"),
                PlayModeStartSceneController.ShowSelectionMenu);
        }
    }
}
