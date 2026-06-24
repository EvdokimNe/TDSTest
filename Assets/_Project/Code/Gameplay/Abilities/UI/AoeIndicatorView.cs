using UnityEngine;

namespace _Project.Code.Gameplay.Abilities
{
    [RequireComponent(typeof(LineRenderer))]
    public sealed class AoeIndicatorView : MonoBehaviour
    {
        [SerializeField] private int _segments = 48;
        [SerializeField] private float _lineWidth = 0.1f;
        [SerializeField] private Color _color = new(0.2f, 0.8f, 1f, 0.8f);
        [SerializeField] private LineRenderer _line;

        private float _builtRadius = -1f;
        private bool _configured;

        public void Show(Vector3 position, float radius)
        {
            EnsureConfigured();

            if (!gameObject.activeSelf)
                gameObject.SetActive(true);

            transform.position = position;

            if (!Mathf.Approximately(radius, _builtRadius))
                RebuildRing(radius);
        }

        public void Hide()
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }

        private void EnsureConfigured()
        {
            if (_configured) return;
            _configured = true;

            _line.useWorldSpace = false;
            _line.loop = true;
            _line.widthMultiplier = _lineWidth;
            _line.startColor = _color;
            _line.endColor = _color;
            _line.positionCount = _segments;
        }

        private void RebuildRing(float radius)
        {
            _builtRadius = radius;

            if (_line.positionCount != _segments)
                _line.positionCount = _segments;

            var step = Mathf.PI * 2f / _segments;
            for (var i = 0; i < _segments; i++)
            {
                var angle = step * i;
                _line.SetPosition(i, new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius));
            }
        }
    }
}
