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

    private float timer;
    private bool timerOn = false;
    public float timeBetweenInteracts = 50f;

    private string[] interactableTags = { "Beef", "Buns", "Cheese", "Counter", "CuttingBoard", "Lettuce", "Oven", "Plates", "Table", "Tomatoes" };

    private void Start()
    {
        insideObject = false;
        hand = 0;
        timer = 0f;
        controller = agent.GetComponent<CharacterCtrl>();
    }

    private void FixedUpdate()
    {
        if (!timerOn) { return; }
        timer += 1f;
        if (timer >= timeBetweenInteracts)
        {
            timer = 0f;
            timerOn = false;
        }
    }

    public void resetHand()
    {
        hand = 0;
    }

    public int getHand()
    {
        return hand;
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
            helper(currentTile);
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
        if ((tile == null || timerOn) && hand != -1) { return; }
        timerOn = true;
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