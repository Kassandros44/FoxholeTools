using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

[XmlRoot("ItemsContainer")]
public class ItemContainer {

    [XmlArray("Items"), XmlArrayItem("Item")]
    public List<Item> Items = new List<Item>();

    public ItemContainer(){}

    public static ItemContainer Load(){

        string filename = "Items";
        XmlSerializer serializer = new XmlSerializer(typeof(ItemContainer));
        string data = Resources.Load<TextAsset>(filename).text;
        using (StringReader reader = new StringReader(data)){

            return (ItemContainer)(serializer.Deserialize(reader)) as ItemContainer;

        }

    }
 
}
