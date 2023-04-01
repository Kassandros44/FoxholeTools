using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mirror;

public class MapUIManager : MonoBehaviour {

    public event EventHandler OnRightMouseDown;
    public GameObject appUICanvas;
    public GameObject P_mapContextMenu;

    [SerializeField]
    private GameObject P_mapPin;
    [SerializeField]
    private UIManager uIManager;
    [SerializeField]
    private List<Sprite> sprites;
    private Camera cam;
    private GameObject contextMenu;
    private Vector3 point;

    public Vector3 GetPoint(){
        return point;
    }

    private void Start() {
        
        cam = Camera.main;
        InvokeRepeating("UpdateMap", 5.0f, 30.0f);

    }

    private void Update() {

        if(Input.GetButtonDown("Fire2") && contextMenu == null && uIManager.tabContent.gameObject.activeSelf == false){

            OnRightMouseDown?.Invoke(this, EventArgs.Empty);

            Vector3 mousePos = Input.mousePosition;{

                Debug.Log(mousePos.x);
                Debug.Log(mousePos.y);

                point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 5));
                Debug.Log(point);

                contextMenu = Instantiate(P_mapContextMenu, appUICanvas.transform);

            }

        } else if (Input.GetButtonDown("Fire2") && contextMenu != null){
            Destroy(contextMenu);
        }

    }

    private void UpdateMap(){

        StartCoroutine(MapData.UpdateMapData());

    }

    public void ClearMapPins(){

        Transform mapPinsContainer = this.transform.Find("MapPinsContainer");

        if(mapPinsContainer.childCount > 0){

            for (int i = 0; i < mapPinsContainer.childCount; i++)
            {
                Destroy(mapPinsContainer.GetChild(i).gameObject);
            }
            
        }

    }

    public void CreateMapPin(Vector3 pinCoords, int pinType){

        Transform mapPinsContainer = this.transform.Find("MapPinsContainer");

        GameObject newPin = Instantiate<GameObject>(P_mapPin, pinCoords, Quaternion.identity, mapPinsContainer);
        SpriteRenderer newPinSpriteRen = newPin.GetComponent<SpriteRenderer>();
        newPin.GetComponent<MapPin>().position = new Vector2(pinCoords.x, pinCoords.y);
        newPinSpriteRen.sprite = sprites[pinType];
        newPin.transform.localScale = new Vector3(0.5f, 0.5f, 1);

    }

}
