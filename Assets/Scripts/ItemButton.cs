using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour {

    UIManager uIManager;

    public int index;

    private void Start() {
        
        uIManager = GameObject.Find("AppUICanvas").GetComponent<UIManager>();

    }

    public void OnClicked(){

        uIManager.CrateItemInteractWindow(index);

    }

}
