using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPin : MonoBehaviour
{

    public Vector2 position;
    private UIManager uIManager;

    // Start is called before the first frame update
    void Start()
    {
        
        uIManager = GameObject.Find("AppUICanvas").GetComponent<UIManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        
        if(uIManager.isUIOverride) {

            

        } else {

            uIManager.CreateMapMarkerWindow(position);

        }

    }
}
