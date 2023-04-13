using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenTile : Tile
{
    // States
    // 0: empty
    // 1: uncooked
    // 2: cooked

    private float cookingPercent;

    private void Start()
    {
        cookingPercent = 0;
    }

    private void FixedUpdate()
    {
        if (state == 1)
        {
            // cooking speed
            cookingPercent += 0.8f;
            if (cookingPercent >= 100f)
            {
                state = 2;
                cookingPercent = 0;
            }
        }
    }

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
            cookingPercent = 0f;
            state = 0;
        }
        // Else if the oven has cookedBeef and the hand does not have unprepared ingredients or cookedBeef, add cookedBeef to hand
        else if (state == 2 && hasCookedBeeforUnprepared(hand))
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
