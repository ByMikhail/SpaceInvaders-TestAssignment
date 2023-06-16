using System;

namespace SpaceInvaders.Gameplay.Units.AbilityInterfaces
{
    public interface IKillable
    {
        bool IsKilled { get; }
        event EventHandler OnKilled;
        void Kill();
    }
}