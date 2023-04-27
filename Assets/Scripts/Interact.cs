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

    private string lastTag = "";

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
        timer = 0f;
        timerOn = false;
        controller.moveOn();
        burgerDisplay.DisplayBurger(0);
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
        if ((tile == null || tile.GetComponent<Tile>() == null || timerOn) && hand != -1) { return; }
        int prevHand = hand;
        int prevState = tile.GetComponent<Tile>().State;
        timerOn = true;
        PlayerAgent pa = agent.GetComponent<PlayerAgent>();

        // Order complete, determine if it was correct and reward the agent respectively
        if (tile.CompareTag("Table"))
        {
            // Burger fulfilled an order
            if (oc.CompleteOrder(hand))
            {
                // Reward agent
                pa.grantReward(10);
                int ln = pa.getLessonNum();
                if (ln >= 4)
                {
                    if (ln == 6)
                    {
                        if (oc.OrdersDone())
                        {
                            pa.EndEpisode();
                        }
                    }
                    else
                    {
                        pa.EndEpisode();
                    }
                }

                // TODO: add confetti or sfx

            }
            // Burger did not fulfil an order
            else
            {
                // Punish agent
                pa.grantReward(11);

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
        string currTag = tile.tag;
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
            if (pa.getLessonNum() == 0)
            {
                pa.EndEpisode();
            }
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
            if (oc.OrderContains(hand))
            {
                pa.grantReward(7);
            }
        }
        if (currTag == "Oven" && prevHand == 1 && hand == 0) // begin task
        {
            pa.grantReward(8);
            if (pa.getLessonNum() == 1)
            {
                pa.EndEpisode();
            }
        }
        if (currTag == "CuttingBoard" && prevHand != -1 && hand == -1) // begin task
        {
            pa.grantReward(8);
        }
        if (currTag == "Counter" && prevHand != 0 && prevHand != hand) // put things down
        {
            pa.grantReward(12);
        }
        if (currTag == "Counter" && lastTag == "Counter") // prevent cheesing the put things down reward
        {
            pa.grantReward(13);
        }
        if ((hand % 4 >= 2 && hand % 8 >= 4 && hand % 16 >= 8) && // has buns, plate, and burger
            (hand != prevHand) && // something changed
            (currTag == "Oven" || currTag == "Buns" || currTag == "Plates" || // allow building from source
             (currTag == "Counter" && hand != prevState))) // allow building from counter without cheesability
        { 
            pa.grantReward(14);
            if (pa.getLessonNum() == 2)
            {
                pa.EndEpisode();
            }
        }
        if (hand == 686 &&
            (currTag == "Oven" || currTag == "Buns" || currTag == "Plates" || currTag == "CuttingBoard" || // allow building from source
             (currTag == "Counter" && hand != prevState))) // allow building from counter without cheesability
        {
            pa.grantReward(15);
            if (pa.getLessonNum() == 3)
            {
                pa.EndEpisode();
            }
        }
        if (hand == 0 && 
            (currTag == "Buns" || currTag == "Chesse" || currTag == "Lettuce" || currTag == "Beef" || currTag == "Tomatoes" || currTag == "Plates")) // returning materials
        {
            pa.grantReward(16);
        }
        lastTag = currTag;

        // end episode


    }
}