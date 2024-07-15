using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 distance;

    private void Start()
    {
        distance = transform.position - player.transform.position;
    }

    private void Update()
    {
        transform.position = distance + player.transform.position;
    }
}
