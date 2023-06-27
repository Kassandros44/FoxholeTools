using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewAccSubmit : MonoBehaviour
{

    public TMP_InputField usernameField;
    public TMP_InputField passkeyField;
    public TMP_InputField valPasskeyField;

    public void Update(){

        if(passkeyField.text == valPasskeyField.text && passkeyField.text != ""){
            this.gameObject.GetComponent<Button>().interactable=true;
        } else {
            this.gameObject.GetComponent<Button>().interactable=false;
        }

    }

    public void CreateAccount(){

        UserModel user = new UserModel{
            username=usernameField.text,
            Passkey=passkeyField.text
        };

        if(!DatabaseManager.UserExists(user.username)){
            DatabaseManager.AddUser(user);
        }else{
            Debug.Log($"{user.username} already exists!");
            return;
        }
    }
}
