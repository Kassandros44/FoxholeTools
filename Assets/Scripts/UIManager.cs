using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mirror;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameObject addStockpileWindow;
    
    [SerializeField]
    private GameObject itemInteractWindow;

    [SerializeField]
    private GameObject stockpileLogWindow;

    [SerializeField]
    private GameObject loginScreen;

    [SerializeField]
    private NetworkManager manager;

    [SerializeField]
    private InputField usernameField;

    [SerializeField]
    private Text connectionText;

    [SerializeField]
    public Text usernameText;

    [SerializeField]
    private GameObject itemButton;
    private Transform currentTab;
    private List<GameObject> markerWindows = new List<GameObject>();

    [SerializeField]
    public Transform tabContent;
    public GameObject stockpileListItem;
    public GameObject requestListItem;
    public GameObject listContent;
    public GameObject itemContent;
    public GameObject requestContent;
    public GameObject logWindow;
    public GameObject mapMarkerWindow;
    public GameObject orderlistScrollview;

    public string username;

    public bool isUIOverride;
    public ItemContainer itemContainer;

    public Transform GetCurrentTab(){
        return currentTab;
    }

    public List<GameObject> GetMarkerWindows(){
        UpdateMarkersList();
        return markerWindows;
    }

    private void Start() {
        
        MapUIManager mapUIManager = GameObject.Find("MapContainer").GetComponent<MapUIManager>();
        mapUIManager.OnRightMouseDown += SpawnMapMenu_OnRightMouseDown;

        GenerateItems();
        

    }

    private void SpawnMapMenu_OnRightMouseDown(object sender, EventArgs e){

        Debug.Log("Message from Canvas UI");
        

    }

    private void Update() {

        if(!NetworkClient.isConnected){

            connectionText.text = "Diconnected";

        } else {

            connectionText.text = "Connected";

        }

        isUIOverride = EventSystem.current.IsPointerOverGameObject();

    }

    public void ChangeTab(int tab){

        currentTab = tabContent.GetChild(tab);

        if(tabContent.gameObject.activeSelf == false){
            tabContent.gameObject.SetActive(true);
            GameObject bg = gameObject.transform.Find("MainBackground").gameObject;
            bg.SetActive(true);
        }

        for (int i = 0; i < tabContent.childCount; i++){

            if(tabContent.GetChild(i) == currentTab){

                currentTab.gameObject.SetActive(true);

            } else {

                tabContent.GetChild(i).gameObject.SetActive(false);

            }
            
        }

    }

    public void ShowMap(){

        tabContent.gameObject.SetActive(false);
        GameObject bg = gameObject.transform.Find("MainBackground").gameObject;
        bg.SetActive(false);

    }

    public void CreateAddStockpileWindow(){

        GameObject window = Instantiate(addStockpileWindow, this.transform);

    }

    public void CrateItemInteractWindow(int index){

        GameObject window = Instantiate(itemInteractWindow, this.transform);
        window.GetComponent<CrateInteractWindow>().index = index;

    }

    public void CreateStockpileLogWindow(){

        GameObject window = Instantiate(stockpileLogWindow, this.transform);
        logWindow = window;

    }

    public GameObject CreateMapMarkerWindow(MapMarkerInfo data){

        GameObject window;

        UpdateMarkersList();

        if(tabContent.gameObject.activeSelf == false && markerWindows.Count <= 2 && IconTypeBool(data.IconType)){

            window = Instantiate(mapMarkerWindow, this.transform);
            window.GetComponent<MapMarkerWindow>().data = data;
            markerWindows.Add(window);
            return window;

        } else {

            return null;

        }

    }

    public GameObject CreateMapMarkerWindow(Vector2 vec){

        GameObject window;

        UpdateMarkersList();

        if(tabContent.gameObject.activeSelf == false && markerWindows.Count <= 2){

            window = Instantiate(mapMarkerWindow, this.transform);
            window.GetComponent<MapMarkerWindow>().position = vec;
            markerWindows.Add(window);
            return window;

        } else {

            return null;

        }

    }

    private bool IconTypeBool(int iconType){

        switch (iconType)
        {
            case 45:
            return true;

            case 46:
            return true;

            case 47:
            return true;

            case 56:
            return true;

            case 57:
            return true;

            case 58:
            return true;

            case 33:
            return true;

            case 52:
            return true;

            default: 
            return false;
        }

    }

    private void UpdateMarkersList(){

        foreach (var item in markerWindows)
        {
            if(item == null){
                markerWindows.Remove(item);
            }
        }

    }

    public void Login(){

        loginScreen.SetActive(false);
        manager.StartClient();
        username = usernameField.text;
        LocalUser.SetLocalUsername(username);

    }

    public void Logout(){

        if(NetworkClient.isConnected){
            
            LocalUser.GetLocalUser().GetComponent<User>().ClientDisconnected(username);
            manager.StopClient();
            loginScreen.SetActive(true);

        } else {

            loginScreen.SetActive(true);

        }

    }

    public void QuitApp(){

        Application.Quit();

    }

    public void RefreshStockpileList(){

        LocalUser.GetLocalUser().GetComponent<User>().RequestStockpileListUpdate();

    }

    public void GenerateItems(){

        itemContainer = ItemContainer.Load();

        foreach (var item in itemContainer.Items)
        {
            GameObject newItem = Instantiate<GameObject>(itemButton, itemContent.transform);
            newItem.GetComponent<ItemButton>().index = itemContainer.Items.IndexOf(item);
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.image);
        }

    }

    public void SetCrateAmount(int amount, int i){

        itemContent.transform.GetChild(i).GetComponentInChildren<Text>().text = amount.ToString();

    }

}

