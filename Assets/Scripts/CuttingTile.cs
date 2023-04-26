using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingTile : Tile
{
    // When something is being cut, the player should be locked in the cutting animation until it is complete
    // State
    // 0: empty
    // 1: cutting
    // 3: finished cutting

    private float cuttingPercent;
    private int cuttingObject = 0;

    protected override void Start()
    {
        base.Start();
        cuttingPercent = 0f;
    }

    private void FixedUpdate()
    {
        if (state == 1)
        {
            cuttingPercent += 1f;
            if (cuttingPercent >= 100)
            {
                state = 2;
                cuttingPercent = 0f;
            }
        }
    }

    override public int Interact(int hand)
    {
        int newHand;
        // If the cuttingBoard is empty
        if (state == 0)
        {
            // If the player has uncutTomato/uncutCheese/uncutLettuce, start cutting
            if (hasUncut(hand))
            {
                state = 1;
                cuttingObject = hand;
                newHand = -1;
            }
            // Else do nothing
            else
            {
                newHand = hand;
            }
        }
        // If the player is cutting, the player continues to cut
        else if (state == 1)
        {
            return -1;
        }
        // Else, cutting animation is complete, give player cutTomato/cutCheese/cutLettuce
        else
        {
            state = 0;
            newHand = cuttingObject * 2;
        }

        kitchen.UpdateTileState(x, z, state);
        return newHand;
    }
    private bool hasUncut(int burger)
    {
        return (burger & (16 + 64 + 256)) != 0;
    }
}
