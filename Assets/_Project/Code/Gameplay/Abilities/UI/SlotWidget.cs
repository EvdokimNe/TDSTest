using _Project.Code.Gameplay.UI;
using _Project.Code.Gameplay.UI.Tooltips;
using R3;
using TMPro;
using UnityEngine;
namespace _Project.Code.Gameplay.Abilities
{
    public class SlotWidget : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _barWidget;
        [SerializeField] private TextMeshProUGUI _abilityNameText;
        [SerializeField] private TextMeshProUGUI _abilityStatusText;
        [SerializeField] private TooltipTrigger _tooltipTrigger;

        public TooltipTrigger TooltipTrigger => _tooltipTrigger;

        public void SetPhase(AbilityPhase phase)
        {
            switch (phase)
            {
                case AbilityPhase.Ready:
                    _abilityStatusText.text = "Готово";
                    break;
                case AbilityPhase.OnCooldown:
                    _abilityStatusText.text = "Не готово";
                    break;
                case AbilityPhase.Targeting:
                    _abilityStatusText.text = "ЛКМ/ПКМ";
                    break;
            }
        }

        public void Init(string abilityName)
        {
            _abilityNameText.text = abilityName;
        }
        
        public void SetCooldown01(float f)
        {
            _barWidget.SetProgress(1f - f);
        }
    }
}
