using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAccBackBtn : MonoBehaviour
{

    public GameObject LoginPanel;

    public void Back(){
        LoginPanel.SetActive(true);
        this.transform.parent.gameObject.SetActive(false);
    }

}
