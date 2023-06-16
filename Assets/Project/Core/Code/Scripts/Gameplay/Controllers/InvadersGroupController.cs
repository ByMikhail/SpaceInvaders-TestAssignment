using SpaceInvaders.Foundation.TinyGameplayFramework;
using SpaceInvaders.Gameplay.Components;
using SpaceInvaders.Gameplay.Units;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Controllers
{
    public class InvadersGroupController : GameplayUnitController<InvadersGroup>
    {
        [SerializeField] private float _fireRate = 1f;

        private readonly Timer _firingTimer = new();

        protected override void TargetBasedUpdate()
        {
            if (_firingTimer.TimeIsOut)
            {
                Target.Fire();

                _firingTimer.Restart(_fireRate);
            }
        }
    }
}