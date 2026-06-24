using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Input;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace _Project.Code.Gameplay.UI.Tooltips
{
    public sealed class TooltipService : ITickable, IDisposable
    {
        private readonly InputService _input;
        private readonly TooltipView _view;
        private readonly Canvas _canvas;
        private readonly Dictionary<Type, ITooltipProcessor> _processors = new();

        private ITooltipToken _current;
        private RectTransform _anchor;

        public TooltipService(
            InputService input,
            TooltipView viewPrefab,
            UiCanvasLayersProvider canvasLayersProvider,
            IEnumerable<ITooltipProcessor> processors)
        {
            _input = input;
            _canvas = canvasLayersProvider.UiLayerCanvas;

            _view = Object.Instantiate(viewPrefab, _canvas.transform, false);
            _view.Hide();

            foreach (var processor in processors)
                _processors[processor.TokenType] = processor;
        }

        public void SetCurrent(ITooltipToken token, RectTransform anchor)
        {
            _current = token;
            _anchor = anchor;
        }

        public void Tick()
        {
            if (!IsHovering() || !_input.IsAltHeld)
            {
                _view.Hide();
                return;
            }

            if (!_processors.TryGetValue(_current.GetType(), out var processor))
            {
                _view.Hide();
                return;
            }

            _view.Show(processor.BuildText(_current), _anchor);
        }

        private bool IsHovering()
        {
            if (_current == null || _anchor == null)
                return false;

            var cam = _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera;
            return RectTransformUtility.RectangleContainsScreenPoint(_anchor, _input.MousePosition, cam);
        }

        public void Dispose()
        {
            if (_view != null)
                Object.Destroy(_view.gameObject);
        }
    }
}
