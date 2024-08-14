using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NSS;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart() {
        GameUIManager.Instance.SetResultActive(false);
        GameManager.Instance.StartNewGame();
        GameManager.Instance.Player.gameObject.SetActive(true);
        FieldManager.Instance.ShowAllBlock();
    }
}
