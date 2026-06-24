using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Projectiles
{
    public abstract class ProjectileModifierConfig : ScriptableObject
    {
    }

    public interface IProjectileModifierProcessor
    {
        Type ConfigType { get; }
        void Apply(ProjectileModifierConfig config, ProjectileModificationContext context);
    }
}
