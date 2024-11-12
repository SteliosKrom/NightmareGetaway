using UnityEngine;

public class ActivateGameObject : MonoBehaviour
{
    public static ActivateGameObject activateInstance;
    private bool active = true;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject[] objects1;
    [SerializeField] private GameObject[] objects2;
    [SerializeField] private GameObject[] objects3;

    private void Awake()
    {
        if (activateInstance == null)
        {
            activateInstance = this;
        }
        else
        {
            Debug.Log("The instance of the object already exists");
        }
    }

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
