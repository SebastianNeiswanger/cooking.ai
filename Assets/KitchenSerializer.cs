using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class KitchenSerializer : MonoBehaviour
{
    public GameObject Tile;
    public GameObject Divider;
    private string filepath = "Test.json";

    private void Start()
    {
        SerializeKitchen();
        DeserializeKitchen();
    }
    void SerializeKitchen()
    {
        // Serialize grid objects
        SaveLoadObjects data = new SaveLoadObjects();
        List<GameObject> objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Tile"));
        objects.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Divider")));
        var sb = new StringBuilder();

        foreach (GameObject obj in objects)
        {
            //TODO: all of this
            data.type = obj.tag;
            // Tile type
            data.objName = obj.name;
            // Grid spaces?
            data.posX = obj.transform.position.x;
            data.posZ = obj.transform.position.z;

            string json = JsonUtility.ToJson(data, false);
            sb.AppendLine(json);
        }

        File.WriteAllText(filepath, sb.ToString());
    }

    void DeserializeKitchen()
    {
        IEnumerable<string> jsonLines = File.ReadLines(filepath);
        foreach (string line in jsonLines)
        {
            SaveLoadObjects data = JsonUtility.FromJson<SaveLoadObjects>(line);
            if (data.type == "Tile")
            {
                Instantiate(Tile);
                Tile.GetComponent<Tile>().Deserialize(data.type, data.objName, data.posX, data.posZ);
            } else if (data.type == "Divider")
            {
                Instantiate(Divider);
                Divider.GetComponent<Divider>().Deserialize(data.type, data.objName, data.posX, data.posZ);
            }
        }
    }
}

class SaveLoadObjects
{
    public string type;
    public string objName;
    public float posX;
    public float posZ;
}