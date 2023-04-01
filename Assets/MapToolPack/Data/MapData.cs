using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class MapData
{
    public static List<scr_map_marker_controller> MapMarkers = new List<scr_map_marker_controller>();
    public static Dictionary<string, List<scr_map_marker_controller>> mapMarkers = new Dictionary<string, List<scr_map_marker_controller>>();
    public static Dictionary<string, List<scr_map_marker_controller>> mapTextMarkers = new Dictionary<string, List<scr_map_marker_controller>>();
    public static bool[] ButtonStates = new bool[5];

    public static IEnumerator UpdateMapData() {

        foreach (var map in mapMarkers)
        {
            WWWForm form = new WWWForm();
            string path = "https://war-service-live.foxholeservices.com/api/worldconquest/maps/" + map.Key + "/dynamic/public";
            UnityWebRequest www = UnityWebRequest.Get(path);
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else {

                JObject jobject = JObject.Parse(www.downloadHandler.text);
                JArray mapItems = (JArray)jobject["mapItems"];

                foreach (var a in mapItems)
                {
                    
                    JObject item = JObject.Parse(a.ToString());
                    MapMarkerInfo info = new MapMarkerInfo(item);

                    foreach (var mapMarkerCon in map.Value)
                    {
                        if(mapMarkerCon.data.X == info.X && mapMarkerCon.data.Y == info.Y){

                            mapMarkerCon.data.Team = info.Team;
                            mapMarkerCon.data.IconType = info.IconType;
                            Vector2 locVector = new Vector2(mapMarkerCon.data.X, mapMarkerCon.data.Y);

                            mapMarkerCon.GetComponent<SpriteRenderer>().sprite = MapIcons.sprites[info.IconType];

                            if (info.Team == 1)
                            {
                                mapMarkerCon.GetComponent<SpriteRenderer>().color = new Color(0.1411765f, 0.3372549f, 0.509804f, 1);
                            }
                            if (info.Team == 2)
                            {
                                mapMarkerCon.GetComponent<SpriteRenderer>().color = new Color(0.3176471f, 0.4235294f, 0.2941177f, 1);
                            }
                            if(info.Team == 0){
                                mapMarkerCon.GetComponent<SpriteRenderer>().color = Color.white;
                            }

                        }

                    }

                }

            }
            
        }

    }

}
