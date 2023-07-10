using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Mirror;
using TMPro;
using Newtonsoft.Json.Linq;
using FoxholeTools.Utils;

public class UIManager : MonoBehaviour {

    #region UI Manager Properties
    [SerializeField]
    private GameObject stockpileTab;
    [SerializeField]
    private GameObject requestsTab;

    [SerializeField]
    private GameObject addStockpileWindow;
    
    [SerializeField]
    private GameObject itemInteractWindow;

    [SerializeField]
    private GameObject stockpileLogWindow;
    [SerializeField]
    private GameObject stockDltWindow;

    [SerializeField]
    private GameObject loginScreen;

    [SerializeField]
    private NetworkManager manager;

    [SerializeField]
    public TMP_InputField usernameField;

    [SerializeField]
    private TMP_InputField passkeyField;

    [SerializeField]
    private Text connectionText;

    [SerializeField]
    public TMP_Text usernameText;

    [SerializeField]
    private GameObject itemButton;
    private Transform currentTab;
    private List<GameObject> markerWindows = new List<GameObject>();
    private StockpileModel currentlyViewedStockpile;

    [SerializeField]
    public Transform tabContent;
    public GameObject stockpileListItem;
    public GameObject requestListItem;
    public GameObject listContent;
    public GameObject itemContent;
    public GameObject requestContent;
    public GameObject logWindow; //Look into why this even exists
    public GameObject mapMarkerWindow;
    public GameObject orderlistScrollview;

    public string username;

    public bool isUIOverride;
    public ItemContainer itemContainer;
    #endregion

    #region Tool UI Functions
    public Transform GetCurrentTab(){
        return currentTab;
    }

    public List<GameObject> GetMarkerWindows(){
        UpdateMarkersList();
        return markerWindows;
    }

