using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderType { ReturnMainMenu, StartGame, Equipment, Upgrade, Character };
public enum ControlType { Enable, Disable };
public class LobbyUICollider : MonoBehaviour
{
    [SerializeField] private ColliderType _Type;
    public ColliderType Type => _Type;

    [SerializeField] private RectTransform SelectedObj;

    private bool _IsShowSelectedObj;
    public bool IsShowSelectedObj => _IsShowSelectedObj;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ControlSelectedObj(ControlType Type) {
        switch (Type) {
            case ControlType.Enable:
                SelectedObj.gameObject.SetActive(true);
                break;

            case ControlType.Disable:
                SelectedObj.gameObject.SetActive(false);
                break;
        }
    }

    public void SetSelectedBool(bool Type) {
        _IsShowSelectedObj = Type;
    }
}
