using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using System;

public class KitchenSerializer : MonoBehaviour
{
    public GameObject Beef;
    public GameObject Buns;
    public GameObject Cheese;
    public GameObject Counter;
    public GameObject CuttingBoard;
    public GameObject Lettuce;
    public GameObject Oven;
    public GameObject Plates;
    public GameObject Table;
    public GameObject Tomatoes;
    public string filepath;
    public bool DeserializeOnLoad;
    private List<SerializedTile> data;

    private void Start()
    {
        if (DeserializeOnLoad)
        {
            DeserializeKitchen();
        }
        else
        {
            SerializeKitchen(false);
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.S))
        {
            SerializeKitchen(true);
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
        {
            GenerateRandomKitchen();
        }
    }

    void SerializeKitchen(bool saveToFile)
    {
        // Serialize grid objects
        data = new List<SerializedTile>();

        foreach (Transform child in transform)
        {
            GameObject obj = child.gameObject;
            Tile objTile = obj.GetComponent<Tile>();
            data.Add(new SerializedTile(obj.tag, objTile.X, objTile.Z, objTile.Orientation, objTile.State, 0));
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
                if (index == data.Count || data[index].posX != i || data[index].posZ != j)
                {
                    data.Insert(index, new SerializedTile("Air", i, j, 0, 0, 0));
                }
                ++index;
            }
        }
        if (data.Count > 100)
        {
            Debug.LogError("Too many tiles");
            return;
        }

        if (saveToFile)
        {
            // Write to file
            var sb = new StringBuilder();
            foreach (SerializedTile s in data)
            {
                string json = JsonUtility.ToJson(s, false);
                sb.AppendLine(json);
            }

            File.WriteAllText("Saves/" + filepath, sb.ToString());
        }
    }

    public void DeserializeKitchen()
    {
        // Destroy any existing children
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        data = new List<SerializedTile>();

        IEnumerable<string> jsonLines = File.ReadLines("Saves/" + filepath);
        int x = 0;
        int z = 0;
        foreach (string line in jsonLines)
        {
            SerializedTile tileData = JsonUtility.FromJson<SerializedTile>(line);
            
            tileData.posX = x;
            tileData.posZ = z;

            data.Add(tileData);

            // Create tiles
            GameObject tile;
            switch (tileData.type) {
                case "Beef":
                    tile = Instantiate(Beef, transform);
                    tile.GetComponent<BeefTile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case "Buns":
                    tile = Instantiate(Buns, transform);
                    tile.GetComponent<BunsTile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case "Cheese":
                    tile = Instantiate(Cheese, transform);
                    tile.GetComponent<CheeseTile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case "Counter":
                    tile = Instantiate(Counter, transform);
                    tile.GetComponent<CounterTile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case "CuttingBoard":
                    tile = Instantiate(CuttingBoard, transform);
                    tile.GetComponent<CuttingTile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case "Lettuce":
                    tile = Instantiate(Lettuce, transform);
                    tile.GetComponent<LettuceTile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case "Oven":
                    tile = Instantiate(Oven, transform);
                    tile.GetComponent<OvenTile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case "Plates":
                    tile = Instantiate(Plates, transform);
                    tile.GetComponent<PlatesTile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case "Table":
                    tile = Instantiate(Table, transform);
                    tile.GetComponent<TableTile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case "Tomatoes":
                    tile = Instantiate(Tomatoes, transform);
                    tile.GetComponent<TomatoTile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                default:
                    break;
            }
            if (++z >= 10)
            {
                z = 0;
                ++x;
            }
        }
    }

    void GenerateRandomKitchen()
    {
        data = new List<SerializedTile>();
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                int typeInt = UnityEngine.Random.Range(0, 50);
                string type;
                switch (typeInt)
                {
                    case 0:
                        type = "Buns";
                        break;
                    case 1:
                        type = "Beef";
                        break;
                    case 2:
                        type = "Cheese";
                        break;
                    case 3:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                        type = "Counter";
                        break;
                    case 4:
                        type = "CuttingBoard";
                        break;
                    case 5:
                        type = "Lettuce";
                        break;
                    case 6:
                        type = "Oven";
                        break;
                    case 7:
                        type = "Plates";
                        break;
                    case 8:
                        type = "Table";
                        break;
                    case 9:
                        type = "Tomatoes";
                        break;
                    default:
                        type = "Air";
                        break;
                }
                data.Add(new SerializedTile(type, i, j, 0, 0, 0));
            }
        }
        var sb = new StringBuilder();
        foreach (SerializedTile s in data)
        {
            string json = JsonUtility.ToJson(s, false);
            sb.AppendLine(json);
        }

        File.WriteAllText("Saves/" + filepath, sb.ToString());
        DeserializeKitchen();
    }

    public void UpdateTileState(int tileX, int tileZ, int newState)
    {
        int index = (10 * tileX) + tileZ;
        if (index < 0 || index > data.Count)
        {
            Debug.LogError("Ignoring invalid tile update request: (" + tileX + ", " + tileZ + ", " + newState + ")");
            return;
        }
        data[index].state = newState;
    }

    public List<int> SendStateToAgent()
    {
        List<int> observations = new List<int>();
        foreach (SerializedTile tile in data)
        {
            observations.Add(tile.getTypeInt());
            observations.Add(tile.orientation);
            observations.Add(tile.state);
        }
        return observations;
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

    public SerializedTile(string t, int x, int z, int o, int s, int b)
    {
        type = t;
        Debug.Log(type.ToString());
        this.x = x;
        this.z = z;
        orientation = o;
        state = s;
    }

    public int getTypeInt()
    {
        switch (type)
        {
            case "Beef":
                return 0;
            case "Buns":
                return 1;
            case "Cheese":
                return 2;
            case "Counter":
                return 3;
            case "CuttingBoard":
                return 4;
            case "Lettuce":
                return 5;
            case "Oven":
                return 6;
            case "Plates":
                return 7;
            case "Table":
                return 8;
            case "Tomatoes":
                return 9;
            default:
                return -1;
        }
    }
}