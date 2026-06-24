using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace _Project.Code.Editor.PlayModeStartScene
{
    [InitializeOnLoad]
    public static class PlayModeStartSceneController
    {
        private const string PrefKey = "Project.PlayModeStartScene.Path";

        public static event Action Changed;

        static PlayModeStartSceneController()
        {
            Apply();
        }

        public static string SelectedScenePath
        {
            get => EditorPrefs.GetString(PrefKey, string.Empty);
            set
            {
                EditorPrefs.SetString(PrefKey, value ?? string.Empty);
                Apply();
            }
        }

        public static string CurrentLabel
        {
            get
            {
                var path = SelectedScenePath;
                return string.IsNullOrEmpty(path) ? "Current scene" : Path.GetFileNameWithoutExtension(path);
            }
        }

        public static void ShowSelectionMenu() => BuildMenu().ShowAsContext();

        public static void ShowSelectionMenu(Rect anchor) => BuildMenu().DropDown(anchor);

        private static GenericMenu BuildMenu()
        {
            var current = SelectedScenePath;
            var menu = new GenericMenu();

            menu.AddItem(
                new GUIContent("Current scene"),
                string.IsNullOrEmpty(current),
                () => SelectedScenePath = string.Empty);

            menu.AddSeparator(string.Empty);

            foreach (var guid in AssetDatabase.FindAssets("t:Scene"))
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                if (path.StartsWith("Packages/"))
                    continue;

                var capturedPath = path;
                menu.AddItem(
                    new GUIContent(Path.GetFileNameWithoutExtension(path)),
                    current == capturedPath,
                    () => SelectedScenePath = capturedPath);
            }

            return menu;
        }

        private static void Apply()
        {
            var path = SelectedScenePath;

            EditorSceneManager.playModeStartScene = string.IsNullOrEmpty(path)
                ? null
                : AssetDatabase.LoadAssetAtPath<SceneAsset>(path);

            Changed?.Invoke();
        }
    }
}
