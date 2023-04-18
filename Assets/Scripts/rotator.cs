using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{
    void Update()
    {
        // rotate around the World's Y axis
        transform.Rotate(Vector3.up * Time.deltaTime * 120, Space.World);
    }
}


