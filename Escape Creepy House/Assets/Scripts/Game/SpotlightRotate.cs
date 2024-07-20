using UnityEngine;

public class SpotlightRotate : MonoBehaviour
{
    [SerializeField] private GameObject goFollow;
    [SerializeField] private Vector3 vectorOffset;
    [SerializeField] private float speed;

    void Start()
    {
        vectorOffset = transform.position - goFollow.transform.position;
    }

    void LateUpdate()
    {
        transform.position = goFollow.transform.position + vectorOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);
    }
}
