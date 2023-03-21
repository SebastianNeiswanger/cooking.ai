using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Divider : MonoBehaviour
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
        // This could be cleaned up, but basically orientation is 0 if vertical and 1 if horizontal
        if (transform.rotation.eulerAngles.y >= 45 && transform.rotation.eulerAngles.y <= 135)
        {
            orientation = 1;
            x = Convert.ToInt32(transform.position.x + 4.5);
            z = Convert.ToInt32(transform.position.z + 5);
        }
        else
        {
            orientation = 0;
            x = Convert.ToInt32(transform.position.x + 5);
            z = Convert.ToInt32(transform.position.z + 4.5);
        }
    }

    public void Deserialize(string subtype, int x, int z, int orientation)
    {
        this.subtype = subtype;
        this.x = x;
        this.z = z;
        this.orientation = orientation;

        // Set position and orientation
        switch (orientation)
        {
            case 0:
                transform.SetPositionAndRotation(new Vector3((float)(x - 5f), 0, (float)(z - 4.5f)), Quaternion.AngleAxis(0, Vector3.up));
                break;
            case 1:
                transform.SetPositionAndRotation(new Vector3((float)(x - 4.5f), 0, (float)(z - 5f)), Quaternion.AngleAxis(90, Vector3.up));
                break;
            default:
                Debug.LogError("Invalid orientation");
                break;
        }
    }
}
