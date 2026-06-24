using UnityEngine;

namespace _Project.Code.Gameplay.Weapons
{
    public sealed class PlayerWeaponView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private GameObject[] _weaponModels;

        public Vector3 MuzzlePosition => _muzzle != null ? _muzzle.position : transform.position;

        public void Equip(int index)
        {
            if (_weaponModels == null) return;

            for (var i = 0; i < _weaponModels.Length; i++)
            {
                if (_weaponModels[i] != null)
                    _weaponModels[i].SetActive(i == index);
            }
        }

        public void PlayAttack(string trigger)
        {
            if (_animator != null && !string.IsNullOrEmpty(trigger))
                _animator.SetTrigger(trigger);
        }
    }
}
