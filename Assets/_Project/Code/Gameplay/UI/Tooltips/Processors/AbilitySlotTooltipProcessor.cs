using System;
using _Project.Code.Gameplay.Abilities;

namespace _Project.Code.Gameplay.UI.Tooltips
{
    public sealed class AbilitySlotTooltipProcessor : ITooltipProcessor
    {
        private readonly AbilitySystem _abilitySystem;

        public AbilitySlotTooltipProcessor(AbilitySystem abilitySystem) => _abilitySystem = abilitySystem;

        public Type TokenType => typeof(AbilitySlotToken);

        public string BuildText(ITooltipToken token)
        {
            var index = ((AbilitySlotToken)token).SlotIndex;
            var slots = _abilitySystem.Slots;

            if (index < 0 || index >= slots.Count)
                return string.Empty;

            var config = slots[index].Config;
            return $"{config.DisplayName}\n{config.Description}";
        }
    }
}
