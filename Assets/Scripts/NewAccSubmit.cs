using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FoxholeTools.Utils;

public class NewAccSubmit : MonoBehaviour
{

    public TMP_InputField usernameField;
    public TMP_InputField passkeyField;
    public TMP_InputField valPasskeyField;

/*     public void Update(){

        if(passkeyField.text == valPasskeyField.text && passkeyField.text != ""){
            this.gameObject.GetComponent<Button>().interactable=true;
        } else {
            this.gameObject.GetComponent<Button>().interactable=false;
        }

    } */

    public void CreateAccount(){
        
        string findUrl = $"{Helper.apiHost}:{Helper.apiPort}/users/find/{usernameField.text}";
        string addUrl = $"{Helper.apiHost}:{Helper.apiPort}/users";

        UserModel user = new UserModel{
            username=usernameField.text,
            //Passkey=passkeyField.text
        };

        WebRequests.Get(findUrl, (i)=>{},(data)=>{
        if(data != "true"){
            WebRequests.Put(addUrl, user, (i)=>{}, (i)=>{});
        }else{
            Debug.Log($"{user.username} already exists!");
            return;
        }
        });


    }
}
