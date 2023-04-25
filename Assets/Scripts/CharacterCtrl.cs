using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : MonoBehaviour
{
    [Tooltip("Move speed in meters/second")]
    public float moveSpeed = 5f;
    public float ForwardInput { get; set; }
    public int horizontal { get; set; }
    public int vertical { get; set; }
    private float DirectionDegrees { get; set; }
    new private Rigidbody rigidbody;
    private bool allowMovement = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (!allowMovement) { return; }
        // Process Actions
        ProcessMovement();

        // Do Actions
        transform.rotation = Quaternion.Euler(0, DirectionDegrees, 0);
        rigidbody.velocity = Vector3.zero;
        rigidbody.velocity += transform.forward * Mathf.Clamp(ForwardInput, -1f, 1f) * moveSpeed;
    }
    public void moveOn()
    {
        allowMovement = true;
    }
    public void moveOff()
    {
        allowMovement = false;
        rigidbody.velocity = Vector3.zero;
    }
    private void ProcessMovement()
    {
        switch (vertical)
        {
            case -1:
                if (horizontal == -1)
                {
                    DirectionDegrees = 225f;
                    ForwardInput = 1;
                }
                else if (horizontal == 0)
                {
                    DirectionDegrees = 180f;
                    ForwardInput = 1;
                }
                else
                {
                    DirectionDegrees = 135f;
                    ForwardInput = 1;
                }
                break;
            case 0:
                if (horizontal == -1)
                {
                    DirectionDegrees = 270f;
                    ForwardInput = 1;
                }
                else if (horizontal == 0)
                {
                    ForwardInput = 0;
                }
                else
                {
                    DirectionDegrees = 90f;
                    ForwardInput = 1;
                }
                break;
            case 1:
                if (horizontal == -1)
                {
                    DirectionDegrees = 315f;
                    ForwardInput = 1;
                }
                else if (horizontal == 0)
                {
                    DirectionDegrees = 0f;
                    ForwardInput = 1;
                }
                else
                {
                    DirectionDegrees = 45f;
                    ForwardInput = 1;
                }
                break;
        }
    }
}