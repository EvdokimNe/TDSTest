using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Project.Code.Gameplay.UI.Cursor
{
    public sealed class CursorInstaller : MonoBehaviour
    {
        [SerializeField] private CursorView _cursorPrefab;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_cursorPrefab);
            builder.RegisterEntryPoint<CustomCursorService>();
        }
    }
}
