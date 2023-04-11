using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTile : Tile
{
    override public int Interact(int hand)
    {
        // Takes anything given to it (should probably look different than a normal counter then, but idk)
        // Essentially, this is both a trash can and a place to put filled orders (let caller handle completing orders, because otherwise passing in the orderController is funky since this is instantiated)

        return 0;
    }
}
