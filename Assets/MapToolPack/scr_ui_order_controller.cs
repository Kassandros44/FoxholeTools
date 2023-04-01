using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_ui_order_controller : MonoBehaviour
{
    public GameObject button_prefab;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform thisRect = GetComponent<RectTransform>();
        int offset = (64 + 4) * 5;
        for(int i = 0; i < 10;i++)
        {
            for (int o = 0; o < 10; o++)
            {
                GameObject go = Instantiate(button_prefab, transform,false);
                float posx = thisRect.rect.xMin;
                go.transform.position += new Vector3(4 + 68 * i,-4 -68 * o, 0);
                //go.GetComponent<RectTransform>().position = new Vector3(thisRect.rect.xMin + (i*64+4),thisRect.rect.yMin+ (o * 64 + 4), 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
