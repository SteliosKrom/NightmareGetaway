using UnityEngine;

public class SpotlightRotate : MonoBehaviour
{
    [SerializeField] private GameObject goFollow;
    [SerializeField] private Vector3 vectorOffset;
    [SerializeField] private float speed = 1.0f;

    void Start()
    {
        vectorOffset = transform.position - goFollow.transform.position;
    }

    void Update()
    {
        transform.position = goFollow.transform.position + vectorOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);
    }
}
