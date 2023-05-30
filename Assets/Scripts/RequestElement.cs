using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestElement : MonoBehaviour
{
    
    [SerializeField]
    private Button viewButton;
    [SerializeField]
    private Text headingText;
    [SerializeField]
    private Text timeStampText;

    private UIManager uIManager;

    void start (){

        uIManager = GameObject.Find("AppUICanvas").GetComponent<UIManager>();

    }

    public void SetElementUI(RequestXml data){

        headingText.text = "Request by " + data.username;
        timeStampText.text = data.timeStamp;

    }

}
