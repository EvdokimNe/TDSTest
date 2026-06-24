using System;
using System.Text;
using _Project.Code.Gameplay.Player;

namespace _Project.Code.Gameplay.UI.Tooltips
{
    public sealed class PlayerHealthTooltipProcessor : ITooltipProcessor
    {
        private readonly PlayerProvider _player;
        private readonly StringBuilder _builder = new();

        public PlayerHealthTooltipProcessor(PlayerProvider player) => _player = player;

        public Type TokenType => typeof(PlayerHealthToken);

        public string BuildText(ITooltipToken token)
        {
            var state = _player.CombatEntityState;
            if (state?.Health == null)
                return "HP: —";

            _builder.Clear();
            _builder.Append($"MAX HP: {state.Health.Max:0}");

            var modifiers = state.DefenseModifiers;
            if (modifiers != null)
            {
                for (var i = 0; i < modifiers.Count; i++)
                {
                    if (modifiers[i] == null) continue;

                    _builder.Append('\n');
                    _builder.Append(modifiers[i].GetDescription());
                }
            }

            return _builder.ToString();
        }
    }
}
