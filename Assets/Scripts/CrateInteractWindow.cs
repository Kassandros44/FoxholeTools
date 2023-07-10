using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Mirror;
using System;

public class CrateInteractWindow : NetworkBehaviour {

    [SerializeField]
    private Dropdown dropdown;
    [SerializeField]
    private InputField inputField;

    private GameObject localUser;

    private StockpileModel m_stockpile;
    private Action<StockpileModel, int, int, string> InputType;

    public int index;

    private void Start() {
        
        dropdown.onValueChanged.AddListener(delegate {DropdownValueChanged(dropdown);});
        m_stockpile = LocalUser.GetUserComponent().currentlyViewedStockpile;
        InputType = m_stockpile.AddCratesToStockpile;

    }

    public void DropdownValueChanged(Dropdown change){
        Debug.Log($"Changed Value {change.value}");

        switch (change.value){
            case 0:
                InputType = m_stockpile.AddCratesToStockpile;
                break;
            case 1:
                InputType = m_stockpile.RemoveCratesFromStockpile;
                break;
            case 2:
                InputType = m_stockpile.SetCratesInStockpile;
                break;
            case 3:
                InputType = m_stockpile.SetQuotaInStockpile;
                break;
        }

    }

    public static event EventHandler<OnSubmitEventArgs> OnSubmit;
    public class OnSubmitEventArgs : EventArgs{
        public StockpileModel stockpile;
    }

    public void SubmitButton(){

        InputType(m_stockpile, index, int.Parse(inputField.text), LocalUser.GetUsername());
        OnSubmit?.Invoke(this, new OnSubmitEventArgs{stockpile = m_stockpile});
        CloseWindow();

    }

    private void CloseWindow(){

        Destroy(this.gameObject);

    }

}
