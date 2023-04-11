using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : Tile
{
    override public int Interact(int hand)
    {
        int newHand = 0;
        // if counter is empty, put hand item on counter
        if (state == 0)
        {
            state = hand;
            newHand = 0;
        }
        // if object in hand or on counter has unprepared or duplicate ingredient, swap.
        else if (hasUnprepared(hand) || hasUnprepared(state) || (hand & state) != 0)
        {
            newHand = state;
            state = hand;
        }
        // Otherwise, combine the two and put in hand.
        else
        {
            newHand = (hand | state);
            state = 0;
        }

        kitchen.UpdateTileState(x, z, state);
        return newHand;
    }
    private bool hasUnprepared(int burger)
    {
        // bitfield
        return (burger & (1 + 16 + 64 + 256)) != 0;
    }
}
