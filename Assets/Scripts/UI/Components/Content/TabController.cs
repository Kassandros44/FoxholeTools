using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> tabs;

    public void UpdateDisplayedContent(GameObject tab)
    {
        if (tab == null) return;

        if (tab.activeSelf == true) return;

        if(tab.activeSelf == false)
        {
            tabs.ForEach(gameObject => { gameObject.SetActive(false); });
            tab.SetActive(true);
        }
    }
}
