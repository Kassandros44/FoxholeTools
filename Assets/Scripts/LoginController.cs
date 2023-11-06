using System;
using UnityEngine;
using TMPro;
using FoxholeTools.Utils;

public class LoginController : MonoBehaviour
{

    [SerializeField]
    public TMP_InputField usernameField;

    [SerializeField]
    private TMP_InputField passkeyField;

    [SerializeField]
    private GameObject loginScreen;

    public static event EventHandler<OnLoginEventArgs> OnLogin;
    public class OnLoginEventArgs : EventArgs{
        public UserModel user;
        public string username;
    }

    public void Login(){

        string url = $"{Helper.apiHost + Helper.apiPort}/users/{usernameField.text}";
        string keyUrl = $"{Helper.apiHost + Helper.apiPort}/checklogin/{passkeyField.text}";
        UserModel user = new UserModel();
        WebRequests.Get(keyUrl, (i)=>{}, (keyData)=>{
            Debug.Log(keyData);
            if(keyData == "true"){
                WebRequests.Get(url, (i)=>{}, (data)=>{
                    user = Helper.GetObjectFromData<UserModel>(data);
                    Debug.Log(usernameField.text);
                    if(user != null  && usernameField.text != ""){
                        string username = usernameField.text;
                        LocalUser.SetLocalUsername(username);
                        OnLogin?.Invoke(this, new OnLoginEventArgs{
                            user = user,
                            username = username
                        });
                        loginScreen.SetActive(false);
                    }else{
                        Debug.Log("Did not login");
                    }
                });
            }else{
                Debug.Log("Incorrect Password");
            }
        });
    }

    public void DiscordLogin()
    {
        if(LocalUser.GetUsername() != null)
            loginScreen.SetActive(false);
    }
}
