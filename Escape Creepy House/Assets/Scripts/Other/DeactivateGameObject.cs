using UnityEngine;

public class DeactivateGameObject : MonoBehaviour
{
    public static DeactivateGameObject deactivateInstance;
    private bool inactive = false;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject[] objects1;
    [SerializeField] private GameObject[] objects2;
    [SerializeField] private GameObject[] objects3;
    [SerializeField] private GameObject[] objects4;
    [SerializeField] private GameObject[] objects5;
    [SerializeField] private GameObject[] objects6;
    [SerializeField] private GameObject[] objects7;
    [SerializeField] private GameObject[] objects8;
    [SerializeField] private GameObject[] objects9;
    [SerializeField] private GameObject[] objects10;
    [SerializeField] private GameObject[] objects11;

    private void Awake()
    {
        if (deactivateInstance == null)
        {
            deactivateInstance = this;
        }
        else
        {
            Debug.Log("The instance of the object already exists");
        }
    }

    public void DeactivateObject(GameObject obj)
    {
        obj.SetActive(inactive);
    }

    public void DeactivateObjectsInControls()
    {
        foreach (GameObject obj in objects1)
        {
            obj.SetActive(inactive);
        }
    }

    public void DeactivateObjectsInSettings()
    {
        foreach (GameObject obj in objects2)
        {
            obj.SetActive(inactive);
        }
    }

    public void DeactivateObjectsInBackToMenu()
    {
        foreach (GameObject obj in objects3)
        {
            obj.SetActive(inactive);
        }
    }

    public void DeactivateObjectsInBackToPrevious()
    {
        foreach (GameObject obj in objects4)
        {
            obj.SetActive(inactive);
        }
    }

    public void DeactivateObjectsInCategoryButtons()
    {
        foreach (GameObject obj in objects5)
        {
            obj.SetActive(inactive);
        }
    }

    public void DeactivateObjectsInGameSettings()
    {
        foreach (GameObject obj in objects6)
        {
            obj.SetActive(inactive);
        }
    }
    
    public void DeactivateObjectsInHome()
    {
        foreach (GameObject obj in objects7)
        {
            obj.SetActive(inactive);
        }
    }

    public void DeactivateObjectsInEndGameIntro()
    {
        foreach (GameObject obj in objects8)
        {
            obj.SetActive(inactive);
        }
    }

    public void DeactivateObjectsInPause()
    {
        foreach (GameObject obj in objects9)
        {
            obj.SetActive(inactive);
        }
    }

    public void DeactivateItems()
    {
        foreach (GameObject obj in objects10)
        {
            obj.SetActive(inactive);
        }
    }

    public void DeactivateDisplayUI()
    {
        foreach (GameObject obj in objects11)
        {
            obj.SetActive(inactive);
        }
    }
}
