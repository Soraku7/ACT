using System;
using UnityEngine;

namespace Unilts.Timer
{
    /// <summary>
    /// 计时器状态
    /// </summary>
    public enum TimerState
    {
        Notworker,
        Working,
        Done
    }

    public class GameTimer
    {
        private float _startTime;
        private Action _task;
        private bool _isStopTimer;
        private TimerState _timerState;

        public GameTimer()
        {
            ResetTimer();
        }

        /// <summary>
        /// 开始计时器
        /// </summary>
        /// <param name="time"></param>
        /// <param name="task"></param>
        public void StartTimer(float time, Action task)
        {
            _startTime = time;
            _task = task;
            _isStopTimer = false;
            _timerState = TimerState.Working;
        }

        /// <summary>
        /// 更新计时器
        /// </summary>
        public void UpdateTimer()
        {
            if (_isStopTimer) return;
            _startTime -= Time.deltaTime;
            if (_startTime < 0f)
            {
                _task?.Invoke();
                _timerState = TimerState.Done;
                _isStopTimer = true;
            }
        }

        public TimerState GetTimerState() => _timerState;

        public void ResetTimer()
        {
            _startTime = 0f;
            _task = null;
            _isStopTimer = true;
            _timerState = TimerState.Notworker;
        }
    }
}