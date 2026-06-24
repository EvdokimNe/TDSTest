namespace _Project.Code.Gameplay.Pause
{
    public sealed class PauseManager
    {
        private int _pauseCount;

        public bool IsPaused => _pauseCount > 0;

        public void Pause() => _pauseCount++;

        public void Resume()
        {
            if (_pauseCount > 0)
                _pauseCount--;
        }
    }
}
