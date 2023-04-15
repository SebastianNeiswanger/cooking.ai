using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettuceTile : Tile
{
    override public int Interact(int hand)
    {
        // If the hand is empty, puts uncutLettuce in hand
        if (hand == 0)
        {
            return 256;
        }
        // Else if the hand has only uncutLettuce, removes uncutLettuce from hand
        else if (hand == 256)
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
