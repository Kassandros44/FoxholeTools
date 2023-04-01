using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequestItemBtn : MonoBehaviour
{

    private RequestPopup requestPopup;

    public int index;

    public void SetRequestPopup(RequestPopup obj){
        requestPopup = obj;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClicked(){

        Sprite sprite = this.transform.Find("Image").GetComponent<Image>().sprite;
        requestPopup.DoRequest(index, sprite);

    }

}
