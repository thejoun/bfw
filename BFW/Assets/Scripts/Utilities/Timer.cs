using System;
using Unimorph.Injection;
using UnityEngine;
using Zenject;

namespace Utilities
{
    public class Timer : ITickable
    {
        private float duration;
        private Action callback;
        
        private bool isLooping;
        private bool isRunning;

        private float time;
        
        public static Timer RunNew()
        {
            return new Timer().Run();
        }

        public Timer WithDuration(float duration)
        {
            isLooping = false;
            this.duration = duration;
            return this;
        }
        
        public Timer WithInterval(float interval)
        {
            isLooping = true;
            duration = interval;
            return this;
        }

        public Timer WithCallback(Action callback)
        {
            this.callback = callback;
            return this;
        }

        private Timer Run()
        {
            ManualInject.Into(this);
            isRunning = true;
            return this;
        }
        
        public void Tick()
        {
            if (!isRunning) return;
            
            time += Time.deltaTime;
            
            while (time >= duration)
            {
                OnGoalReached();
            }
        }

        private void OnGoalReached()
        {
            if (isLooping)
            {
                time -= duration;
            }
            else
            {
                isRunning = false;
                time = 0f;
            }
            
            callback?.Invoke();
        }
    }
}