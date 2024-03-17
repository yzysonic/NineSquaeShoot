using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NSS;

public class LobbyCharacter : MonoBehaviour, GameInput.ILobbyPlayerActions
{

    private GameInput.LobbyPlayerActions playerInput;
    
    private RectTransform _transform;
    [SerializeField] private RectTransform CharacterShadowTransform;
    [SerializeField] private RectTransform CharacterIdleTransform;
    [SerializeField] private RectTransform CharacterMoveTransform;

    private Animator CharacterAnimator;

    [SerializeField] private float Speed;

    private bool IsMoving;
    private bool LockMove;
    private bool LockLeft;
    private bool LockRight;

    [SerializeField] private LobbyUICollider CurrentShowTarget;

    void Awake() {
        CharacterAnimator = GetComponent<Animator>();
        _transform = GetComponent<RectTransform>();
        playerInput = GameInputManager.Instance.Input.LobbyPlayer;
        playerInput.SetCallbacks(this);
    }

    private void OnEnable() {
        playerInput.Enable();
    }

    private void OnDisable() {
        playerInput.Disable();
    }

    private void OnDestroy() {
        playerInput.SetCallbacks(null);
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Vector2 vector2d = playerInput.Moving.ReadValue<Vector2>();
        if (vector2d != Vector2.zero && vector2d.x != 0 && !LockMove) {
            if (vector2d.x < 0) {
                if (!LockLeft) {
                    _transform.anchoredPosition -= new Vector2(Speed * Time.deltaTime, 0);
                }
            }
            else {
                if (!LockRight) {
                    _transform.anchoredPosition += new Vector2(Speed * Time.deltaTime, 0);
                }
            }
        }
    }

    public void SetCanMove() {
        LockMove = false;
    }

    void GameInput.ILobbyPlayerActions.OnMoving(InputAction.CallbackContext context) {
        Vector2 CheckValue = context.ReadValue<Vector2>();
        if (context.started && CheckValue.x != 0 && !LockMove) {
            IsMoving = true;
            CharacterAnimator.SetBool("CanRun", true);
            _transform.eulerAngles = new Vector3(0, (CheckValue.x < 0) ? 180 : 0, 0);
        }

        if (context.canceled && IsMoving) {
            IsMoving = false;
            CharacterAnimator.SetBool("CanRun", false);
        }

        if (context.performed) {
            if (CheckValue.x == 0) {
                IsMoving = false;
                CharacterAnimator.SetBool("CanRun", false);
            }
            else {
                if (!LockMove) {
                    IsMoving = true;
                    CharacterAnimator.SetBool("CanRun", true);
                    _transform.eulerAngles = new Vector3(0, (CheckValue.x < 0) ? 180 : 0, 0);
                }
            }
        }
    }

    void GameInput.ILobbyPlayerActions.OnReturnMainMenu(InputAction.CallbackContext context) {
        if (context.started && CurrentShowTarget.Type == ColliderType.ReturnMainMenu) {
            _transform.parent.transform.parent.gameObject.SetActive(false);
        }
    }

    void GameInput.ILobbyPlayerActions.OnStartGame(InputAction.CallbackContext context) {
        if (context.started && CurrentShowTarget.Type == ColliderType.StartGame) {
             FadeManager.Instance.LoadSceneWithFade("MainGameScene");
        }
    }

    void GameInput.ILobbyPlayerActions.OnCharacterOpen(InputAction.CallbackContext context) {
        if (context.started && CurrentShowTarget != null) {
            if (CurrentShowTarget.Type == ColliderType.Character) {
                LockMove = true;
                PopupUIController.Instance.ShowPopupUIObj(0);
            }
        }
    }

    void GameInput.ILobbyPlayerActions.OnEquipmentIOpen(InputAction.CallbackContext context) {
        if (context.started && CurrentShowTarget != null) {
            if (CurrentShowTarget.Type == ColliderType.Equipment) {
                LockMove = true;
                PopupUIController.Instance.ShowPopupUIObj(1);
            }
        }
    }

    void GameInput.ILobbyPlayerActions.OnUpgradeOpen(InputAction.CallbackContext context) {
        if (context.started && CurrentShowTarget != null) {
            if (CurrentShowTarget.Type == ColliderType.Upgrade) {
                LockMove = true;
                PopupUIController.Instance.ShowPopupUIObj(2);
            }
        }
    }

    void OnTriggerStay(Collider collision) {
        if (collision.gameObject.GetComponent<LobbyUICollider>()) {
            LobbyUICollider temp = collision.gameObject.GetComponent<LobbyUICollider>();
            temp.ControlSelectedObj(ControlType.Enable);
            temp.SetSelectedBool(true);
            if (temp.Type == ColliderType.Equipment) {
                if (CurrentShowTarget != null) {
                    if (CurrentShowTarget.Type == ColliderType.Upgrade) {
                        CurrentShowTarget.ControlSelectedObj(ControlType.Disable);
                    }
                    CurrentShowTarget = temp;
                }
                else {
                    CurrentShowTarget = temp;
                }
            }

            if (temp.Type == ColliderType.Upgrade) {
                if (CurrentShowTarget != null) {
                    if (CurrentShowTarget.Type == ColliderType.Equipment) {
                        CurrentShowTarget.ControlSelectedObj(ControlType.Disable);
                    }
                    CurrentShowTarget = temp;
                }
                else {
                    CurrentShowTarget = temp;
                }
            }

            if (temp.Type == ColliderType.ReturnMainMenu) {
                LockLeft = true;
            }

            if (temp.Type == ColliderType.StartGame) {
                LockRight = true;
            }
            CurrentShowTarget = temp;
        }
    }

    void OnTriggerExit(Collider collision) {
        if (collision.gameObject.GetComponent<LobbyUICollider>()) {
            LobbyUICollider temp = collision.gameObject.GetComponent<LobbyUICollider>();
            temp.ControlSelectedObj(ControlType.Disable);
            temp.SetSelectedBool(false);
            if (temp.Type == ColliderType.ReturnMainMenu) {
                LockLeft = false;
            }

            if (temp.Type == ColliderType.StartGame) {
                LockRight = false;
            }
            CurrentShowTarget = null;
        }
    }
}
