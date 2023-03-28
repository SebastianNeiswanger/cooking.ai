using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class KitchenSerializer : MonoBehaviour
{
    public GameObject Tile;
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

    // TODO: Add a bool to distinguish between sending to ML-Agent and writing to JSON file
    void SerializeKitchen()
    {
        // Serialize grid objects
        List<SaveLoadObjects> data = new List<SaveLoadObjects>();
        List<Transform> objects = new List<Transform>(gameObject.GetComponentsInChildren<Transform>());

        // Remove serializer object
        objects.RemoveAt(0);

        foreach (Transform t in objects)
        {
            GameObject obj = t.gameObject;
            Tile objTile = obj.GetComponent<Tile>();
            data.Add(new SaveLoadObjects(obj.tag, objTile.X, objTile.Z, objTile.Orientation, objTile.State));
        }

        // Sort objects on grid
        data.Sort((a, b) =>
            {
                if (a.posX == b.posX)
                {
                    return a.posZ - b.posZ;
                } else
                {
                    return a.posX - b.posX;
                }
            }
        );

        // Add air tiles
        int index = 0;
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                if (data[index].posX != i || data[index].posZ != j)
                {
                    data.Insert(index, new SaveLoadObjects("Air", i, j, 0, 0));
                }
                ++index;
            }
        }
        if (data.Count > 100)
        {
            Debug.LogError("Too many tiles");
            return;
        }


        // Add to json
        var sb = new StringBuilder();
        foreach (SaveLoadObjects s in data) {
            string json = JsonUtility.ToJson(s, false);
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
            // Don't create an air tile
            if (data.type == "Air")
            {
                continue;
            }
            GameObject newTile = Instantiate(Tile, transform);
            newTile.GetComponent<Tile>().Deserialize(data.type, data.posX, data.posZ, data.orientation, data.state);
        }
    }
}

class SaveLoadObjects
{
    public int posX;
    public int posZ;
    public int orientation;
    public int state;
    public string type;

    public SaveLoadObjects(string t, int x, int z, int o, int s)
    {
        type = t;
        posX = x;
        posZ = z;
        orientation = o;
        state = s;
    }
}