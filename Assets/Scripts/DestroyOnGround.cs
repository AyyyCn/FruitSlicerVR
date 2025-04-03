using UnityEngine;

public class DestroyOnGround : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object collided with: " + other.gameObject.name);
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
