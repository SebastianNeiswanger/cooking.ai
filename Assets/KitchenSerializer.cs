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
    private List<SerializedTile> data;

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
        data = new List<SerializedTile>();
        List<Transform> objects = new List<Transform>(gameObject.GetComponentsInChildren<Transform>());

        // Remove serializer object
        objects.RemoveAt(0);

        foreach (Transform t in objects)
        {
            GameObject obj = t.gameObject;
            Tile objTile = obj.GetComponent<Tile>();
            data.Add(new SerializedTile(obj.tag, objTile.X, objTile.Z, objTile.Orientation, objTile.State));
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
                    data.Insert(index, new SerializedTile("Air", i, j, 0, 0));
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
        foreach (SerializedTile s in data) {
            string json = JsonUtility.ToJson(s, false);
            sb.AppendLine(json);
        }

        File.WriteAllText("Saves/" + filepath, sb.ToString());
    }

    void DeserializeKitchen()
    {
        IEnumerable<string> jsonLines = File.ReadLines("Saves/" + filepath);
        int x = 0;
        int z = 0;
        foreach (string line in jsonLines)
        {
            SerializedTile tileData = JsonUtility.FromJson<SerializedTile>(line);
            // Don't create an air tile
            if (tileData.type != "Air")
            {
                tileData.posX = x;
                tileData.posZ = z;
                // TODO: Instantiate prefab based on type
                GameObject newTile = Instantiate(Tile, transform);
                newTile.GetComponent<Tile>().Deserialize(tileData.type, tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
            }
            if (++z >= 10)
            {
                z = 0;
                ++x;
            }
        }
    }
}

class SerializedTile
{
    private int x;
    private int z;
    public int orientation;
    public int state;
    public string type;

    public int posX { get => x; set => x = value; }
    public int posZ { get => z; set => z = value; }

    public SerializedTile(string t, int x, int z, int o, int s)
    {
        type = t;
        this.x = x;
        this.z = z;
        orientation = o;
        state = s;
    }
}