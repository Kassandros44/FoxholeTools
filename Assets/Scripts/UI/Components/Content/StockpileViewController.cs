using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StockpileViewController : MonoBehaviour
{

    [SerializeField]
    private TMP_Text titleText;
    [SerializeField]
    private Transform contentTransform;

    public void UpdateStockpileView(StockpileData.ViewArgs args)
    {

        titleText.text = args.args2;

        for (int i = 0; i < args.args1.Count; i++)
        {

            contentTransform.GetChild(i).Find("AmountTxt").GetComponent<Text>().text = args.args1[i].amount.ToString();
            contentTransform.GetChild(i).Find("QuotaTxt").GetComponent<Text>().text = args.args1[i].quota.ToString();

        }
    }

}
