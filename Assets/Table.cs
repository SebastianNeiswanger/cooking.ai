using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : Tile
{
    override public int Interact(int hand)
    {
        // Takes anything given to it (should probably look different than a normal counter then, but idk)
        // Essentially, this is both a trash can and a place to put filled orders (let caller handle completing orders)

        return 0;
    }
}
