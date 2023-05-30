using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System;

[Serializable]
public class MapPinXml
{

    [XmlAttribute("MapPinCoords")]
    public float[] mapPinCoords;

    [XmlAttribute("PinType")]
    public int pinType;

}
