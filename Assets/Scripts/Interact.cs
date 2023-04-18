using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private bool insideObject;
    private Collider currentTile;
    private int hand;
    public GameObject agent;
    private CharacterCtrl controller;

    private void Start()
    {
        insideObject = false;
        hand = 16;
        controller = agent.GetComponent<CharacterCtrl>();
    }

    // Run interact by input
    public void tryInteract()
    {
        if (insideObject)
        {
            helper(currentTile);
        }
    }

    // Trigger functions
    private void OnTriggerEnter(Collider other)
    {
        insideObject = true;
        currentTile = other;
    }
    private void OnTriggerStay(Collider other)
    {
        if (hand == -1)
        {
            helper(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        insideObject = false;
        Debug.Log("Left " + other);
    }

    // Helper function that runs interact and 
    private void helper(Collider tile)
    {
        int returnedHand = tile.GetComponent<Tile>().Interact(hand);
        Debug.Log(returnedHand + " from " + tile);
        hand = returnedHand;
        if (hand == -1)
        {
            controller.moveOff();
        } else
        {
            controller.moveOn();
        }
    }
}