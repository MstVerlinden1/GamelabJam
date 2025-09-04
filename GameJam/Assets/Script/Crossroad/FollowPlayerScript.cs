using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    [SerializeField, Tooltip("Player transform")]
    private Transform playerTransform;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);    
    }
}
