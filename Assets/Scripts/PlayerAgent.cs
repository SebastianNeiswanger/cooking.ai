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

    // learning rewards
    // 0 is no reward
    private float movementReward; // 1
    private float survivalReward; // 2
    private float getBreadReward; // 3
    private float getPlateReward; // 4
    private float getBeefReward; // 5
    private float getCookedBeefReward; // 6
    private float getMatReward; // 7
    private float startTaskReward; // 8
    private float finishTaskReward; // 9 IRRELEVANT, WONT BE USED
    private float correctDeliveryReward; // 10
    private float incorrectDeliveryReward; // 11
    private float placementReward; // 12
    private float nonVarietyReward; // 13
    private float basicBurgerReward; // 14
    private float fullBurgerReward; // 15
    private float returnReward; // 16

    void Start()
    {
        intr = interactionObj.GetComponent<Interact>();
        cc = GetComponent<CharacterCtrl>();
        rewardGroup = System.Convert.ToInt32(Academy.Instance.EnvironmentParameters.GetWithDefault("rewardGroup", defaultRewardGroup));
        setRewards(rewardGroup);
        SetReward(0f);
    }

    public void grantReward(int ind)
    {
        switch(ind)
        {
            case 1:
                AddReward(movementReward);
                break;
            case 2:
                AddReward(survivalReward);
                break;
            case 3:
                AddReward(getBreadReward);
                break;
            case 4:
                AddReward(getPlateReward);
                break;
            case 5:
                AddReward(getBeefReward);
                break;
            case 6:
                AddReward(getCookedBeefReward);
                break;
            case 7:
                AddReward(getMatReward);
                break;
            case 8:
                AddReward(startTaskReward);
                break;
            case 9:
                AddReward(finishTaskReward);
                break;
            case 10:
                AddReward(correctDeliveryReward);
                break;
            case 11:
                AddReward(incorrectDeliveryReward);
                break;
            case 12:
                AddReward(placementReward);
                break;
            case 13:
                AddReward(nonVarietyReward);
                break;
            case 14:
                AddReward(basicBurgerReward);
                break;
            case 15:
                AddReward(fullBurgerReward);
                break;
            case 16:
                AddReward(returnReward);
                break;
            default: // including 0
                break; 
        }
    }
    // TODO: Set rewards based on rewardGroup - look at config/cooking.yaml for lesson descriptions
    // gonna be hardcoded because time is of the essence

    private void setRewards(int rg)
    {
        switch (rg)
        {
            case 0: // interact with beef
                movementReward = 0.009f;
                survivalReward = -0.01f;
                getBreadReward = 0.0f;
                getPlateReward = 0f;
                getBeefReward = 1.5f;
                getCookedBeefReward = 100f;
                getMatReward = 0.0f;
                startTaskReward = 0.1f;
                finishTaskReward = 0.1f;
                correctDeliveryReward = 100f;
                incorrectDeliveryReward = 0.0f;
                placementReward = 0f;
                nonVarietyReward = 0f;
                basicBurgerReward = 100f;
                fullBurgerReward = 100f;
                returnReward = -1.5f;
                break;
            case 1: // put beef in the oven
                movementReward = 0.009f;
                survivalReward = -0.01f;
                getBreadReward = 0.02f;
                getPlateReward = 0.02f;
                getBeefReward = 1.0f;
                getCookedBeefReward = 1.5f;
                getMatReward = 0.02f;
                startTaskReward = 0.8f;
                finishTaskReward = 0.5f;
                correctDeliveryReward = 100f;
                incorrectDeliveryReward = 0.0f;
                placementReward = 0.05f;
                nonVarietyReward = -0.06f;
                basicBurgerReward = 100f;
                fullBurgerReward = 100f;
                returnReward = -1.0f;
                break;
            case 2: // grab plate buns and cooked burger
                movementReward = 0.0045f;
                survivalReward = -0.005f;
                getBreadReward = 0.3f;
                getPlateReward = 0.3f;
                getBeefReward = 0.5f;
                getCookedBeefReward = 0.8f;
                getMatReward = 0.02f;
                startTaskReward = 0.4f;
                finishTaskReward = 0.2f;
                correctDeliveryReward = 100f;
                incorrectDeliveryReward = 0.0f;
                placementReward = 0.05f;
                nonVarietyReward = -0.06f;
                basicBurgerReward = 5f;
                fullBurgerReward = 100f;
                returnReward = -0.5f;
                break;
            case 3: // Add all ingredients to the burger
                movementReward = 0.0045f;
                survivalReward = -0.005f;
                getBreadReward = 0.2f;
                getPlateReward = 0.2f;
                getBeefReward = 0.2f;
                getCookedBeefReward = 0.6f;
                getMatReward = 0.1f;
                startTaskReward = 0.3f;
                finishTaskReward = 0.15f;
                correctDeliveryReward = 100f;
                incorrectDeliveryReward = 0.001f;
                placementReward = 0.02f;
                nonVarietyReward = -0.03f;
                basicBurgerReward = 0.1f;
                fullBurgerReward = 5f;
                returnReward = -0.4f;
                break;
            case 4: // serve finished burger to customer
                movementReward = 0.004f;
                survivalReward = -0.005f;
                getBreadReward = 0.15f;
                getPlateReward = 0.15f;
                getBeefReward = 0.15f;
                getCookedBeefReward = 0.2f;
                getMatReward = 0.1f;
                startTaskReward = 0.3f;
                finishTaskReward = 0.15f;
                correctDeliveryReward = 5f;
                incorrectDeliveryReward = 0.3f;
                placementReward = 0.02f;
                nonVarietyReward = -0.03f;
                basicBurgerReward = 0.1f;
                fullBurgerReward = 1f;
                returnReward = -0.4f;
                break;
            case 5: // give a customer a burger based on their order
                movementReward = 0.004f;
                survivalReward = -0.005f;
                getBreadReward = 0.05f;
                getPlateReward = 0.05f;
                getBeefReward = 0.05f;
                getCookedBeefReward = 0.05f;
                getMatReward = 0.3f;
                startTaskReward = 0.2f;
                finishTaskReward = 0.1f;
                correctDeliveryReward = 7f;
                incorrectDeliveryReward = 0.01f;
                placementReward = 0.02f;
                nonVarietyReward = -0.03f;
                basicBurgerReward = 0.3f;
                fullBurgerReward = 0.0f;
                returnReward = -0.3f;
                break;
            case 6: // multiple orders
                movementReward = 0.003f;
                survivalReward = -0.005f;
                getBreadReward = 0.05f;
                getPlateReward = 0.05f;
                getBeefReward = 0.05f;
                getCookedBeefReward = 0.05f;
                getMatReward = 0.3f;
                startTaskReward = 0.5f;
                finishTaskReward = 0.3f;
                correctDeliveryReward = 1.5f;
                incorrectDeliveryReward = 0.01f;
                placementReward = 0.06f;
                nonVarietyReward = -0.07f;
                basicBurgerReward = 0.4f;
                fullBurgerReward = 0.0f;
                returnReward = -0.3f;
                break;
        }
    }

    public override void OnEpisodeBegin()
    {
        // Center the agent
        transform.position = new Vector3(2.0f, -0.49f, 2.0f);

        // Set reward group
        rewardGroup = System.Convert.ToInt32(Academy.Instance.EnvironmentParameters.GetWithDefault("rewardGroup", defaultRewardGroup));

        // Get new kitchen (once the single kitchen Proof of Concept works)
            // kitchen.filepath = random kitchen path

        // Reset kitchen state
        kitchen.DeserializeKitchen();

        // Reset orders
        oc.ResetOrders();
        switch (rewardGroup) {
            case 5:
                oc.CreateNewOrder();
                break;
            case 6:
                oc.CreateNewOrder();
                oc.CreateNewOrder();
                oc.CreateNewOrder();
                break;
            default:
                oc.CreateNewOrder(686); // Full burger
                break;
        }

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
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        bool interact = Input.GetKey(KeyCode.Space);

        ActionSegment<int> actions = actionsOut.DiscreteActions;
        actions[0] = horizontal >= 0 ? horizontal : 2;
        actions[1] = vertical >= 0 ? vertical : 2;
        actions[2] = interact ? 1 : 0;
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        // actions[]:
        // 0: Left and Right (-1, 1)
        // 1: Down and Up (-1, 1)
        // 2: Interact

        cc.horizontal = actions.DiscreteActions[0] <= 1 ? actions.DiscreteActions[0] : -1;
        cc.vertical = actions.DiscreteActions[1] <= 1 ? actions.DiscreteActions[1] : -1;
        bool interact = actions.DiscreteActions[2] > 0;

        if (interact)
        {
            intr.tryInteract();
        }


        // Movement reward
        if (actions.DiscreteActions[0] !=0 || actions.DiscreteActions[1] != 0)
        {
            grantReward(1);
        }

        // Existential penalty
        grantReward(2);

    }
}