using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    UIManager uIManager;

    public int index;
    public string itemName;
    private Text amountText;
    private Text quotaText;
    private Color lerpedColor = Color.red;

    private void Start() {

        amountText = this.transform.Find("AmountTxt").GetComponent<Text>();
        quotaText = this.transform.Find("QuotaTxt").GetComponent<Text>();

        uIManager = GameObject.Find("AppUICanvas").GetComponent<UIManager>();
        TickTimerSystem.OnTick_5 += (object sender, TickTimerSystem.OnTickEventArgs e) => {CheckQuota();};

    }

    

    public void OnClicked(){

        uIManager.CrateItemInteractWindow(index);

    }

    public void OnPointerEnter(PointerEventData data){
        Tooltip.ShowTooltip_Static($"{itemName}\nQuota: {quotaText.text}\nAmount: {amountText.text}");
    }

    public void OnPointerExit(PointerEventData data){
        Tooltip.HideTooltip_Static();
    }

    private void CheckQuota(){

        float amount = Convert.ToSingle(amountText.text);
        float quota = Convert.ToSingle(quotaText.text);
        float toLerp;

        if(amount <= quota){
            toLerp = amount/quota;
        } else {
            toLerp = 1;
        }

        if(quota == 0){
            quotaText.color = Color.clear;
        } else {
            lerpedColor = Color.Lerp(Color.red, Color.green, toLerp);
            quotaText.color = lerpedColor;
        }
    }

}
