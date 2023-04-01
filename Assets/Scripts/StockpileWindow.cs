using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.IO;

public class StockpileWindow : NetworkBehaviour {

    [SerializeField]
    private InputField nameField;

    [SerializeField]
    private InputField locationField;

    [SerializeField]
    private InputField passcodeField;

    [SerializeField]
    private Toggle shareToggle;

    private StockpileXml stockpileXml;

    public GameObject localUser;

    private void Start() {
        
        localUser = NetworkClient.localPlayer.gameObject;
        stockpileXml = new StockpileXml();

    }

    public void CreateStockple(){

/*         if(shareToggle.isOn == true){ */

            stockpileXml.name = nameField.text;
            stockpileXml.location = locationField.text;
            stockpileXml.passcode = passcodeField.text;

            localUser.GetComponent<User>().AddStockpileOnServer(stockpileXml);

/*         } else {

            if(File.Exists(Path.Combine(Application.dataPath, "Stockpiles.xml"))){

                var stockpileCollection = StockpileContainer.Load(Path.Combine(Application.dataPath, "Stockpiles.xml"));

                stockpileXml.name = nameField.text;
                stockpileXml.location = locationField.text;
                stockpileXml.passcode = passcodeField.text;

                stockpileCollection.Stockpiles.Add(stockpileXml);
                stockpileCollection.Save(Path.Combine(Application.dataPath, "Stockpiles.xml"));

            } else {

                stockpileXml.name = nameField.text;
                stockpileXml.location = locationField.text;
                stockpileXml.passcode = passcodeField.text;
    
                stockpileContainer.Stockpiles.Add(stockpileXml);
                stockpileContainer.Save(Path.Combine(Application.dataPath, "Stockpiles.xml"));

            }

        } */

        Destroy(this.gameObject);

    }
    
}
