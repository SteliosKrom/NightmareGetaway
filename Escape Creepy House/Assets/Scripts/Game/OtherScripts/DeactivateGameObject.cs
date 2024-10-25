using UnityEngine;

public class DeactivateGameObject : MonoBehaviour
{
    private bool inactive = false;
    public GameObject[] objects;

    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(inactive);
    }

    public void DeactivateObjectsInControls()
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(inactive);
        }
    }
}
