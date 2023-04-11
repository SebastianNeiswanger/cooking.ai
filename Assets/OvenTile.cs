using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTile : Tile
{
    // TODO: Implement cooking time
    // States
    // 0: empty
    // 1: uncooked
    // 2: cooked

    override public int Interact(int hand)
    {
        int newHand;
        // If the oven is empty and the player is holding uncooked beef, takes the uncooked beef from the hand
        if (state == 0 && hand == 1)
        {
            newHand = 0;
            state = 1;
        }
        // Else if the oven has uncookedBeef and the hand is empty, puts the oven's beef in hand
        else if (state == 1 && hand == 0)
        {
            newHand = 1;
            state = 0;
        }
        // Else if the oven has cookedBeef and the hand does not have unprepared ingredients or cookedBeef, add cookedBeef to hand
        else if (hasCookedBeeforUnprepared(hand))
        {
            newHand = 2 + hand;
            state = 0;
        }
        // Else do nothing
        else
        {
            return hand;
        }

        kitchen.UpdateTileState(x, z, state);
        return newHand;
    }
    private bool hasCookedBeeforUnprepared(int burger)
    {
        return (burger & (1 + 2 + 16 + 64 + 256)) != 0;
    }
}