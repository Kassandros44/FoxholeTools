using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockpileViewController : MonoBehaviour
{
    
    public void UpdateStockpileView(List<Crate> crates)
    {
        for (int i = 0; i < crates.Count; i++)
        {

            transform.GetChild(i).Find("AmountTxt").GetComponent<Text>().text = crates[i].amount.ToString();
            transform.GetChild(i).Find("QuotaTxt").GetComponent<Text>().text = crates[i].quota.ToString();

        }
    }

}
