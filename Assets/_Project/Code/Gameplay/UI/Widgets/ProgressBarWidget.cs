using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Gameplay.UI
{
    public sealed class ProgressBarWidget : MonoBehaviour
    {
        [SerializeField] private Image _fill;

        public void SetProgress(float normalized)
        {
            if (_fill != null)
                _fill.fillAmount = Mathf.Clamp01(normalized);
        }
    }
}
