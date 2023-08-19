using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Json_reader : MonoBehaviour
{
    public TextAsset json;

    [System.Serializable]
    public class Primitives 
    {
        public string name;
        public string colour;
    }

    [System.Serializable]
    public class PrimitiveList 
    {
        public Primitives[] primitives;
    }

    public PrimitiveList myPrimitiveList = new PrimitiveList();
    //string json = File.ReadAllText("Assets/Resources/JSON_File.json");
    // Load the JSON file as a TextAsset
    private void Awake()
    {
    }

    void Start()
    {
        json = Resources.Load<TextAsset>("JSON_File");
        myPrimitiveList = JsonUtility.FromJson<PrimitiveList>(json.text);
    }
}
