using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string subtype;
    private int x;
    private int z;
    private int orientation;

    public int X { get => x; set => x = value; }
    public int Z { get => z; set => z = value; }
    public int Orientation { get => orientation; set => orientation = value; }

    private void Start()
    {
        // This could be cleaned up. Orientation is based on current rotation
        if (transform.rotation.eulerAngles.y >= 45 && transform.rotation.eulerAngles.y <= 135)
        {
            orientation = 1;
        }
        else if (transform.rotation.eulerAngles.y >= 135 && transform.rotation.eulerAngles.y <= 225)
        {
            orientation = 2;
        }
        else if (transform.rotation.eulerAngles.y >= 225 && transform.rotation.eulerAngles.y <= 315)
        {
            orientation = 3;
        }
        else
        {
            orientation = 0;
        }

        x = Convert.ToInt32(transform.position.x + 4.5);
        z = Convert.ToInt32(transform.position.z + 4.5);
    }

    public void Deserialize(string subtype, int x, int z, int orientation)
    {
        this.x = x;
        this.z = z;
        this.orientation = orientation;

        // Set position and orientation
        transform.position = new Vector3((float)(x - 4.5f), 0, (float)(z - 4.5f));
        switch (orientation)
        {
            case 0:
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                break;
            case 1:
                transform.rotation = Quaternion.AngleAxis(90, Vector3.up);
                break;
            case 2:
                transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
                break;
            case 3:
                transform.rotation = Quaternion.AngleAxis(270, Vector3.up);
                break;
            default:
                Debug.LogError("Invalid orientation");
                break;
        }

    }
}
