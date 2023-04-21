using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    private bool insideObject;
    private Collider currentTile = null;
    private int hand;
    public GameObject agent;
    public BurgerDisplay burgerDisplay;
    private CharacterCtrl controller;

    private void Start()
    {
        insideObject = false;
        hand = 0;
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
        currentTile = null;
        Debug.Log("Left " + other);
    }

    // Helper function that runs interact and 
    private void helper(Collider tile)
    {
        if (tile == null) { return; }
        int returnedHand = tile.GetComponent<Tile>().Interact(hand);
        Debug.Log(returnedHand + " from " + tile);
        hand = returnedHand;
        burgerDisplay.DisplayBurger(returnedHand);
        if (hand == -1)
        {
            controller.moveOff();
        } else
        {
            controller.moveOn();
        }
    }
}