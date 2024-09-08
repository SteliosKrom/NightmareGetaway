using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void OnInteract()
    {
        Collect();
    }

    public void Collect()
    {
        Debug.Log(gameObject.name + "has been collected");
        gameObject.SetActive(false);
    }
}
