using Newtonsoft.Json.Linq;

public class MapMarkerInfo
{
    public string Text;
    public int IconType;
    public int Team;
    public int MarkerType;
    public float X;
    public float Y;
    public bool isTownBase;
    public MapMarkerInfo()
    {
    }
    public MapMarkerInfo(JObject jObject)
    {
        if (jObject.ContainsKey("iconType"))
            IconType = (int)jObject["iconType"];
            int[] towns = {56, 57, 58};
            for (int i = 0; i < towns.Length; i++)
            {
                if(IconType == i){
                    isTownBase = true;
                } else {
                    isTownBase = false;
                }
            }

        if (jObject.ContainsKey("text"))
            Text = (string)jObject["text"];

        if (jObject.ContainsKey("x"))
            X = (float)jObject["x"];

        if (jObject.ContainsKey("y"))
            Y = (float)jObject["y"];

        if (jObject.ContainsKey("teamId"))
        {
            if ((string)jObject["teamId"] == "WARDENS")
            {
                Team = 1;
            }
            else if ((string)jObject["teamId"] == "COLONIALS")
            {
                Team = 2;
            }
            else
            {
                Team = 0;
            }
        }

        if (jObject.ContainsKey("mapMarkerType"))
        {
            switch ((string)jObject["mapMarkerType"])
            {
                case "Major":
                    MarkerType = 1;
                    break;
                case "Minor":
                    MarkerType = 0;
                    break;
            }
        }
    }
}
