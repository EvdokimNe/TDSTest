using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Combat
{
    public abstract class DefenseModifierConfig : ScriptableObject
    {
        public abstract string GetDescription();
    }

    public interface IDefenseModifierProcessor
    {
        Type ConfigType { get; }
        void Apply(DefenseModifierConfig config, DamageContext context);
    }
}
