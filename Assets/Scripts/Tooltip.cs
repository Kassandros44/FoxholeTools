using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{   

    private static Tooltip instance;

    [SerializeField]
    private Camera uiCamera;
    private TMP_Text tooltipText;
    private RectTransform backgroundRectTransform;

    private void Awake() {

        instance = this;
        tooltipText = transform.Find("TooltipTxt").GetComponent<TMP_Text>();
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();

        ShowTooltip("Random Text: jewnlcefew;ncuhUnkcnebsdkhlfe;fh");

    }

    private void Update(){

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(),
        Input.mousePosition,
        uiCamera,
        out localPoint);
        transform.localPosition = localPoint;
        
    }

    private void ShowTooltip(string tooltipString){
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float paddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + paddingSize * 2f, tooltipText.preferredHeight + paddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void HideTooltip(){
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipString){
        instance.ShowTooltip(tooltipString);
    }

    public static void HideTooltip_Static(){
        instance.HideTooltip();
    }

}
