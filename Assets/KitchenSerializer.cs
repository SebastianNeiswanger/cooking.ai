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

    void DeserializeKitchen()
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
                case TileType.Beef:
                    tile = Instantiate(Beef, transform);
                    tile.GetComponent<Tile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case TileType.Buns:
                    tile = Instantiate(Buns, transform);
                    tile.GetComponent<Tile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case TileType.Cheese:
                    tile = Instantiate(Cheese, transform);
                    tile.GetComponent<Tile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case TileType.Counter:
                    tile = Instantiate(Counter, transform);
                    tile.GetComponent<Tile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case TileType.CuttingBoard:
                    tile = Instantiate(CuttingBoard, transform);
                    tile.GetComponent<Tile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case TileType.Lettuce:
                    tile = Instantiate(Lettuce, transform);
                    tile.GetComponent<Tile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case TileType.Oven:
                    tile = Instantiate(Oven, transform);
                    tile.GetComponent<Tile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case TileType.Plates:
                    tile = Instantiate(Plates, transform);
                    tile.GetComponent<Tile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case TileType.Table:
                    GameObject newTile = Instantiate(Table, transform);
                    newTile.GetComponent<Tile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
                    break;
                case TileType.Tomatoes:
                    tile = Instantiate(Tomatoes, transform);
                    tile.GetComponent<Tile>().Deserialize(tileData.type.ToString(), tileData.posX, tileData.posZ, tileData.orientation, tileData.state);
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

    void UpdateTileState(int tileX, int tileZ, int newState)
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
            observations.Add((int) tile.type);
            observations.Add(tile.orientation);
            observations.Add(tile.state);
            observations.Add(tile.burgerState);
        }
        return observations;
    }

}

enum TileType
{
    Air,
    Beef,
    Buns,
    Cheese,
    Counter,
    CuttingBoard,
    Lettuce,
    Oven,
    Plates,
    Table,
    Tomatoes,
}

class SerializedTile
{
    private int x;
    private int z;
    private int bugerState;
    // Burger states (flags):
    // 1: uncooked beef
    // 2: cooked beef
    // 4: buns
    // 8: plate
    // 16: cheese
    // 32: tomato
    // 64: lettuce
    public int orientation;
    public int state;
    public TileType type;

    public int posX { get => x; set => x = value; }
    public int posZ { get => z; set => z = value; }
    public int burgerState { get => bugerState; set => bugerState = value; }

    public SerializedTile(string t, int x, int z, int o, int s, int b)
    {
        if (!Enum.TryParse<TileType>(t, out type))
        {
            Debug.LogError("Cound not parse type: " + t);
            type = TileType.Air;
        }
        this.x = x;
        this.z = z;
        orientation = o;
        state = s;
        burgerState = b;
    }
}