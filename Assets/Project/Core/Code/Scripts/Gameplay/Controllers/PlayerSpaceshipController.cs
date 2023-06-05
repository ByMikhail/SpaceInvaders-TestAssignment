using SpaceInvaders.Foundation.TinyGameplayFramework;
using SpaceInvaders.Gameplay.Units;
using UnityEngine;

namespace SpaceInvaders.Gameplay.Controllers
{
    public class PlayerSpaceshipController : GameplayUnitController<Spaceship>
    {
        private Vector2 _movementInput;

        protected override void TargetBasedUpdate()
        {
            _movementInput = new Vector2(Input.GetAxisRaw(InputConstants.HorizontalAxis), Input.GetAxisRaw(InputConstants.VerticalAxis));

            if (Input.GetButtonDown(InputConstants.FireButton))
            {
                Target.Fire();
            }
        }

        protected override void TargetBasedFixedUpdate()
        {
            if (Mathf.Abs(_movementInput.sqrMagnitude) <= Mathf.Epsilon) return;

            Target.Move(_movementInput);
        }
    }
}