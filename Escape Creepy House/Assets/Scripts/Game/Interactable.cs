using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Animator doorAnimator;

    private bool isOpen;
    private bool active = true;
    private bool inactive = false;

    public virtual void OnInteract()
    {
        Collect();
    }

    public void Collect()
    {
        gameObject.SetActive(false);
    }
}
