using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMarkerWindow : MonoBehaviour
{

    public MapMarkerInfo data;
    public Vector2 position;
    public Text titleText;
    [SerializeField]
    private GameObject requestPopup;

    // Start is called before the first frame update
    void Start()
    {
        if(data != null){
           titleText.text = data.Text; 
        } else {
            titleText.text = position.x + ", " + position.y;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateRequestPopup(){

        GameObject popup = Instantiate<GameObject>(requestPopup, this.transform.parent);
        if(data != null){
        popup.GetComponent<RequestPopup>().location = data.Text;
        } else {
            popup.GetComponent<RequestPopup>().location = position.x + ", " + position.y;
        }

    }

}
