using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsernameTextPresenter : MonoBehaviour
{

    [SerializeField]
    User user;
    [SerializeField]
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUserText()
    {

        if (user.UserModel.guildName == "")
            text.text = user.UserModel.globalName;
        else
            text.text = user.UserModel.guildName;

        Debug.Log(text.text);
    }
}
