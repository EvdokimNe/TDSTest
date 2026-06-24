using System;
using _Project.Code.Gameplay.UI.Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Gameplay.UI.VictoryDefeat
{
    public sealed class VictoryDefeatView : BaseView
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _btText;
        [SerializeField] private TextMeshProUGUI _descText;
        [SerializeField] private Button _continueButton;

        public Action ContinueClicked;

        private void Awake()
        {
            if (_continueButton != null)
                _continueButton.onClick.AddListener(OnContinueClicked);
        }

        private void OnDestroy()
        {
            if (_continueButton != null)
                _continueButton.onClick.RemoveListener(OnContinueClicked);
        }

        public void SetView(string titleText, string btText, string descText)
        {
            _title.text = titleText;
            _btText.text = btText;
            _descText.text = descText;
        }

        private void OnContinueClicked() => ContinueClicked?.Invoke();
    }
}
