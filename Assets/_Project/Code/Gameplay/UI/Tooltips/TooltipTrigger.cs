using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Code.Gameplay.UI.Tooltips
{
    public sealed class TooltipTrigger : MonoBehaviour, IPointerEnterHandler
    {
        private TooltipService _service;
        private ITooltipToken _token;
        private RectTransform _rect;

        public void Init(TooltipService service, ITooltipToken token)
        {
            _service = service;
            _token = token;
            _rect = transform as RectTransform;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_service == null) return;
            _service.SetCurrent(_token, _rect);
        }
    }
}
