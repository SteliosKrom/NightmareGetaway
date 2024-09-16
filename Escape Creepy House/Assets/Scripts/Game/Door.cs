using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool isOpen = false;
    public float openAngle = 90f;
    public float doorSpeed = 2f;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation;
    }

    private void Update()
    {
        SetTheDoorRotation();
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
    }

    public void SetTheDoorRotation()
    {
        Quaternion targetRotation;

        if (isOpen)
        {
            targetRotation = openRotation;
        }
        else
        {
            targetRotation = closedRotation;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * doorSpeed);
    }
}
