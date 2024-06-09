using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NSS;

public class LobbyCharacter : MonoBehaviour
{   
    private RectTransform _transform;

    public Animator CharacterAnimator;

    [SerializeField] private float Speed;

    private bool _isMoving;
    public bool IsMoving => _isMoving;
    private bool _lockMove;
    public bool LockMove => _lockMove;
    private bool _IsInUICollider;
    public bool IsinUICollider => _IsInUICollider;
    private bool LockLeft;
    private bool LockRight;

    void Awake() {
        CharacterAnimator = GetComponent<Animator>();
        _transform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void SetLockMove(bool Type) {
        _lockMove = Type;
    }

    public void SetIsMoving(bool Type) {
        _isMoving = Type;
    }

    public void Move(Vector2 Vector) {
        if (!_lockMove) {
            if (Vector.x < 0) {
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

    public void SetLockLeftFalse() {
        LockLeft = false;
    }

    void OnTriggerStay(Collider collision) {
        if (collision.gameObject.GetComponent<LobbyUICollider>()) {
            _IsInUICollider = true;
            LobbyUICollider temp = collision.gameObject.GetComponent<LobbyUICollider>();
            if (!temp.IsShowSelectedObj) {
                LobbyUIController.Instance.SendColliderTriggeredEvent(temp.ColliderType, ControlType.Enable);
            }

            if (temp.ColliderType == ColliderType.ReturnMainMenu) {
                LobbyUIController.Instance.IsReturnMainMenu = true;
                LockLeft = true;
            }

            if (temp.ColliderType == ColliderType.StartGame) {
                LockRight = true;
            }
        }
    }

    void OnTriggerExit(Collider collision) {
        if (collision.gameObject.GetComponent<LobbyUICollider>()) {
            _IsInUICollider = false;
            LobbyUICollider temp = collision.gameObject.GetComponent<LobbyUICollider>();
            if (temp.IsShowSelectedObj) {
                LobbyUIController.Instance.SendColliderTriggeredEvent(temp.ColliderType, ControlType.Disable);
            }

            if (temp.ColliderType == ColliderType.ReturnMainMenu) {
                LobbyUIController.Instance.IsReturnMainMenu = false;
                LockLeft = false;
            }

            if (temp.ColliderType == ColliderType.StartGame) {
                LockRight = false;
            }
        }
    }
}
