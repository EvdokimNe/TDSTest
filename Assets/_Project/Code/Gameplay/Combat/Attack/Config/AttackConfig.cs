using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Combat
{
    [CreateAssetMenu(menuName = "TDS/Combat/Attack Profile")]
    public sealed class AttackConfig : ScriptableObject
    {
        public float BaseDamage;
        public DamageType DamageType;
        public DamageTags DamageTags;
        public List<PreResolvedModifierConfig> PreResolvedModifiers = new();
        public List<OnHitModifierConfig> OnHitModifiers = new();
    }
}
