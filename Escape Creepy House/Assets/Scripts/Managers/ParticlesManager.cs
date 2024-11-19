using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [Header("SCRIPT REFERENCES")]
    public static ParticlesManager instance;

    [Header("TYPES")]
    private bool active = true;
    private bool inactive = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Instance of the object is null");
        }
    }
    public void ActivateObject(GameObject obj)
    {
        obj.SetActive(active);
    }

    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(inactive);
    }
}
