using UnityEngine;

public class DeactivateGameObject : MonoBehaviour
{
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
}
