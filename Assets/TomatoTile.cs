using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoTile : Tile
{
    override public int Interact(int hand)
    {
        // If the hand is empty, puts uncutTomato in hand
        if (hand == 0)
        {
            return 64;
        }
        // Else if the hand has only uncutTomato, removes uncutTomato from hand
        else if (hand == 64)
        {
            return 0;
        }
        // Else do nothing
        else
        {
            return hand;
        }
    }
}