    private void Start() {
        
        Helper.LoadDotEnv();
        MapUIManager mapUIManager = GameObject.Find("MapContainer").GetComponent<MapUIManager>();
        mapUIManager.OnRightMouseDown += SpawnMapMenu_OnRightMouseDown;

        GenerateItems();
        
        stockpileTab.SetActive(true);
        requestsTab.SetActive(false);
        Tooltip.HideTooltip_Static();

        CrateInteractWindow.OnSubmit += (object sender, CrateInteractWindow.OnSubmitEventArgs e) => {UpdateStockpileContent(e.stockpile);};
        TickTimerSystem.OnTick_5 += (object sender, TickTimerSystem.OnTickEventArgs e) => {UpdateStockpileList();};
        StockpileListItem.OnStockViewChange += (object sender, StockpileListItem.OnViewEventArgs e) => {
            UpdateStockpileContent(e.stockpile);
            currentlyViewedStockpile = e.stockpile;
        };

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

    void UpdateStockpileList(){
        
        string url = $"{Helper.apiHost}:{Helper.apiPort}/stockpiles";
        WebRequests.Get(url, (list) => {}, (data) => {

            Transform listObjectTransform = listContent.transform;
            List<StockpileModel> children = new List<StockpileModel>();
            List<StockpileModel> list = Helper.GetListFromData<StockpileModel>(data);

            foreach(Transform t in listObjectTransform){
                children.Add(t.GetComponent<StockpileListItem>().stockpileXml);
            }

            for (int i = 0; i < listObjectTransform.childCount; i++) {
                StockpileModel stockpile =
                    listObjectTransform.GetChild(i)
                    .gameObject.GetComponent<StockpileListItem>().stockpileXml;
                if(!list.Contains(stockpile))
                    Destroy(listObjectTransform.GetChild(i).gameObject);
            }
            foreach (var item in list) {

                if(!children.Contains(item)){
                    GameObject listItem = Instantiate(stockpileListItem, listContent.transform);
                    listItem.GetComponent<StockpileListItem>().SetItemUI(item);
                    //listItem.GetComponent<StockpileListItem>().OnStockViewChange += OnStockViewChange;
                }
            }
        }); 
    }

    public void UpdateStockpileContent(StockpileModel stockpile){
        
        Debug.Log(stockpile.name);

        for (int i = 0; i < stockpile.crates.Count; i++) {
            
            itemContent.transform.GetChild(i).Find("AmountTxt").GetComponent<Text>().text = stockpile.crates[i].amount.ToString();
            itemContent.transform.GetChild(i).Find("QuotaTxt").GetComponent<Text>().text = stockpile.crates[i].quota.ToString();

        }

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

    public void OnDelStkBtnClicked(){
        GameObject window = Instantiate(stockDltWindow, this.transform);
        window.GetComponent<StockDltWindow>().stockpile = currentlyViewedStockpile;
    }

    public void CreateAddStockpileWindow(){

        GameObject window = Instantiate(addStockpileWindow, this.transform);

    }

    public void CrateItemInteractWindow(int index){

        GameObject window = Instantiate(itemInteractWindow, this.transform);
        window.GetComponent<CrateInteractWindow>().index = index;
        //window.GetComponent<CrateInteractWindow>().OnSubmit += OnCrateInteractWindowSubmitted;

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

    public static event EventHandler<OnLoginEventArgs> OnLogin;
    public class OnLoginEventArgs : EventArgs{
        public UserModel user;
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

/*     public void RefreshStockpileList(){

        LocalUser.GetLocalUser().GetComponent<User>().RequestStockpileListUpdate();

    } */

    public void GenerateItems(){

        itemContainer = ItemContainer.Load();

        foreach (var item in itemContainer.Items)
        {
            GameObject newItem = Instantiate<GameObject>(itemButton, itemContent.transform);
            newItem.GetComponent<ItemButton>().index = itemContainer.Items.IndexOf(item);
            newItem.GetComponent<ItemButton>().itemName = item.name;
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.image);
        }

    }
    #endregion
}


#region Editor Code
#if UNITY_EDITOR
[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor {

    #region Serialized Properties
    SerializedProperty m_NetworkManagerProp;
    SerializedProperty m_TabContentProp;
    SerializedProperty m_LocalUsernameProp;
    SerializedProperty m_IsUIOverrideProp;
    SerializedProperty m_StockpileTabProp;
    SerializedProperty m_RequestsTabProp;
    SerializedProperty m_AddStockpileWindowProp;
    SerializedProperty m_ItemInteractWindowProp;
    SerializedProperty m_StockpileLogWindowProp;
    SerializedProperty m_MapMarkerWindowProp;
    SerializedProperty m_StockDltWindowProp;
    SerializedProperty m_LoginPanelProp;
    SerializedProperty m_UsernameFieldProp;
    SerializedProperty m_PasskeyFieldProp;
    SerializedProperty m_ItemButtonProp;
    SerializedProperty m_ConnectionTextProp;
    SerializedProperty m_UsernameTextProp;
    SerializedProperty m_StockpileListItemProp;
    SerializedProperty m_RequestListItemProp;
    SerializedProperty m_StockpileScrollContentProp;
    SerializedProperty m_ItemScrollContentProp;
    SerializedProperty m_RequestScrollContentProp;
    #endregion

    bool showTabs = false;
    bool showWindows = false;
    bool showPanels = false;
    bool showInputFields = false;
    bool showButtons = false;
    bool showText = false;
    bool showListItems = false;
    bool showScrollContent = false;

    void OnEnable() {

        m_NetworkManagerProp = serializedObject.FindProperty("manager");
        m_TabContentProp = serializedObject.FindProperty("tabContent");
        m_LocalUsernameProp = serializedObject.FindProperty("username");
        m_IsUIOverrideProp = serializedObject.FindProperty("isUIOverride");

        //Tabs
        m_StockpileTabProp = serializedObject.FindProperty("stockpileTab");
        m_RequestsTabProp = serializedObject.FindProperty("requestsTab");

        //Windows
        m_AddStockpileWindowProp = serializedObject.FindProperty("addStockpileWindow");
        m_ItemInteractWindowProp = serializedObject.FindProperty("itemInteractWindow");
        m_StockpileLogWindowProp = serializedObject.FindProperty("stockpileLogWindow");
        m_MapMarkerWindowProp = serializedObject.FindProperty("mapMarkerWindow");
        m_StockDltWindowProp = serializedObject.FindProperty("stockDltWindow");

        //Panel & Screens
        m_LoginPanelProp = serializedObject.FindProperty("loginScreen");

        //InputFields
        m_UsernameFieldProp = serializedObject.FindProperty("usernameField");
        m_PasskeyFieldProp = serializedObject.FindProperty("passkeyField");

        //Buttons
        m_ItemButtonProp = serializedObject.FindProperty("itemButton");

        //Text
        m_ConnectionTextProp = serializedObject.FindProperty("connectionText");
        m_UsernameTextProp = serializedObject.FindProperty("usernameText");

        //List Items
        m_StockpileListItemProp = serializedObject.FindProperty("stockpileListItem");
        m_RequestListItemProp = serializedObject.FindProperty("requestListItem");

        //Scroll Rect Content
        m_StockpileScrollContentProp = serializedObject.FindProperty("listContent");
        m_ItemScrollContentProp = serializedObject.FindProperty("itemContent");
        m_RequestScrollContentProp = serializedObject.FindProperty("requestContent");

    }

    public override void OnInspectorGUI(){
        
        EditorGUILayout.PropertyField(m_NetworkManagerProp, new GUIContent("Network Manager"));
        EditorGUILayout.PropertyField(m_TabContentProp, new GUIContent("Tab Content"));

        showTabs = EditorGUILayout.BeginFoldoutHeaderGroup(showTabs, "Tab Panels");
        if(showTabs){
            EditorGUILayout.PropertyField(m_StockpileTabProp, new GUIContent("Stockpile Tab"));
            EditorGUILayout.PropertyField(m_RequestsTabProp, new GUIContent("Request Tab"));
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showWindows = EditorGUILayout.BeginFoldoutHeaderGroup(showWindows, "UI Windows");
        if(showWindows){
            EditorGUILayout.PropertyField(m_AddStockpileWindowProp, new GUIContent("Add Stockpile Window"));
            EditorGUILayout.PropertyField(m_ItemInteractWindowProp, new GUIContent("Item Interact Window"));
            EditorGUILayout.PropertyField(m_StockpileLogWindowProp, new GUIContent("Stockpile Log Window"));
            EditorGUILayout.PropertyField(m_MapMarkerWindowProp, new GUIContent("Map Marker Window"));
            EditorGUILayout.PropertyField(m_StockDltWindowProp, new GUIContent("Delete Stockpile Window"));
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showPanels = EditorGUILayout.BeginFoldoutHeaderGroup(showPanels, "Panels & Screens");
        if(showPanels){
            EditorGUILayout.PropertyField(m_LoginPanelProp, new GUIContent("Login Screen"));
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showInputFields = EditorGUILayout.BeginFoldoutHeaderGroup(showInputFields, "Input Fields");
        if(showInputFields){
            EditorGUILayout.PropertyField(m_UsernameFieldProp, new GUIContent("Username Input Field"));
            EditorGUILayout.PropertyField(m_PasskeyFieldProp, new GUIContent("Passkey Input Field"));
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showButtons = EditorGUILayout.BeginFoldoutHeaderGroup(showButtons, "Buttons");
        if(showButtons){
            EditorGUILayout.PropertyField(m_ItemButtonProp, new GUIContent("Item Button"));
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showText = EditorGUILayout.BeginFoldoutHeaderGroup(showText, "Text");
        if(showText){
            EditorGUILayout.PropertyField(m_UsernameTextProp, new GUIContent("Client Username Text"));
            EditorGUILayout.PropertyField(m_ConnectionTextProp, new GUIContent("Connection Status Text"));
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showListItems = EditorGUILayout.BeginFoldoutHeaderGroup(showListItems, "List Items/Elements");
        if(showListItems){
            EditorGUILayout.PropertyField(m_StockpileListItemProp, new GUIContent("Stockpile List UI Element"));
            EditorGUILayout.PropertyField(m_RequestListItemProp, new GUIContent("Request List UI Element"));
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        showScrollContent = EditorGUILayout.BeginFoldoutHeaderGroup(showScrollContent, "Scroll Rect Content");
        if(showScrollContent){
            EditorGUILayout.PropertyField(m_StockpileScrollContentProp, new GUIContent("Stockpile List Content"));
            EditorGUILayout.PropertyField(m_ItemScrollContentProp, new GUIContent("Item List Content"));
            EditorGUILayout.PropertyField(m_RequestScrollContentProp, new GUIContent("Request List Content"));
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        EditorGUILayout.PropertyField(m_LocalUsernameProp, new GUIContent("Local Client Username"));
        EditorGUILayout.PropertyField(m_IsUIOverrideProp, new GUIContent("UI Override Boolean"));
        serializedObject.ApplyModifiedProperties();

    }

}
#endif
#endregion
