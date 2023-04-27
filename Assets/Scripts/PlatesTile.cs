using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesTile : Tile
{
    // State = number of plates?
    override public int Interact(int hand)
    {
        // int newHand;
        // If the hand is has no unprepared ingredients or plate, puts plate in hand
        if (!hasUnpreparedOrPlate(hand))
        {
            return hand + 8;
            // newHand = hand + 8;
            // --state
        }
        // Else if the hand has only plate, removes plate from hand
        else if (hand == 8)
        {
            return 0;
            // newHand = 0;
            // ++state
        }
        // Else do nothing
        else
        {
            return hand;
        }
        
        // kitchen.UpdateTileState(x, z, state);
        // return newHand;
    }
    private bool hasUnpreparedOrPlate(int burger)
    {
        return (burger & (1 + 8 + 16 + 64 + 256)) != 0;
    }
}
