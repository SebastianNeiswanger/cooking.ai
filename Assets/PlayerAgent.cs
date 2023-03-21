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
        sensor.AddObservation(transform.localPosition);
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