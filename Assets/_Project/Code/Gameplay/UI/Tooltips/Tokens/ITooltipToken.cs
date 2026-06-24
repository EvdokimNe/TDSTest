namespace _Project.Code.Gameplay.UI.Tooltips
{
    public interface ITooltipToken { }

    public sealed class AbilitySlotToken : ITooltipToken
    {
        public int SlotIndex;
    }

    public sealed class PlayerHealthToken : ITooltipToken { }
}
