using _Project.Code.Gameplay.UI;
using UnityEngine;

namespace _Project.Code.Gameplay.Enemies.HealthBars
{
    public sealed class EnemyHealthBarView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private ProgressBarWidget _progressBar;

        public void SetWorldPosition(Vector3 worldPosition) => _rect.position = worldPosition;

        public void SetProgress(float normalized) => _progressBar.SetProgress(normalized);
    }
}
