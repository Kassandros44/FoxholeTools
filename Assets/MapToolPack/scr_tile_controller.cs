using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class scr_tile_controller : MonoBehaviour
{
    public string MapName;
    public Vector2 Location;
    public GameObject MapMarker, mapMarkerText;

    // Start is called before the first frame update
    void Start()
    {
        MapIcons.LoadIcons();
        Vector3 vector = new Vector3();
        vector.y = 3.475f * Location.y;
        vector.x = 6.03f * Location.x;
        vector.z = transform.position.z;
        transform.position = vector;
        if (MapName != "")
        {

            // StartCoroutine(LoadMapData(MapName));
            StartCoroutine(LoadMapText(MapName));

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClearMaps(){

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }

    }

    IEnumerator LoadMapData(string map)
    {

        List<scr_map_marker_controller> tempMarkers = new List<scr_map_marker_controller>();

        WWWForm form = new WWWForm();
        string path = "https://war-service-live.foxholeservices.com/api/worldconquest/maps/" + map + "/dynamic/public";
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            JObject jobject = JObject.Parse(www.downloadHandler.text);
            JArray mapItems = (JArray)jobject["mapItems"];
            GameObject mmholder = Instantiate(new GameObject(),this.transform,false);
            mmholder.name = map + "_Items";

            foreach (var a in mapItems)
            {

                JObject item = JObject.Parse(a.ToString());
                MapMarkerInfo info = new MapMarkerInfo(item);

                try
                {
                    if (MapIcons.sprites.ContainsKey(info.IconType))
                    {
                        if(info.IconType == 56 || info.IconType == 57 || info.IconType == 58 || info.IconType == 45 || info.IconType == 46 || info.IconType == 47){
                            tempMarkers.Add(SpawnBases(info, map));
                        } else if(info.IconType == 33 || info.IconType == 52){
                            
                        } else {
                            float mod = 8f;
                            float mapX = info.X * mod - 4;
                            float mapY = (info.Y * mod * -1 + 4f) * .875f;
                            Vector3 loc = new Vector3(mapX, mapY, 0f);

                            //loc.y = loc.y * -1f;
                            GameObject temp = Instantiate(MapMarker, mmholder.transform, true);
                            temp.transform.position += this.transform.position + loc;
                            temp.GetComponent<SpriteRenderer>().enabled = true;
                            temp.GetComponent<SpriteRenderer>().sprite = MapIcons.sprites[info.IconType];
                            temp.GetComponent<scr_map_marker_controller>().Text.text = "";
                            temp.GetComponent<scr_map_marker_controller>().data = info;
                            temp.name = map + "_Items_" + info.IconType;
                            tempMarkers.Add(temp.GetComponent<scr_map_marker_controller>());
                        }
                                 
                    }

                }
                catch(Exception ex)
                {
                    Debug.Log(ex.Message);
                }

            }

            foreach (var a in mapItems)
            {
                JObject item = JObject.Parse(a.ToString());
                MapMarkerInfo info = new MapMarkerInfo(item);

                if(MapIcons.sprites.ContainsKey(info.IconType)){
                        
                    if(info.IconType == 33 || info.IconType == 52){
                        tempMarkers.Add(SpawnFacility(info, map));
                    } else {
                        
                    }

                }

            }

            MapData.mapMarkers.Add(map, tempMarkers);

        }

    }

    IEnumerator LoadMapText(string map)
    {

        List<scr_map_marker_controller> tempMarkers = new List<scr_map_marker_controller>();

        WWWForm form = new WWWForm();
        string path = "https://war-service-live.foxholeservices.com/api/worldconquest/maps/"+map+ "/static";
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();
        if(www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            JObject jobject = JObject.Parse(www.downloadHandler.text);
            JArray mapTextItems = (JArray)jobject["mapTextItems"];
            GameObject mmholder = Instantiate(new GameObject(), Vector3.zero, Quaternion.Euler(0, 0, 0), this.transform);
            mmholder.name = map + "_Text";

            foreach (var a in mapTextItems)
            {
                
                JObject item = JObject.Parse(a.ToString());
                MapMarkerInfo info = new MapMarkerInfo(item);

                if ((string)item["mapMarkerType"] == "Major")
                {
                    float mod = 8f;
                    float mapX = info.X * mod - 4;
                    float mapY = (info.Y * mod * -1 + 4f) * .875f;
                    Vector3 loc = new Vector3(mapX,mapY, 0f);

                    GameObject temp = Instantiate(mapMarkerText, mmholder.transform, true);
                    temp.transform.position += this.transform.position + loc;
                    temp.name = map + "_Text_"+ info.Text;
                    temp.GetComponent<scr_map_marker_controller>().Text.text = info.Text;
                    temp.GetComponent<scr_map_marker_controller>().data = info;
                    temp.GetComponent<SpriteRenderer>().enabled = false;
                    tempMarkers.Add(temp.GetComponent<scr_map_marker_controller>());
                }
            }
            MapData.mapTextMarkers.Add(map, tempMarkers);
            StartCoroutine(LoadMapData(MapName));
            Debug.Log(www.downloadHandler.text);
        }

    }

    scr_map_marker_controller SpawnBases(MapMarkerInfo info, string map){

        float mod = 8f;
        float mapX = info.X * mod - 4;
        float mapY = (info.Y * mod * -1 + 4f) * .875f;
        Vector3 loc = new Vector3(mapX, mapY, 0f);
        Vector2 vec = new Vector2(info.X, info.Y);
        GameObject holder = new GameObject();

        foreach (var mapIter in MapData.mapTextMarkers)
        {
            if(mapIter.Key == map){
                foreach (var staticData in mapIter.Value)
                {
                    Vector2 dataVec = new Vector2(staticData.data.X, staticData.data.Y);
                    if(Vector2.Distance(vec, dataVec) <= 0.08f){
                        holder = staticData.gameObject;
                    }

                }

            }

        }

        GameObject temp = Instantiate(MapMarker, holder.transform, true);
        temp.transform.position += this.transform.position + loc;
        temp.GetComponent<SpriteRenderer>().enabled = true;
        temp.GetComponent<SpriteRenderer>().sprite = MapIcons.sprites[info.IconType];
        temp.GetComponent<scr_map_marker_controller>().Text.text = "";
        temp.GetComponent<scr_map_marker_controller>().data = info;
        temp.GetComponent<scr_map_marker_controller>().data.Text = holder.GetComponent<scr_map_marker_controller>().data.Text;
        if(info.IconType == 56 || info.IconType == 57 || info.IconType == 58)
            holder.GetComponent<scr_map_marker_controller>().data.isTownBase = true;
        temp.name = map + "_Items_" + info.IconType;

        return temp.GetComponent<scr_map_marker_controller>();

    }

    scr_map_marker_controller SpawnFacility(MapMarkerInfo info, string map){

        float mod = 8f;
        float mapX = info.X * mod - 4;
        float mapY = (info.Y * mod * -1 + 4f) * .875f;
        Vector3 loc = new Vector3(mapX, mapY, 0f);
        Vector2 vec = new Vector2(info.X, info.Y);
        GameObject holder = new GameObject();

        foreach (var mapIter in MapData.mapTextMarkers)
        {
            if(mapIter.Key == map){
                scr_map_marker_controller prev = null;
                scr_map_marker_controller curr = null;
                foreach (var staticData in mapIter.Value)
                {
                    if(staticData.data.isTownBase == true){
                        curr = staticData;
                        if(prev == null){
                            prev = curr;
                        } else if (Vector2.Distance(new Vector2(prev.data.X, prev.data.Y), vec) < Vector2.Distance(new Vector2(curr.data.X, curr.data.Y), vec)){
                            curr = prev;
                        } else {
                            prev = curr;
                        }
                    }
                    
                }

                if(curr != null && prev != null && prev == curr){
                    holder = curr.gameObject;
                }

            }

        }

        GameObject temp = Instantiate(MapMarker, holder.transform, true);
        temp.transform.position += this.transform.position + loc;
        temp.GetComponent<SpriteRenderer>().enabled = true;
        temp.GetComponent<SpriteRenderer>().sprite = MapIcons.sprites[info.IconType];
        temp.GetComponent<scr_map_marker_controller>().Text.text = "";
        temp.GetComponent<scr_map_marker_controller>().data = info;
        
        if(info.IconType == 33){
            temp.GetComponent<scr_map_marker_controller>().data.Text = holder.GetComponent<scr_map_marker_controller>().data.Text + " Depot";
        } else if (info.IconType == 52){
            temp.GetComponent<scr_map_marker_controller>().data.Text = holder.GetComponent<scr_map_marker_controller>().data.Text + " Seaport";
        }

        temp.name = map + "_Items_" + info.IconType;

        return temp.GetComponent<scr_map_marker_controller>();

    }

}
