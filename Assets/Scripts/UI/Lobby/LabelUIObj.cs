using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelUIObj : MonoBehaviour
{

    [SerializeField] private int LabelCount;

    [SerializeField] private RectTransform ActiveLabelImgObj;

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.LabelChanged += OnLabelChanged;
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnLabelChanged(int Number) {
        ActiveLabelImgObj.gameObject.SetActive((Number == LabelCount) ? true : false);
    } 
}
