using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ContentPresenter : MonoBehaviour
{

    [SerializeField]
    private GameObject contentPrefab;

    public UnityEvent<StockpileModel.simpleData> OnContentDrawn;


    //May need another list to make this work.
    public void DisplayContent(List<StockpileModel.simpleData> contentData)
    {

        

        for (int i = 0; i < transform.childCount; i++)
        {
            if (contentData.Contains(transform.GetChild(i).GetComponent<StockpileListItem>().StockpileData))
            {
                continue;
            } else
            {

            }
            Destroy(transform.GetChild(i).gameObject);
        }

        contentData.ForEach(data => {

            GameObject contentObject = Instantiate(contentPrefab, transform);
            OnContentDrawn?.Invoke(data);
            
            Debug.Log("Displayed");
        });
    }

    public void DisplayContent(List<Crate> content) { }

}
