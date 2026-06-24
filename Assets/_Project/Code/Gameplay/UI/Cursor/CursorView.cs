using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Gameplay.UI.Cursor
{
    public sealed class CursorView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private Image _image;

        private Canvas _canvas;
        private RectTransform _canvasRect;

        public void SetVisible(bool isVisible)
        {
            if (gameObject.activeSelf != isVisible)
                gameObject.SetActive(isVisible);
        }

        public void SetColor(Color color) => _image.color = color;

        public void UpdatePosition(Vector2 screenPosition)
        {
            EnsureCanvas();

            var cam = _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRect, screenPosition, cam, out var local);

            _rect.localPosition = local;
        }

        private void EnsureCanvas()
        {
            if (_canvas != null) return;

            _canvas = GetComponentInParent<Canvas>();
            _canvasRect = _canvas.transform as RectTransform;
        }
    }
}
