using _Project.Code.Gameplay.Combat;
using UnityEngine;

namespace _Project.Code.Gameplay.HitDetection
{
    /// <summary>
    /// Композиционный модуль «может получить урон». Владелец (враг, игрок, снаряд)
    /// держит его у себя и сам регистрирует/снимает в <see cref="HittableRegistry"/>.
    /// Плоские данные: боевая сущность + трансформ для актуальной позиции + радиус.
    /// </summary>
    public sealed class HittableModule
    {
        public ICombatEntity Entity { get; }
        public Transform Body { get; }
        public float Radius { get; set; }

        public Vector3 Position => Body.position;

        public HittableModule(ICombatEntity entity, Transform body, float radius)
        {
            Entity = entity;
            Body = body;
            Radius = radius;
        }
    }
}
