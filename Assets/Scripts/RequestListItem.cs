using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.Xml.Serialization;

public class RequestListItem : MonoBehaviour
{

    public struct Data
    {
        public Data(int i, int a){

            index = i;
            amount = a;

        }

        [XmlAttribute("index")]
        public int index {get; set;}
        [XmlAttribute("amount")]
        public int amount {get; set;}

    }

    public Data data;

    [SerializeField]
    private int index;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private Image image;

    public void SetIndex(int i){
        index = i;
    }
    public int GetIndex(){
        return index;
    }

    public void SetName(string n){
        itemName = n;
    }

    public string GetName(){
        return itemName;
    }

    public InputField GetInputField(){
        return inputField;
    }

    public Image GetImage(){
        return image;
    }

    public void Remove(){

        Destroy(this.gameObject);

    }
    
}

public class ItemListData
{
    public ItemListData(){



    }

    public ItemListData(int i, string n, int a, int p){
        index = i;
        itemName = n;
        amount = a;
        priority = p;
    }

    [XmlAttribute("index")]
    public int index {get; set;}

    [XmlAttribute("name")]
    public string itemName {get; set;}

    [XmlAttribute("amount")]
    public int amount {get; set;}

    [XmlAttribute("priority")]
    public int priority {get; set;}
}
