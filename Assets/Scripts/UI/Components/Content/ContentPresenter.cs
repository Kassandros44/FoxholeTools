using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContentPresenter : MonoBehaviour
{

    [SerializeField]
    private GameObject contentPrefab;

    public UnityEvent<StockpileModel.simpleData> OnContentDrawn;

    public void DisplayContent(List<StockpileModel.simpleData> contentData)
    {

        for (int i = 0; i < transform.childCount; i++)
        {
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
