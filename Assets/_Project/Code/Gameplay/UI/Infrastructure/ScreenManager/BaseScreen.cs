using Cysharp.Threading.Tasks;

namespace _Project.Code.Gameplay.UI.Infrastructure
{
    public abstract class BaseScreen
    {
        private UniTaskCompletionSource _closeSource;

        internal abstract void BindView(BaseView view);

        internal UniTask RunAsync()
        {
            _closeSource = new UniTaskCompletionSource();
            OnOpen();
            return _closeSource.Task;
        }

        protected void RequestClose()
        {
            OnClose();
            _closeSource?.TrySetResult();
        }

        protected virtual void OnOpen() { }
        protected virtual void OnClose() { }
    }

    public abstract class BaseScreen<TView> : BaseScreen where TView : BaseView
    {
        protected TView View { get; private set; }

        internal override void BindView(BaseView view) => View = (TView)view;
    }

    public abstract class BaseScreen<TView, TArgs> : BaseScreen<TView>, IScreenWithArgs<TArgs>
        where TView : BaseView
        where TArgs : IScreenArgs
    {
        protected TArgs Args { get; private set; }

        void IScreenWithArgs<TArgs>.SetArgs(TArgs args) => Args = args;
    }
}
