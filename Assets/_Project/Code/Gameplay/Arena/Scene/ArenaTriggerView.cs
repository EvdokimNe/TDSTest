using System;
using _Project.Code.Gameplay.PlayerMovement;
using UnityEngine;

namespace _Project.Code.Gameplay.Arena
{
    public sealed class ArenaTriggerView : MonoBehaviour
    {
        public event Action PlayerEntered;

        private bool _fired;

        private void OnTriggerEnter(Collider other)
        {
            if (_fired) return;
            if (other.GetComponent<PlayerView>() == null) return;
            _fired = true;
            PlayerEntered?.Invoke();
        }
    }
}
