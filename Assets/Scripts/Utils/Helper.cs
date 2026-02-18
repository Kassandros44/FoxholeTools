using System.Collections;
using System;
using System.IO;
using System.Reflection;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public static class Helper{

    public static string discordClientID = "1138105236007948341";
    public static string discordRedrectURI = "https://localhost:7253/discord-login";

    #if UNITY_EDITOR
    public static string apiHost = Environment.GetEnvironmentVariable("LOCALHOST");
    public static string apiPort = Environment.GetEnvironmentVariable("LOCAL_PORT");
    #elif UNITY_STANDALONE_WIN
    public static string apiHost = "https://foxholetoolsapi.azurewebsites.net";
    public static string apiPort = "";
    #endif

    public static void LoadDotEnv(){
        string root = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(root, ".env");

        if(!File.Exists(filePath)){
            Debug.Log($"FILE NOT FOUND:::{filePath}");
            return;
        }
        foreach(var line in File.ReadAllLines(filePath)){
            var parts = line.Split(
                "=",
                StringSplitOptions.RemoveEmptyEntries);
            if(parts.Length != 2){
                continue;
            }
            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }

    }

    public delegate T ObjectActivator<T>(params object[] args);

    public static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor){

        Type type = ctor.DeclaringType;
        ParameterInfo[] paramsInfo = ctor.GetParameters();

        ParameterExpression param = Expression.Parameter(typeof(object[]), "args");

        Expression[] argsExp = new Expression[paramsInfo.Length];

        for (int i = 0; i < paramsInfo.Length; i++){

            Expression index = Expression.Constant(i);
            Type paramType = paramsInfo[i].ParameterType;

            Expression paramAccessorExp = Expression.ArrayIndex(param, index);

            Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);

            argsExp[i] = paramCastExp;

        }

        NewExpression newExp = Expression.New(ctor, argsExp);

        LambdaExpression lambda = Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

        ObjectActivator<T> compiled = (ObjectActivator<T>)lambda.Compile();
        return compiled;

    }

    public static List<T> GetListFromData<T>(string data) where T : class{
        
        ConstructorInfo ctor = typeof(T).GetConstructor(new[] {typeof(JObject)});
        ObjectActivator<T> createdActivator = GetActivator<T>(ctor);

        List<T> list = new List<T>();
        JArray jArray = JArray.Parse(data);
        foreach(var i in jArray){
            JObject jObject = JObject.Parse(i.ToString());
            T newObject = createdActivator(jObject);
            //T newObject = (T)Activator.CreateInstance(typeof(T), jObject);
            list.Add(newObject);
        }
        return list;
    }

    public static List<T> GetListFromStruct<T>(string data) where T : struct
    {

        ConstructorInfo ctor = typeof(T).GetConstructor(new[] { typeof(JObject) });
        ObjectActivator<T> createdActivator = GetActivator<T>(ctor);

        List<T> list = new List<T>();
        JArray jArray = JArray.Parse(data);
        foreach (var i in jArray)
        {
            JObject jObject = JObject.Parse(i.ToString());
            T newObject = createdActivator(jObject);
            //T newObject = (T)Activator.CreateInstance(typeof(T), jObject);
            list.Add(newObject);
        }
        return list;
    }

    public static T GetObjectFromData<T>(string data) where T : class{

        ConstructorInfo ctor = typeof(T).GetConstructor(new[] {typeof(JObject)});
        ObjectActivator<T> createdActivator = GetActivator<T>(ctor);

        JObject jobject = JObject.Parse(data);
        T newObject = createdActivator(jobject);
        return newObject;
    }

}
