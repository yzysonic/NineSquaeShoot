using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelController : MonoBehaviour
{

    [SerializeField] private int CurrentGridID;

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.RegisterOnChangeButtonClicked(OnChangeButtonClicked);
        LobbyUIController.Instance.RegisterOnConfirmButtonClicked(OnConfirmButtonClicked);
        LobbyUIController.Instance.RegisterOnGridMoveButtonClicked(OnGridMoveButtonClicked);
    }

    // Update is called once per frame
    void Update() {
        
    }


    void OnChangeButtonClicked() {

    }

    void OnConfirmButtonClicked() {

    }

    void OnGridMoveButtonClicked(Vector2 Vector) {
        if (Vector.y != 0) {
            CurrentGridID = (Vector.y < 0) ? CurrentGridID + 3 : CurrentGridID - 3;
            if (CurrentGridID > 6 || CurrentGridID < 1) {
                CurrentGridID = (CurrentGridID > 6) ? CurrentGridID - 3 : CurrentGridID + 3;
            }
        }

        if (Vector.x != 0) {
            CurrentGridID = (Vector.x < 0) ? CurrentGridID - 1 : CurrentGridID + 1;
            if (CurrentGridID < 1 || CurrentGridID > 6) {
                CurrentGridID = (CurrentGridID < 1) ? CurrentGridID = 1 : 6;
            }
        }
        LobbyUIController.Instance.SendSelectGridChangedEvent(CurrentGridID);
    }
}
