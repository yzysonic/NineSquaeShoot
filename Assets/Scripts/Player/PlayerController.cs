using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NSS
{
    public class PlayerController : MonoBehaviour, GameInput.IPlayerActions
    {
        private GameInput input;
        private CharacterMovement movement;
        private Weapon weapon;

        // Start is called before the first frame update
        private void Awake()
        {
            input = new GameInput();
            input.Player.SetCallbacks(this);

            movement = GetComponent<CharacterMovement>();
            weapon = GetComponent<Weapon>();

            GameUIManager.Instance.Pause.PauseStateChanged += value =>
            {
                if (value)
                {
                    input.Player.Disable();
                }
                else
                {
                    input.Player.Enable();
                }
            };
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }

        private void OnDestroy()
        {
            input.Player.SetCallbacks(null);
        }

        void GameInput.IPlayerActions.OnFire(InputAction.CallbackContext context)
        {
            if (weapon != null)
            {
                if (context.started)
                {
                    weapon.StartRapidFire();
                }
                else if (context.canceled)
                {
                    weapon.StopRapidFire();
                }
            }
        }

        void GameInput.IPlayerActions.OnMoveUp(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                movement.TryMove(EMoveDirection.Upper);
            }
        }

        void GameInput.IPlayerActions.OnMoveDown(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                movement.TryMove(EMoveDirection.Lower);
            }
        }

        void GameInput.IPlayerActions.OnMoveLeft(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                movement.TryMove(EMoveDirection.Left);
            }
        }

        void GameInput.IPlayerActions.OnMoveRight(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                movement.TryMove(EMoveDirection.Right);
            }
        }

        void GameInput.IPlayerActions.OnPause(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                GameUIManager.Instance.SetPauseActive(true);
            }
        }
    }
}
