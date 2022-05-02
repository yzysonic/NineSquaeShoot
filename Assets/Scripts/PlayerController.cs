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

        // Start is called before the first frame update
        private void Awake()
        {
            input = new GameInput();
            input.Player.SetCallbacks(this);
            input.Enable();

            movement = GetComponent<CharacterMovement>();
        }

        void GameInput.IPlayerActions.OnFire(InputAction.CallbackContext context)
        {
            
        }

        void GameInput.IPlayerActions.OnMoveUp(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                movement.TryMove(Vector2.up);
            }
        }

        void GameInput.IPlayerActions.OnMoveDown(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                movement.TryMove(Vector2.down);
            }
        }

        void GameInput.IPlayerActions.OnMoveLeft(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                movement.TryMove(Vector2.left);
            }
        }

        void GameInput.IPlayerActions.OnMoveRight(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                movement.TryMove(Vector2.right);
            }
        }
    }
}
