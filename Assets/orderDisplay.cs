using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orderDisplay : MonoBehaviour
{
    public GameObject tomato;
    public GameObject lettuce;
    public GameObject beef;
    public GameObject cheese;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dispOrder(int order)
    {
        if (order % 4 >= 2)
        {
            beef.SetActive(true);
        }
        else
        {
            beef.SetActive(false);
        }
        if (order % 64 >= 32)
        {
            cheese.SetActive(true);
        }
        else
        {
            cheese.SetActive(false);
        }
        if (order % 256 >= 128)
        {
            tomato.SetActive(true);
        }
        else
        {
            tomato.SetActive(false);
        }
        if (order % 1024 >= 512)
        {
            lettuce.SetActive(true);
        }
        else
        {
            lettuce.SetActive(false);
        }
    }
}
