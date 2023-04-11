using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PlayerAgent : Agent
{
    CharacterController cc;
    public float moveSpeed;
    public KitchenSerializer kitchen;
    public OrderController oc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    public override void OnEpisodeBegin()
    {
        // Center the agent
        transform.position = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent Position
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.z);


        // TODO: Agent burger (what is he holding)
        sensor.AddObservation(0);


        // Tiles and burgers
        // Should be 400 values: type, orientation, state, burgerState
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

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 movement = moveSpeed * Vector3.Normalize(new Vector3(actionBuffers.ContinuousActions[0], 0, actionBuffers.ContinuousActions[1]));
        //Forward/Backward
        cc.SimpleMove(movement);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}