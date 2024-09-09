using UnityEngine;

public class FlashlightInteractable : Interactable
{
    public Interactor playerInteractor;

    public override void OnInteract()
    {
        base.OnInteract();
        CollectFlashlight();
    }

    private void CollectFlashlight()
    {
        if (playerInteractor == null)
        {
            Debug.LogError("Interactor not assigned to FlashlightInteractable!");
            return;
        }

        playerInteractor.hasFlashlight = true;
        Debug.Log("Flashlight has been collected!");
    }
}
