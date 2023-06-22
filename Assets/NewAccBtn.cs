using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAccBtn : MonoBehaviour
{

    public void OpenNewAccPanel(){
        GameObject loginpanel = this.transform.parent.gameObject;
        loginpanel.transform.parent.Find("CreateAccUIPanel").gameObject.SetActive(true);
        loginpanel.SetActive(false);
    }

}
