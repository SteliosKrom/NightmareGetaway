using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [Header("SCRIPT REFERENCES")]
    public static ParticlesManager instance;

    [Header("TYPES")]
    private bool active = true;
    private bool inactive = false;

    public void ActivateObject(GameObject obj)
    {
        obj.SetActive(active);
    }

    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(inactive);
    }
}
