using UnityEngine;

public class KidsRoomLight : MonoBehaviour
{
    private bool inactive = false;
    private bool active = true;

    [Header("OTHER")]
    [SerializeField] private Light kidsRoomLight;

    private void Start()
    {
        kidsRoomLight = GetComponent<Light>();
        kidsRoomLight.enabled = inactive;
    }
}
