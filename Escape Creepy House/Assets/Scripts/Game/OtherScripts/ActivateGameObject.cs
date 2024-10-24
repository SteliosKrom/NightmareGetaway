using UnityEngine;

public class ActivateGameObject : MonoBehaviour
{
    public static ActivateGameObject activateInstance;
    private bool active = true;

    private void Start()
    {
        if (activateInstance == null)
        {
            activateInstance = this;
        }
        else
        {
            Debug.Log("There is no instance of activate game object class");
        }
    }

    public void ActivateObject(GameObject obj)
    {
        obj.SetActive(active);
    }
}
