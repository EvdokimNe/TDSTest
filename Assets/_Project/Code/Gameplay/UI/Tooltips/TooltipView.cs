using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Gameplay.UI.Tooltips
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class TooltipView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _verticalPadding = 8f;

        private Canvas _canvas;
        private RectTransform _canvasRect;
        private readonly Vector3[] _corners = new Vector3[4];

        private void Awake()
        {
            var canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        public void Show(string text, RectTransform anchor)
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);

            _text.text = text;
            PositionNearAnchor(anchor);
        }

        public void Hide()
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        private void PositionNearAnchor(RectTransform anchor)
        {
            EnsureCanvas();

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rect);

            var cam = _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera;

            anchor.GetWorldCorners(_corners);
            for (var i = 0; i < 4; i++)
                _corners[i] = RectTransformUtility.WorldToScreenPoint(cam, _corners[i]);

            var anchorTopY = _corners[1].y;
            var anchorBottomY = _corners[0].y;
            var anchorCenterX = (_corners[0].x + _corners[2].x) * 0.5f;

            var w = _rect.rect.width;
            var h = _rect.rect.height;

            var fitsAbove = anchorTopY + _verticalPadding + h <= Screen.height;
            var y = fitsAbove
                ? anchorTopY + _verticalPadding + h * 0.5f
                : anchorBottomY - _verticalPadding - h * 0.5f;

            var x = Mathf.Clamp(anchorCenterX, w * 0.5f, Screen.width - w * 0.5f);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRect, new Vector2(x, y), cam, out var local);

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
