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

    public void UpdateStockpileView(List<Crate> crates, string name)
    {

        titleText.text = name;

        for (int i = 0; i < crates.Count; i++)
        {

            contentTransform.GetChild(i).Find("AmountTxt").GetComponent<Text>().text = crates[i].amount.ToString();
            contentTransform.GetChild(i).Find("QuotaTxt").GetComponent<Text>().text = crates[i].quota.ToString();

        }
    }

}
