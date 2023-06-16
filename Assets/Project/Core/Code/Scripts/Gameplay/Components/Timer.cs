using UnityEngine;

namespace SpaceInvaders.Gameplay.Components
{
    public class Timer
    {
        public bool TimeIsOut => Time.time >= _nextTimeStamp;

        private float _nextTimeStamp = 0f;

        public void Restart(float duration)
        {
            _nextTimeStamp = Time.time + duration;
        }
    }
}