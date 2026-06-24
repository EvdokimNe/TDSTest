using Cysharp.Threading.Tasks;

namespace _Project.Code.Application.Loading
{
    public sealed class LoadingScreenController
    {
        private readonly LoadingScreenView _view;

        private UniTaskCompletionSource _retrySource;

        public bool IsVisible { get; private set; }

        public LoadingScreenController(LoadingScreenView loadingScreenView) => _view = loadingScreenView;

        public void Start()
        {
            IsVisible = true;
            UnityEngine.Cursor.visible = true;

            _view.SetRetryVisible(false);
            _view.SetVisible(true);
            _view.RetryClicked += OnRetryClicked;
        }

        public void Stop()
        {
            IsVisible = false;
            _view.RetryClicked -= OnRetryClicked;
            _view.SetVisible(false);
        }

        public void SetProgress(float p) => _view.SetProgress(p);

        public void SetStatusText(string text) => _view.SetStatus(text);

        public void ShowRetryButton(string message)
        {
            _view.SetStatus(message);
            _retrySource = new UniTaskCompletionSource();
            _view.SetRetryVisible(true);
        }

        public UniTask WaitForRetryAsync() => _retrySource?.Task ?? UniTask.CompletedTask;

        private void OnRetryClicked()
        {
            _view.SetRetryVisible(false);
            _retrySource?.TrySetResult();
        }
    }
}
