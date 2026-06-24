using UnityEngine;

namespace _Project.Code.Gameplay.Abilities
{
    public sealed class CastHintView : MonoBehaviour
    {
        public void Show()
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}
