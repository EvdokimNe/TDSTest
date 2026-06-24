using System;
using _Project.Code.Gameplay.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.Application.Loading
{
    public sealed class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _progressBar;
        [SerializeField] private TMP_Text _statusText;
        [SerializeField] private Button _retryButton;

        public event Action RetryClicked;

        private void OnEnable() => _retryButton.onClick.AddListener(OnRetryClicked);
        private void OnDisable() => _retryButton.onClick.RemoveListener(OnRetryClicked);

        public void SetVisible(bool isVisible) => gameObject.SetActive(isVisible);

        public void SetProgress(float progress) => _progressBar.SetProgress(Mathf.Clamp01(progress));

        public void SetStatus(string status) => _statusText.text = status;

        public void SetRetryVisible(bool isVisible) => _retryButton.gameObject.SetActive(isVisible);

        private void OnRetryClicked() => RetryClicked?.Invoke();
    }
}
