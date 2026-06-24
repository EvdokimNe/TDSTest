using System;

namespace _Project.Code.Gameplay.Abilities
{
    public interface IAbilityProcessor
    {
        Type ConfigType { get; }
        void Execute(AbilityConfig config, AbilityContext ctx);
    }
}
