using UnityEngine;

public class DeactivateGameObject : MonoBehaviour
{
    public static DeactivateGameObject instance;
    private bool inactive = false;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("There is no instance of deactivate game object class");
        }
    }

    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(inactive);
    }
}
