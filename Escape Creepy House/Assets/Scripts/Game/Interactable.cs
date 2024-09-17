using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void OnInteract()
    {
        Collect();
    }

    public void Collect()
    {
        gameObject.SetActive(false);
    }
}
