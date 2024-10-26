using UnityEngine;

public class ActivateGameObject : MonoBehaviour
{
    private bool active = true;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject[] objects1;
    [SerializeField] private GameObject[] objects2;
    [SerializeField] private GameObject[] objects3;

    public void ActivateObject(GameObject obj)
    {
        obj.SetActive(active);
    }

    public void ActivateObjectsInSettings()
    {
        foreach (GameObject obj in objects1)
        {
            obj.SetActive(active);
        }
    }

    public void ActivateObjectsInBackToPrevious()
    {
        foreach (GameObject obj in objects2)
        {
            obj.SetActive(active);
        }
    }

    public void ActivateObjectsInGameSettings()
    {
        foreach (GameObject obj in objects3)
        {
            obj.SetActive(active);
        }
    }
}
