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
            Debug.Log("Game object has null reference");
        }
    }

    public void DeactivateParticle(GameObject obj)
    {
        obj.SetActive(inactive);
    }

    public void ActivateParticle(GameObject obj)
    {
        obj.SetActive(active);
    }
}
