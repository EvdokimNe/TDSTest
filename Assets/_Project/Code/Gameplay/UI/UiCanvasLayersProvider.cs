using UnityEngine;
namespace _Project.Code.Gameplay.UI
{
    public class UiCanvasLayersProvider : MonoBehaviour
    {
        [SerializeField] private Canvas _uiLayerCanvas;
        [SerializeField] private Canvas _hpBarLayerCanvas;

        public Canvas UiLayerCanvas => _uiLayerCanvas;
        public Canvas HpBarLayerCanvas => _hpBarLayerCanvas;
    }
}
