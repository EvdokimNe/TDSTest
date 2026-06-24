using _Project.Code.Gameplay.UI;
using _Project.Code.Gameplay.UI.Tooltips;
using TMPro;
using UnityEngine;

namespace _Project.Code.Gameplay.Player.UI
{
    public sealed class PlayerHudView : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TooltipTrigger _tooltipTrigger;

        public TooltipTrigger TooltipTrigger => _tooltipTrigger;

        public void SetHealth(float current, float max)
        {
            _healthBar.SetProgress(max > 0f ? current / max : 0f);
            _healthText.text = $"{current:0} / {max:0}";
        }
    }
}
