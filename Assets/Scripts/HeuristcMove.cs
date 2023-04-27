using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeuristcMove : MonoBehaviour
{
    private CharacterCtrl characterController;
    public GameObject interactionObj;
    private Interact intr;
    // Start is called before the first frame update
    void Start()
    {
        intr = interactionObj.GetComponent<Interact>();
        characterController = GetComponent<CharacterCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        characterController.vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        characterController.horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));

        if (Input.GetKeyDown("space"))
        {
            intr.tryInteract();
        }
    }
}
