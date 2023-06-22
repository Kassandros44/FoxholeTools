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

    public UserModel userXml;

    public StockpileModel currentlyViewedStockpile;

    public DataManager dataManager;

    private void Start() {

        //if(isLocalPlayer){

            uIManager = GameObject.Find("AppUICanvas").GetComponent<UIManager>();
            mapUIManager = GameObject.Find("MapContainer").GetComponent<MapUIManager>();
            //networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            LocalUser.SetLocalUser(this.gameObject);
            UIManager.OnLogin += OnLogin;

            userXml = new UserModel();
            StartDataManager();
            userXml.username = uIManager.username;

            
            //RequestStockpileListUpdate();
            UpdateRequestList();

            StockpileListItem.OnStockViewChange += OnStockViewChange;

        //}

    }

    private void OnLogin(object sender, UIManager.OnLoginEventArgs e){
        CreateUser(e.user);
    }

    private void OnStockViewChange(object sender, StockpileListItem.OnViewEventArgs e){
        currentlyViewedStockpile = e.stockpile;
        //UpdateStockpileContent(e.stockpile);
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

    //[Command]
    void CreateUser(UserModel user){

        //dataManager = LoadData();

        ReturnConnection(user.username);
        UpdateMapPins(dataManager);

/*         foreach (var item in dataManager.userData.Users){

            if(item.username == username){
                return;
            }
            
        } */

        //dataManager.userData.Users.Add(userXml);
        //SaveData();
        Debug.Log($"{user.username} Connected");

    }

    //[Command]
    public void ClientDisconnected(string username){

        Debug.Log(username + " Disconnected");

    }

    //[Command]
/*     public void AddStockpileOnServer(StockpileModel stockpile){

        //dataManager.stockpileData.Stockpiles.Add(stockpile);
        CreateCrateListOnServer(stockpile);
        
        //SaveData();

        //UpdateStockpileList();
        DatabaseManager.AddStockpile(stockpile);

    } */

    //[Server]
/*     public void CreateCrateListOnServer(StockpileModel stockpile){

            var itemCollection = ItemContainer.Load();

            foreach (var cItem in itemCollection.Items) {

                Crate crate = new Crate();
                crate.name = cItem.name;
                crate.amount = 0;
                stockpile.crates.Add(crate);

            }

        SaveData();
        Debug.Log(stockpile.name + " was Saved");

    } */



    //[Command]
/*     public void RequestStockpileListUpdate(){

        if(File.Exists(Path.Combine(Application.persistentDataPath, "serverdata.xml"))){

            UpdateStockpileList();

        } else {

            return;

        }

    } */

/*     [Command]
    public void RequestStockpileData(string id){

        foreach (var item in DatabaseManager.GetAllStockpiles())
        {
            Debug.Log(item.name + " " + item.passcode);
            if(item.Id == id){
                UpdateStockpileContent(item);
            }

        }

    } */

    //[Command]
    /* 
    public void AddCratesToStockpile(StockpileModel stockpile, int index, int num, string username){

        foreach (var item in stockpile.crates) {

            Debug.Log(stockpile.crates.IndexOf(item) + "--" + index);

            if(stockpile.crates.IndexOf(item) == index){

                item.amount += num;
                stockpile.Logs.Add(new Log(username, DateTime.UtcNow.ToString(), "Added", item.name, num.ToString()));
                Debug.Log(username + ": " + DateTime.UtcNow.ToString() + " " + "added" + ": " + num + " " + item.name + " crates");

            }

        }

        foreach (var item in dataManager.stockpileData.Stockpiles)
        {
            if(item.passcode == stockpile.passcode){
                item.crates = stockpile.crates;
                item.Logs = stockpile.Logs;
            }
        }
        SaveData();
        Debug.Log(stockpile.Id);
        DatabaseManager.UpdateStockpile(stockpile);
        UpdateStockpileContent(stockpile);
        
    } */

    //[Command]
/*     public void RemoveCratesFromStockpile(StockpileModel stockpile, int index, int num, string username){

        foreach (var item in stockpile.crates) {
            
            if(stockpile.crates.IndexOf(item) == index){

                item.amount -= num;
                stockpile.Logs.Add(new Log(username, DateTime.UtcNow.ToString(), "Removed", item.name, num.ToString()));
                Debug.Log(username + ": " + DateTime.UtcNow.ToString() + " " + "removed" + ": " + num + " " + item.name + " crates");

            }

        }

        foreach (var item in dataManager.stockpileData.Stockpiles)
        {
            if(item.passcode == stockpile.passcode){
                item.crates = stockpile.crates;
                item.Logs = stockpile.Logs;
            }
        }

        SaveData();
        DatabaseManager.UpdateStockpile(stockpile);
        UpdateStockpileContent(stockpile);

    } */

    //[Command]
/*     public void SetCratesInStockpile(StockpileModel stockpile, int index, int num, string username){

        foreach (var item in stockpile.crates) {
            
            if(stockpile.crates.IndexOf(item) == index){

                item.amount = num;
                stockpile.Logs.Add(new Log(username, DateTime.UtcNow.ToString(), "Set", item.name, num.ToString()));
                Debug.Log(username + ": " + DateTime.UtcNow.ToString() + " " + "set" + ": " + num + " " + item.name + " crates");

            }

        }

        foreach (var item in dataManager.stockpileData.Stockpiles)
        {
            if(item.passcode == stockpile.passcode){
                item.crates = stockpile.crates;
                item.Logs = stockpile.Logs;
            }
        }

        SaveData();
        DatabaseManager.UpdateStockpile(stockpile);
        UpdateStockpileContent(stockpile);

    } */

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

    //[TargetRpc]
    void ReturnConnection(string username){

        Debug.Log(username);
        uIManager.usernameText.text = username;

    }

    //[TargetRpc]
 /*    void UpdateStockpileList(){

        for (int i = 0; i < uIManager.listContent.transform.childCount; i++) {
            
            Destroy(uIManager.listContent.transform.GetChild(i).gameObject);

        }

        foreach (var item in DatabaseManager.GetAllStockpiles()) {
            
            GameObject listItem = Instantiate(uIManager.stockpileListItem, uIManager.listContent.transform);
            listItem.GetComponent<StockpileListItem>().SetItemUI(item);
            //listItem.GetComponent<StockpileListItem>().OnStockViewChange += OnStockViewChange;

        }

    } */

    //[TargetRpc]
/*     public void UpdateStockpileContent(StockpileModel stockpile){
        
        currentlyViewedStockpile = stockpile;
        Debug.Log(currentlyViewedStockpile.name);

        for (int i = 0; i < stockpile.crates.Count; i++) {
            
            uIManager.SetCrateAmount(stockpile.crates[i].amount, i);

        }

    } */

    //[ClientRpc]
    public void UpdateMapPins(DataManager data){

        mapUIManager.ClearMapPins();

        foreach (var item in data.mapPinData.MapPins)
        {
            mapUIManager.CreateMapPin(new Vector3(item.mapPinCoords[0], item.mapPinCoords[1], item.mapPinCoords[2]), item.pinType);
        }

    }

}
