using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapContextMenu : MonoBehaviour
{

    private MapUIManager mapUIManager;

    [SerializeField]
    private GameObject baseSubMenu;

    // Start is called before the first frame update
    void Start()
    {
        
        mapUIManager = GameObject.Find("MapContainer").GetComponent<MapUIManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleBaseSub(){

        baseSubMenu.SetActive(!baseSubMenu.activeSelf);

    }

    public void BaseMapPin(int pinType){

        Vector3 pinCoords = mapUIManager.GetPoint();
        LocalUser.GetLocalUser().GetComponent<User>().AddMapPin(pinCoords, pinType);

        Destroy(this.gameObject);
        
    }

}
