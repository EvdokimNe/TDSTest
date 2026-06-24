using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Combat
{
    public abstract class OnHitModifierConfig : ScriptableObject
    {
    }

    public interface IOnHitModifierProcessor
    {
        Type ConfigType { get; }
        void Apply(OnHitModifierConfig config, DamageContext context);
    }
}
