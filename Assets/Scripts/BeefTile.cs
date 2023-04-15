using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeefTile : Tile
{
    override public int Interact(int hand)
    {
        // If the hand is empty, puts uncookedBeef in hand
        if (hand == 0)
        {
            return 1;
        }
        // Else if the hand has only uncookedBeef, removes uncookedBeef from hand
        else if (hand == 1)
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
