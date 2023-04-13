using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingTile : Tile
{
    // When something is being cut, the player will be locked into the cutting animation and cannot move unless they stop cutting.
    // TODO: Implement cutting animation/time
    // State
    // 0: empty
    // 1: cutting
    // 3: finished cutting

    override public int Interact(int hand)
    {
        // If the cuttingBoard is empty
        if (state == 0)
        {
            // If the player has uncutTomato/uncutCheese/uncutLettuce, start cutting
            if (hasUncut(hand))
            {
                state = 1;
                return -1;
            }
            // Else do nothing
            else
            {
                return hand;
            }
        }
        // If the player is cutting, the player stops cutting
        else if (state == 1)
        {
            state = 0;
            return hand;
        }
        // Else, cutting animation is complete, give player cutTomato/cutCheese/cutLettuce
        else
        {
            state = 0;
            return hand * 2;
        }
    }
    private bool hasUncut(int burger)
    {
        return (burger & (16 + 64 + 256)) != 0;
    }
}
