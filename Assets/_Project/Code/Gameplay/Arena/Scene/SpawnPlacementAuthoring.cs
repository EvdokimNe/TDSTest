using UnityEngine;

namespace _Project.Code.Gameplay.Arena
{
    public abstract class SpawnPlacementAuthoring : MonoBehaviour
    {
        public abstract Vector3 NextPoint();
    }
}
