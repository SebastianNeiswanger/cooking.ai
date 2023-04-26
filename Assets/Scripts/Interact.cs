using System;
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
    private string[] interactableTags = { "Beef", "Buns", "Cheese", "Counter", "CuttingBoard", "Lettuce", "Oven", "Plates", "Table", "Tomatoes" };

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
        if (!Array.Exists(interactableTags, elm => other.CompareTag(elm))) { return; }
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
    }

    // Helper function that runs interact and 
    private void helper(Collider tile)
    {
        if (tile == null) { return; }
        int returnedHand = tile.GetComponent<Tile>().Interact(hand);
        hand = returnedHand;
        if (hand == -1)
        {
            burgerDisplay.DisplayBurger(0);
            controller.moveOff();
        } else
        {
            burgerDisplay.DisplayBurger(returnedHand);
            controller.moveOn();
        }
    }
}