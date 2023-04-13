using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunsTile : Tile
{
    override public int Interact(int hand)
    {
        // If the hand has no buns or unpreparedIngredients, puts buns in hand
        if (!hasUnpreparedorBuns(hand))
        {
            return hand + 4;
        }
        // Else if the hand has only buns, removes buns from hand
        else if (hand == 4)
        {
            return 0;
        }
        // Else do nothing
        else
        {
            return hand;
        }
    }
    private bool hasUnpreparedorBuns(int burger)
    {
        return (burger & (1 + 4 + 16 + 64 + 256)) != 0;
    }
}
