using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class Crate {

    public string name;
    public int amount;
    public int quota;

    public Crate(){}

    public Crate(JObject jobject){
        if(jobject.ContainsKey("name")){
            name = (string)jobject["name"];
        }
        if(jobject.ContainsKey("amount")){
            amount = (int)jobject["amount"];
        }
        if(jobject.ContainsKey("quota")){
            quota = (int)jobject["quota"];
        }
    }

}
