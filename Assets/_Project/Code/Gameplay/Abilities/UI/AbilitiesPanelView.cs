using UnityEngine;
namespace _Project.Code.Gameplay.Abilities
{
    public class AbilitiesPanelView : MonoBehaviour
    {
        [SerializeField] private SlotWidget _slotWidget;
        
        public SlotWidget GetNewView()
        {
            return Instantiate(_slotWidget, transform, false);
        }
    }
}
