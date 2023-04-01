using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_map_marker_controller : MonoBehaviour
{

    public TextMesh Text;
    public MapMarkerInfo data;

    private UIManager uIManager;
    SpriteRenderer SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        uIManager = GameObject.Find("AppUICanvas").GetComponent<UIManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void UpdateGraphics()
    {
        
        Color c = SpriteRenderer.color;
        if (data.IconType == 56 || data.IconType == 57 || data.IconType == 58)
        {
            if (MapData.ButtonStates[0])
            {
                c.a = 1f;
            }
            else
            {
                c.a = .25f;
            }
        }
        SpriteRenderer.color = c;
    }

    private void OnMouseDown() {

        if(uIManager.isUIOverride) {

            

        } else {

            uIManager.CreateMapMarkerWindow(data);

        }

    }

}
