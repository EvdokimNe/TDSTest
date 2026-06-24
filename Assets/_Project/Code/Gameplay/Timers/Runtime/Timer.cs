using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Timers
{
    public sealed class Timer : IDisposable
    {
        private readonly TimerRunner _timerRunner;

        private const float MinDuration = 0.0001f;

        private float _maxTimeSeconds;
        private float _elapsedTime;
        private bool _isRunning;
        private bool _isPaused;

        public bool IsFinished { get; private set; }

        public Timer(TimerRunner timerRunner)
        {
            _timerRunner = timerRunner;
            _timerRunner.Add(this);
        }

        public void Setup(float maxTimeSeconds)
        {
            _maxTimeSeconds = maxTimeSeconds <= 0f ? MinDuration : maxTimeSeconds;
        }

        public void Restart()
        {
            _elapsedTime = 0f;
            IsFinished = false;
            _isPaused = false;
            _isRunning = true;
        }

        public void Pause() => _isPaused = true;

        public void Stop()
        {
            _isRunning = false;
            _isPaused = false;
            _elapsedTime = 0f;
            IsFinished = false;
        }

        public void Tick()
        {
            if (!_isRunning || _isPaused || IsFinished)
                return;

            _elapsedTime += Time.deltaTime;

            if (_elapsedTime < _maxTimeSeconds)
                return;

            IsFinished = true;
            _isRunning = false;
        }

        public void Dispose()
        {
            _timerRunner.Remove(this);
        }
    }
}
