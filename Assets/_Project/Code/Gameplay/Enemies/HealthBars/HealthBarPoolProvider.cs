using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.UI;
using UnityEngine;
using uPools;
using Object = UnityEngine.Object;

namespace _Project.Code.Gameplay.Enemies.HealthBars
{
    public sealed class HealthBarPoolProvider : IDisposable
    {
        private readonly ObjectPool<EnemyHealthBarView> _pool;
        private readonly List<EnemyHealthBarView> _created = new();

        public HealthBarPoolProvider(EnemyHealthBarView prefab, UiCanvasLayersProvider canvasLayersProvider)
        {
            _pool = new ObjectPool<EnemyHealthBarView>(
                createFunc: () =>
                {
                    var view = Object.Instantiate(prefab, canvasLayersProvider.HpBarLayerCanvas.transform);
                    _created.Add(view);
                    return view;
                },
                onRent: view => view.gameObject.SetActive(true),
                onReturn: view => view.gameObject.SetActive(false),
                onDestroy: view => Object.Destroy(view.gameObject));
        }

        public EnemyHealthBarView Rent() => _pool.Rent();

        public void Return(EnemyHealthBarView view) => _pool.Return(view);

        public void Dispose()
        {
            for (var i = 0; i < _created.Count; i++)
            {
                if (_created[i] != null)
                    Object.Destroy(_created[i].gameObject);
            }

            _created.Clear();
        }
    }
}
