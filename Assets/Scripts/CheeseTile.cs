using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseTile : Tile
{
    override public int Interact(int hand)
    {
        // If the hand is empty, puts uncutCheese in hand
        if (hand == 0)
        {
            return 16;
        }
        // Else if the hand has only uncutCheese, removes uncutCheese from hand
        else if (hand == 16)
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
