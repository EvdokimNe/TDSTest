using System;
#if UNITY_EDITOR
using System.IO;
#endif
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Code.Infrastructure.SceneLoading
{
    public sealed class SceneLoadingService
    {
        private bool _isLoading;

        public async UniTask LoadSceneAsync(string sceneName, Action<float> progressChanged = null)
        {
            if (_isLoading) return;
#if UNITY_EDITOR
            if (!CanStartLoadingScene(SceneNames.Empty)) return;
            if (!CanStartLoadingScene(sceneName)) return;
#endif

            _isLoading = true;

            try
            {
                progressChanged?.Invoke(0f);

                await LoadSingleAsync(SceneNames.Empty, progress => progressChanged?.Invoke(progress * 0.2f));
                await LoadSingleAsync(sceneName, progress => progressChanged?.Invoke(0.2f + progress * 0.8f));
            }
            finally
            {
                _isLoading = false;
            }
        }

        private static async UniTask LoadSingleAsync(string sceneName, Action<float> progressChanged)
        {
            var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            if (operation == null)
            {
                Debug.LogError($"[SceneLoadingService] Unity failed to start loading scene '{sceneName}'. Check Build Settings.");
                return;
            }

            while (!operation.isDone)
            {
                progressChanged?.Invoke(operation.progress);
                await UniTask.Yield();
            }

            progressChanged?.Invoke(1f);
        }

#if UNITY_EDITOR
        private static bool CanStartLoadingScene(string sceneName)
        {
            return true;

            if (string.IsNullOrWhiteSpace(sceneName))
            {
                Debug.LogError("[SceneLoadingService] Scene name is empty.");

                return false;
            }

            if (IsSceneInBuildSettings(sceneName))
                return true;

            Debug.LogError($"[SceneLoadingService] Scene '{sceneName}' is not available. Add it to Build Settings or fix SceneNames.");
            return false;

        }

#endif

#if UNITY_EDITOR
        private static bool IsSceneInBuildSettings(string sceneName)
        {
            for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                var buildSceneName = Path.GetFileNameWithoutExtension(scenePath);
                if (string.Equals(buildSceneName, sceneName, StringComparison.Ordinal))
                    return true;
            }

            return false;
        }
#endif
    }
}
