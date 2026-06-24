using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer.Unity;

namespace _Project.Code.Infrastructure.Configs
{
    public sealed class ConfigService
    {
        public bool Inited;
        
        private readonly Dictionary<string, AsyncOperationHandle<IList<ScriptableObject>>> _handlesByGroup = new();
        private readonly Dictionary<string, List<ScriptableObject>> _configsByGroup = new();
        private readonly List<ScriptableObject> _configs = new();
        
        public async UniTask<bool> LoadAsync(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                Debug.LogError("[ConfigService] Config group name is empty.");
                return false;
            }

            if (_handlesByGroup.ContainsKey(groupName))
                return true;

            var handle = Addressables.LoadAssetsAsync<ScriptableObject>(groupName, null);

            while (!handle.IsDone)
                await UniTask.Yield();

            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"[ConfigService] Failed to load config group '{groupName}'.");
                Addressables.Release(handle);
                return false;
            }

            if (handle.Result.Count == 0)
            {
                Debug.LogError($"[ConfigService] Config group '{groupName}' is empty.");
            }

            RegisterGroup(groupName, handle);
            Inited = true;
            return true;
        }

        public void Release(string groupName)
        {
            if (!_handlesByGroup.TryGetValue(groupName, out var handle))
                return;

            UnregisterGroup(groupName);
            Addressables.Release(handle);
            _handlesByGroup.Remove(groupName);
        }

        public bool TryGet<TConfig>(out TConfig config) where TConfig : ScriptableObject
        {
            config = null;

            var count = 0;
            TConfig found = null;

            for (var i = 0; i < _configs.Count; i++)
            {
                if (_configs[i] is not TConfig typedConfig)
                    continue;

                found = typedConfig;
                count++;
            }

            if (count == 1)
            {
                config = found;
                return true;
            }

            if (count > 1)
                Debug.LogError($"[ConfigService] Expected one config of type {typeof(TConfig).Name}, found {count}. Use TryGetAll instead.");

            return false;
        }

        public bool TryGetAll<TConfig>(out IReadOnlyList<TConfig> configs) where TConfig : ScriptableObject
        {
            var result = new List<TConfig>();

            for (var i = 0; i < _configs.Count; i++)
            {
                if (_configs[i] is TConfig typedConfig)
                    result.Add(typedConfig);
            }

            configs = result;
            return result.Count > 0;
        }

        private void RegisterGroup(string groupName, AsyncOperationHandle<IList<ScriptableObject>> handle)
        {
            var groupConfigs = new List<ScriptableObject>(handle.Result.Count);

            for (var i = 0; i < handle.Result.Count; i++)
            {
                var config = handle.Result[i];
                if (config == null)
                    continue;

                groupConfigs.Add(config);
                _configs.Add(config);
            }

            _handlesByGroup[groupName] = handle;
            _configsByGroup[groupName] = groupConfigs;
        }

        private void UnregisterGroup(string groupName)
        {
            if (!_configsByGroup.TryGetValue(groupName, out var groupConfigs))
                return;

            for (var i = 0; i < groupConfigs.Count; i++)
                _configs.Remove(groupConfigs[i]);

            _configsByGroup.Remove(groupName);
        }
    }
}
