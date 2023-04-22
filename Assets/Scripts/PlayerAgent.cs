using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PlayerAgent : Agent
{
    CharacterCtrl cc; 
    public GameObject interactionObj;
    private Interact intr;
    private int rewardGroup;
    public int defaultRewardGroup;
    public KitchenSerializer kitchen;
    public OrderController oc;

    void Start()
    {
        intr = interactionObj.GetComponent<Interact>();
        cc = GetComponent<CharacterCtrl>();
        rewardGroup = (int) Academy.Instance.EnvironmentParameters.GetWithDefault("rewardGroup", defaultRewardGroup);
    }

    // TODO: Set rewards based on rewardGroup.

    public override void OnEpisodeBegin()
    {
        // Center the agent
        transform.position = new Vector3(2.0f, -0.49f, 2.0f);

        // Set reward group
        rewardGroup = (int)Academy.Instance.EnvironmentParameters.GetWithDefault("rewardGroup", defaultRewardGroup);

        // Get new kitchen (once the single kitchen PoC works)
            // kitchen.filepath = random kitchen path
        // Reset kitchen state
        kitchen.DeserializeKitchen();

        // Reset orders
        oc.ResetOrders();
        oc.CreateNewOrder(686); // Full burger

        // TODO: Clear hand
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent Position
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.z);


        // TODO: Agent burger (what is he holding)
        sensor.AddObservation(0);


        // Tiles and burgers
        // Should be 300 values: type, orientation, state
        foreach (int i in kitchen.SendStateToAgent())
        {
            sensor.AddObservation(i);
        }


        // Orders
        List<int> orders = oc.SendOrdersToAgent();
        // Up to 5 orders can be sent to the agent
        for (int i = 0; i < 5; ++i)
        {
            if (orders.Count > i)
            {
                sensor.AddObservation(orders[i]);
            } else
            {
                sensor.AddObservation(0);
            }
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // actions[]:
        // 0: Left and Right (-1, 1)
        // 1: Down and Up (-1, 1)
        // 2: Interact

        int horizontal = actions.DiscreteActions[0] <= 1 ? actions.DiscreteActions[0] : -1;
        int vertical = actions.DiscreteActions[1] <= 1 ? actions.DiscreteActions[1] : -1;
        bool interact = actions.DiscreteActions[2] > 0;

        switch (vertical)
        {
            case -1:
                if (horizontal == -1)
                {
                    cc.DirectionDegrees = 225f;
                    cc.ForwardInput = 1;
                } else if (horizontal == 0)
                {
                    cc.DirectionDegrees = 180f;
                    cc.ForwardInput = 1;
                } else
                {
                    cc.DirectionDegrees = 135f;
                    cc.ForwardInput = 1;
                }
                break;
            case 0:
                if (horizontal == -1)
                {
                    cc.DirectionDegrees = 270f;
                    cc.ForwardInput = 1;
                }
                else if (horizontal == 0)
                {
                    cc.ForwardInput = 0;
                }
                else
                {
                    cc.DirectionDegrees = 90f;
                    cc.ForwardInput = 1;
                }
                break;
            case 1:
                if (horizontal == -1)
                {
                    cc.DirectionDegrees = 315f;
                    cc.ForwardInput = 1;
                }
                else if (horizontal == 0)
                {
                    cc.DirectionDegrees = 0f;
                    cc.ForwardInput = 1;
                }
                else
                {
                    cc.DirectionDegrees = 45f;
                    cc.ForwardInput = 1;
                }
                break;
        }

        if (interact)
        {
            intr.tryInteract();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));

        ActionSegment<int> actions = actionsOut.DiscreteActions;
        actions[0] = horizontal >= 0 ? horizontal : 2;
        actions[1] = vertical >= 0 ? vertical : 2;
        actions[2] = Input.GetKeyDown("space") ? 1 : 0;
    }
}