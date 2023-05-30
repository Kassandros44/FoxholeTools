using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class M_Server : NetworkBehaviour {

    [SerializeField]
    private DataManager dataManager = new DataManager();
    
    [Server]
    private void Start() {
        
        if(File.Exists(Path.Combine(Application.persistentDataPath, "serverdata.xml"))){

            return;

        } else {

            dataManager.Save(Path.Combine(Application.persistentDataPath, "serverdata.xml"));

        }

        if(File.Exists(Path.Combine(Application.persistentDataPath, "serverdata.json"))){

            return;

        } else {

            dataManager.JSave(dataManager);

        }

    }

}

