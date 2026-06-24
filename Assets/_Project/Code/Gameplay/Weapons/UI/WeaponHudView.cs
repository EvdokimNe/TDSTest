using TMPro;
using UnityEngine;

namespace _Project.Code.Gameplay.Weapons.UI
{
    public sealed class WeaponHudView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _weaponText;

        public void SetWeapon(string weaponName) => _weaponText.text = weaponName;
    }
}
