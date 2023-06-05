using System;

namespace SpaceInvaders.Gameplay.Units.AbilityInterfaces
{
    public interface IKillable
    {
        event EventHandler OnKilled;
        void Kill();
    }
}