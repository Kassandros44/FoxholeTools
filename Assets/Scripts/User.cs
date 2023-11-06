using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

public class User : MonoBehaviour
{

    [SerializeField]
    public UIManager uIManager;
    public MapUIManager mapUIManager;

    [SerializeField]
    private NetworkManager networkManager;

    public StockpileModel currentlyViewedStockpile;

    public DataManager dataManager;

    private void Start() {

        //if(isLocalPlayer){

        uIManager = GameObject.Find("AppUICanvas").GetComponent<UIManager>();
        mapUIManager = GameObject.Find("MapContainer").GetComponent<MapUIManager>();
        LocalUser.SetLocalUser(this.gameObject);
        //LoginController.OnLogin += OnLogin;
        DiscordAuthHandler.OnAuthComplete += OnDiscordAuth;

        StartDataManager();
        UpdateRequestList();
        StockpileListItem.OnStockViewChange += OnStockViewChange;

        //}

    }

    //private void OnLogin(object sender, LoginController.OnLoginEventArgs e){
    //    CreateUser(e.user);
    //}

    private void OnStockViewChange(object sender, StockpileListItem.OnViewEventArgs e){
        currentlyViewedStockpile = e.stockpile;
    }

    private void OnDiscordAuth(object sender, DiscordAuthHandler.AuthEventArgs e)
    {
        LocalUser.SetLocalUsername(e.userModel.username);
    }

    //[Server]
    private void SaveData(){

        dataManager.Save(Path.Combine(Application.persistentDataPath, "serverdata.xml"));
        dataManager.JSave(dataManager);

    }

    //[Server]
    private DataManager LoadData(){

        return(DataManager.Load(Path.Combine(Application.persistentDataPath, "serverdata.xml")));

    }

    //[Command]
    private void StartDataManager(){

        dataManager = new DataManager();

    }

    //request code
    //[Command]
    public void AddRequestData(Vector3 point){
        
        Debug.Log("pin " + point.x);

        dataManager = LoadData();

        RequestXml newReq = new RequestXml();
        newReq.mapPinCoords = new float[3] {point.x, point.y, point.z};

        dataManager.requestData.Requests.Add(newReq);

        SaveData();

    }
    
    RequestXml request;

    //[Command]
    public void GenerateNewRequest(){
        request = new RequestXml();
    }

    //[Command]
    public void AddNewRequestItem(int index, string name, int amount, int priority){
        request.itemList.Add(new ItemListData(index, name, amount, priority));
    }

    //[Command]
    public void CreateRequest(string location, string username){

        request.location = location;
        request.username = username;
        request.timeStamp = DateTime.UtcNow.ToString();
        dataManager.requestData.Requests.Add(request);

        SaveData();

    }

    //[Command]
    public void UpdateRequestList(){

        LoadRequestData(LoadData());

    }

    //[TargetRpc]
    public void LoadRequestData(DataManager data){

        for(int i = 0; i < uIManager.requestContent.transform.childCount; i++){

            Destroy(uIManager.requestContent.transform.GetChild(i).gameObject);

        }

        foreach (var item in data.requestData.Requests) {
            
            GameObject listItem = Instantiate(uIManager.requestListItem, uIManager.requestContent.transform);
            listItem.GetComponent<RequestElement>().SetElementUI(item);

        }

    }

    //map pin code
    //[Command]
    public void AddMapPin(Vector3 point, int pinType){

        MapPinXml newMapPin = new MapPinXml();
        newMapPin.mapPinCoords = new float[3] {point.x, point.y, point.z};
        newMapPin.pinType = pinType;

        dataManager.mapPinData.MapPins.Add(newMapPin);

        SaveData();
        UpdateMapPins(dataManager);

    }

    //[ClientRpc]
    public void UpdateMapPins(DataManager data){

        mapUIManager.ClearMapPins();

        foreach (var item in data.mapPinData.MapPins)
        {
            mapUIManager.CreateMapPin(new Vector3(item.mapPinCoords[0], item.mapPinCoords[1], item.mapPinCoords[2]), item.pinType);
        }

    }

}
