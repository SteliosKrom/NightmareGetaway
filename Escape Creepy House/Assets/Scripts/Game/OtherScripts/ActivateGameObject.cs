using UnityEngine;

public class ActivateGameObject : MonoBehaviour
{
    private bool active = true;

    public void ActivateObject(GameObject obj)
    {
        obj.SetActive(active);
    }
}
