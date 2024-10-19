using UnityEngine;

public class ActivateGameObject : MonoBehaviour
{
    public static ActivateGameObject instance;
    private bool active = true;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
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
