using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Projectiles
{
    [CreateAssetMenu(menuName = "TDS/Projectiles/Projectile Config")]
    public sealed class ProjectileConfig : ScriptableObject
    {
        public ProjectileView Prefab;
        public float Speed;
        public float Lifetime;
        public List<ProjectileModifierConfig> Modifiers = new();
    }
}
