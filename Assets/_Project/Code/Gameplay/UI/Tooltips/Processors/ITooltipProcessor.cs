using System;

namespace _Project.Code.Gameplay.UI.Tooltips
{
    public interface ITooltipProcessor
    {
        Type TokenType { get; }
        string BuildText(ITooltipToken token);
    }
}
