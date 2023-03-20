using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class KitchenSerializer : MonoBehaviour
{
    public GameObject Tile;
    public GameObject Divider;
    public string filepath;
    public bool DeserializeOnLoad;

    private void Start()
    {
        if (DeserializeOnLoad)
        {
            DeserializeKitchen();
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S))
        {
            SerializeKitchen();
        }
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
            data.type = obj.tag;

            if (obj.CompareTag("Tile"))
            {
                Tile objTile = obj.GetComponent<Tile>();
                data.subtype = objTile.subtype;
                // Grid spaces
                data.posX = objTile.X;
                data.posZ = objTile.Z;
                data.orientation = objTile.Orientation;
            }
            else if (obj.CompareTag("Divider"))
            {
                Divider objDivider = obj.GetComponent<Divider>();
                data.subtype = objDivider.subtype;
                // Grid spaces
                data.posX = objDivider.X;
                data.posZ = objDivider.Z;
                data.orientation = objDivider.Orientation;
            }

            string json = JsonUtility.ToJson(data, false);
            sb.AppendLine(json);
        }

        File.WriteAllText("Saves/" + filepath, sb.ToString());
    }

    void DeserializeKitchen()
    {
        IEnumerable<string> jsonLines = File.ReadLines("Saves/" + filepath);
        foreach (string line in jsonLines)
        {
            SaveLoadObjects data = JsonUtility.FromJson<SaveLoadObjects>(line);
            if (data.type == "Tile")
            {
                GameObject newTile = Instantiate(Tile);
                newTile.GetComponent<Tile>().Deserialize(data.subtype, data.posX, data.posZ, data.orientation);
            }
            else if (data.type == "Divider")
            {
                GameObject newDivider = Instantiate(Divider);
                newDivider.GetComponent<Divider>().Deserialize(data.subtype, data.posX, data.posZ, data.orientation);
            }
        }
    }
}

class SaveLoadObjects
{
    public string type;
    public string subtype;
    public int posX;
    public int posZ;
    public int orientation;
}