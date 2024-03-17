using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUIController : MonoBehaviour
{
    public static PopupUIController Instance;

    [SerializeField] private RectTransform PopupUIObjParent;
    [SerializeField] private RectTransform[] PopupUIObjArray;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void ShowPopupUIObj(int ObjCount) {
        PopupUIObjParent.gameObject.SetActive(true);
        for (int i = 0; i < PopupUIObjArray.Length; i++) {
            if (i != ObjCount) {
                PopupUIObjArray[i].gameObject.SetActive(false);
            }
            else {
                PopupUIObjArray[i].gameObject.SetActive(true);
            }
        }
    }
}
