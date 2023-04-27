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
    public OrderController oc;
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
        int prevHand = hand;
        timerOn = true;

        // Order complete, determine if it was correct and reward the agent respectively
        if (tile.CompareTag("Table"))
        {
            // Burger fulfilled an order
            if (oc.CompleteOrder(hand))
            {
                // Reward agent
                agent.GetComponent<PlayerAgent>().grantReward(10);

                // TODO: add confetti or sfx

            }
            // Burger did not fulfil an order
            else
            {
                // Punish agent
                agent.GetComponent<PlayerAgent>().grantReward(11);

            }
        }


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

        // Reward agent for interractions
        PlayerAgent pa = agent.GetComponent<PlayerAgent>();
        String currTag = tile.tag;
        int tileState = tile.GetComponent<Tile>().State;
        if (currTag == "Buns" && hand == 4 && prevHand != 4) // get bread
        {
            pa.grantReward(3);
        }
        if (currTag == "Plates" && hand == 8 && prevHand != 8) // get plate
        {
            pa.grantReward(4);
        }
        if (currTag == "Beef" && hand == 1 && prevHand == 0) // get uncooked beef
        {
            pa.grantReward(5);
        }
        if (currTag == "Oven" && hand % 4 >= 2 && prevHand != 2) // get cooked beef
        {
            pa.grantReward(6);
        }
        if (((currTag == "Lettuce" && hand == 256) ||
             (currTag == "Tomatoes" && hand == 64) ||
             (currTag == "Cheese" && hand == 16)) &&
             prevHand == 0) // get materials for burger
        {
            pa.grantReward(7);
        }
        if ((currTag == "Oven" && prevHand == 1 && hand == 0) || 
            (currTag == "CuttingBoard" && prevHand != -1 && hand == -1)) // begin task
        {
            pa.grantReward(8);
        }
        if (false)
        {
            pa.grantReward(12);
        }
        if (false)
        {
            pa.grantReward(13);
        }
        if (false)
        {
            pa.grantReward(14);
        }
        if (false)
        {
            pa.grantReward(15);
        }






    }
}