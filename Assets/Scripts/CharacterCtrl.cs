using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : MonoBehaviour
{
    [Tooltip("Move speed in meters/second")]
    public float moveSpeed = 5f;
    public float ForwardInput { get; set; }
    public float DirectionDegrees { get; set; }
    new private Rigidbody rigidbody;
    private bool allowMovement = true;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (allowMovement)
        {
            // Process Turning
            transform.rotation = Quaternion.Euler(0, DirectionDegrees, 0);

            // Reset the velocity
            rigidbody.velocity = Vector3.zero;

            // Apply a forward or backward velocity based on player input
            rigidbody.velocity += transform.forward * Mathf.Clamp(ForwardInput, -1f, 1f) * moveSpeed;
        }
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
}