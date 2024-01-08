using System;
using System.Collections.Generic;
using Unilts.Timer;
using Unilts.Tools.Singleton;
using UnityEngine;

namespace Manager
{
    public class TimeManager : Singleton<TimeManager>
    {
        [SerializeField] private int _initMaxTimerCount = 0;

        private Queue<GameTimer> _noWorkTimer = new Queue<GameTimer>();
        private List<GameTimer> _workingTimer = new List<GameTimer>();

        private void Start()
        {
            InitTimerManager();
        }

        private void Update()
        {
            UpdateWorkingTimer();
        }

        private void InitTimerManager()
        {
            for (int i = 0; i < _initMaxTimerCount; i++)
            {
                CreatTimer();
            }
        }

        private void CreatTimer()
        {
            var timer = new GameTimer();
            _noWorkTimer.Enqueue(timer);
        }
        
        public void TryGetOneTimer(float time , Action task)
        {
            if (_noWorkTimer.Count == 0)
            {
                CreatTimer();
                var timer = _noWorkTimer.Dequeue();
                timer.StartTimer(time , task);
                _workingTimer.Add(timer);
            }
            else
            {
                var timer = _noWorkTimer.Dequeue();
                timer.StartTimer(time , task);
                _workingTimer.Add(timer);
            }
        }

        private void UpdateWorkingTimer()
        {
            if (_workingTimer.Count == 0) return;
            for (var i = 0; i < _workingTimer.Count; i++)
            {
                if (_workingTimer[i].GetTimerState() == TimerState.Working)
                {
                    _workingTimer[i].UpdateTimer();
                }
                else
                {
                    //任务完成
                    _noWorkTimer.Enqueue(_workingTimer[i]);
                    _workingTimer[i].ResetTimer();
                    _workingTimer.Remove(_workingTimer[i]);

                }
            }
        }
    }
}