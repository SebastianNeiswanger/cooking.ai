using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeuristcMove : MonoBehaviour
{
    private CharacterCtrl characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        if (vertical == 0 && horizontal == 1)
        {
            characterController.DirectionDegrees = 90f;
            characterController.ForwardInput = 1;
        }
        else if (vertical == 1 && horizontal == 1)
        {
            characterController.DirectionDegrees = 45f;
            characterController.ForwardInput = 1;
        }
        else if (vertical == 1 && horizontal == 0)
        {
            characterController.DirectionDegrees = 0f;
            characterController.ForwardInput = 1;
        }
        else if (vertical == 1 && horizontal == -1)
        {
            characterController.DirectionDegrees = 315f;
            characterController.ForwardInput = 1;
        }
        else if (vertical == 0 && horizontal == -1)
        {
            characterController.DirectionDegrees = 270f;
            characterController.ForwardInput = 1;
        }
        else if (vertical == -1 && horizontal == -1)
        {
            characterController.DirectionDegrees = 225f;
            characterController.ForwardInput = 1;
        }
        else if (vertical == -1 && horizontal == 0)
        {
            characterController.DirectionDegrees = 180f;
            characterController.ForwardInput = 1;
        }
        else if (vertical == -1 && horizontal == 1)
        {
            characterController.DirectionDegrees = 135f;
            characterController.ForwardInput = 1;
        }
        else
        {
            characterController.ForwardInput = 0;
        }
    }
}
