using System;
using UnityEngine;
namespace _Project.Code.Gameplay.Enemies.Providers
{
    [Serializable]
    public class ShootPointProvider : IEnemyAdditionalParamsProvider
    {
        [SerializeField] private Transform _transform;
        public Vector3 Pos => _transform.position;
    }
}
