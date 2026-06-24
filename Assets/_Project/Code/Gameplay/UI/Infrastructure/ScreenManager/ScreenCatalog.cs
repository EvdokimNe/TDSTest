using System;
using System.Collections.Generic;

namespace _Project.Code.Gameplay.UI.Infrastructure
{
    public sealed class ScreenCatalog
    {
        private readonly Dictionary<Type, BaseView> _prefabs = new();

        public void Add<TScreen>(BaseView prefab) where TScreen : BaseScreen
        {
            _prefabs[typeof(TScreen)] = prefab;
        }

        public BaseView GetPrefab(Type screenType) => _prefabs[screenType];
    }
}
