using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelUIObj : MonoBehaviour
{

    [SerializeField] private int LabelCount;

    [SerializeField] private RectTransform ActiveLabelImgObj;

    [SerializeField] private Text LabelText;
    [SerializeField] private Text ActiveLabelText;

    // Start is called before the first frame update
    void Start() {
        LobbyUIController.Instance.RegisterOnLabelChanged(OnLabelChanged);
        ActiveLabelImgObj.gameObject.SetActive((LabelCount == 1) ? true : false);
        LabelText.text = "¨¤¦â" + LabelCount.ToString();
        ActiveLabelText.text = "¨¤¦â" + LabelCount.ToString();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SetLabelCount(int Count) {
        LabelCount = Count;
    }

    void OnLabelChanged(int Number) {
        ActiveLabelImgObj.gameObject.SetActive((Number == LabelCount) ? true : false);
    } 
}
