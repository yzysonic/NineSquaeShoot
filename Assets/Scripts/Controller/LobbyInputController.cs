using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NSS;

public class LobbyInputController : MonoBehaviour, GameInput.ILobbyPlayerActions
{

    private GameInput.LobbyPlayerActions playerInput;

    [SerializeField] private LobbyCharacter Character;

    private void OnEnable() {
        playerInput.Enable();
    }

    private void OnDisable() {
        playerInput.Disable();
    }

    private void OnDestroy() {
        playerInput.SetCallbacks(null);
    }

    void Awake() {
        playerInput = GameInputManager.Instance.Input.LobbyPlayer;
        playerInput.SetCallbacks(this);
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Vector2 vector2d = playerInput.Moving.ReadValue<Vector2>();
        if (vector2d != Vector2.zero && vector2d.x != 0 && !Character.LockMove) {
            Character.Move(vector2d);
        }
    }

    void GameInput.ILobbyPlayerActions.OnMoving(InputAction.CallbackContext context) {
        Vector2 CheckValue = context.ReadValue<Vector2>();
        if (context.started && CheckValue.x != 0 && !Character.LockMove) {
            Character.SetIsMoving(true);
            Character.CharacterAnimator.SetBool("CanRun", true);
            Character.transform.eulerAngles = new Vector3(0, (CheckValue.x < 0) ? 180 : 0, 0);
        }

        if (context.canceled && Character.IsMoving) {
            Character.SetIsMoving(false);
            Character.CharacterAnimator.SetBool("CanRun", false);
        }

        if (context.performed) {
            if (CheckValue.x == 0) {
                Character.SetIsMoving(false);
                Character.CharacterAnimator.SetBool("CanRun", false);
            }
            else {
                if (!Character.LockMove) {
                    Character.SetIsMoving(true);
                    Character.CharacterAnimator.SetBool("CanRun", true);
                    Character.transform.eulerAngles = new Vector3(0, (CheckValue.x < 0) ? 180 : 0, 0);
                }
            }
        }

        if (context.started && LobbyUIController.Instance.IsShowPopupUI) {
            LobbyUIController.Instance.SendGridMoveButtonClickedEvent(CheckValue);
        }
    }

    public void OnOpenUI(InputAction.CallbackContext context) {
        if (context.started) {
            if (!LobbyUIController.Instance.IsShowPopupUI && !LobbyUIController.Instance.IsReturnMainMenu) {
                if (Character.IsinUICollider) {
                    Character.SetLockMove(true);
                    LobbyUIController.Instance.IsShowPopupUI = true;
                    LobbyUIController.Instance.SendPopupUIButtonClickedEvent();
                }
            }
            else {
                if (LobbyUIController.Instance.IsShowPopupUI) {
                    LobbyUIController.Instance.SendChangeButtonClickedEvent();
                }

                if (LobbyUIController.Instance.IsReturnMainMenu) {
                    LobbyUIController.Instance.IsReturnMainMenu = false;
                    LobbyUIController.Instance.SendPopupUIButtonClickedEvent();
                }
            }
        }
    }

    public void OnCloseUI(InputAction.CallbackContext context) {
        if (context.started) {
            if (LobbyUIController.Instance.IsShowPopupUI) {
                Character.SetLockMove(false);
                LobbyUIController.Instance.IsShowPopupUI = false;
                LobbyUIController.Instance.SendCanaelButtonClickedEvent();
            }
        }
    }

    void GameInput.ILobbyPlayerActions.OnConfirmUI(InputAction.CallbackContext context) {
        if (context.started) {
            Character.SetLockMove(false);
            LobbyUIController.Instance.IsShowPopupUI = false;
            LobbyUIController.Instance.SendConfirmButtonClickedEvent();
        }
    }

    public void OnMoveUI(InputAction.CallbackContext context) {
        if (context.started) {
            LobbyUIController.Instance.SendUILabelChangeButtonClickedEvent(context.ReadValue<float>());
        }
    }
}
