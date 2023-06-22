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

    public GameObject localUser;

    private void Start() {
        
        localUser = LocalUser.GetLocalUser();

    }

    public void CreateStockple(){

        StockpileModel.CreateNewStockpile(nameField.text, locationField.text, passcodeField.text);

        Destroy(this.gameObject);

    }
    
}
