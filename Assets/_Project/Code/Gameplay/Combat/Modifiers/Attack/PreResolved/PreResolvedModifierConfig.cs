using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Combat
{
    public abstract class PreResolvedModifierConfig : ScriptableObject
    {
    }
    
    public interface IPreResolvedModifierProcessor
    {
        Type ConfigType { get; }
        void Apply(PreResolvedModifierConfig config, DamageResolutionContext context);
    }
}
