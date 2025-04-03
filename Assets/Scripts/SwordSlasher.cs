using UnityEngine;
using EzySlice;
using UnityEngine.XR;

public class SwordSlasher : MonoBehaviour
{
    [Header("Slicing Settings")]
    public Material slicedMaterial;
    public float minCutVelocity = 1.0f;
public enum Hand { Left, Right }
    [Header("Hand Assignment")]
    
    public Hand hand = Hand.Right;

    private InputDevice device;

    private void Start()
    {
        XRNode node = (hand == Hand.Left) ? XRNode.LeftHand : XRNode.RightHand;
        device = InputDevices.GetDeviceAtXRNode(node);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Fruit")) return;

        float velocity = hand == Hand.Left
            ? ControllerVelocity.LeftControllerVelocity.magnitude
            : ControllerVelocity.RightControllerVelocity.magnitude;

        if (velocity < minCutVelocity)
        {
            Debug.Log("Too slow to slice.");
            return;
        }
        if (other.CompareTag("Bomb"))
        {
            BombManager.Instance?.BombHit();
            Destroy(other.gameObject);
            TriggerHapticFeedback();
            return;
            
        }
        TriggerHapticFeedback();
        SliceFruit(other.gameObject);
    }

    private void SliceFruit(GameObject fruit)
    {
        Vector3 slicePos = transform.position;
        Vector3 sliceNormal = hand == Hand.Left
    ? ControllerVelocity.LeftControllerVelocity.normalized
    : ControllerVelocity.RightControllerVelocity.normalized;


        // Use fruit's material for cap surface but need to change
        Material capMat = fruit.GetComponent<MeshRenderer>().material;

        SlicedHull slicedObject = fruit.Slice(slicePos, sliceNormal, capMat);
        if (slicedObject == null)
        {
            Debug.Log("Slice failed.");
            return;
        }

        // Create sliced halves
        GameObject upperHull = slicedObject.CreateUpperHull(fruit, slicedMaterial);
        GameObject lowerHull = slicedObject.CreateLowerHull(fruit, slicedMaterial);

        ApplyHullPhysics(upperHull, sliceNormal);
        ApplyHullPhysics(lowerHull, -sliceNormal);

        Destroy(fruit);
        ScoreManager.Instance?.AddScore(1);
    }

    private void ApplyHullPhysics(GameObject obj, Vector3 forceDir)
    {
        obj.transform.position += obj.transform.up * 0.01f; // prevent overlap

        Rigidbody rb = obj.AddComponent<Rigidbody>();
        rb.mass = Random.Range(0.4f, 0.6f);

        MeshCollider collider = obj.AddComponent<MeshCollider>();
        collider.convex = true;

        rb.AddForce(forceDir.normalized * 3f, ForceMode.Impulse);
        rb.AddTorque(Random.onUnitSphere * 2f, ForceMode.Impulse);

        Destroy(obj, 5f);
    }

    private void TriggerHapticFeedback()
    {
        if (!device.isValid) return;

        if (device.TryGetHapticCapabilities(out HapticCapabilities capabilities) && capabilities.supportsImpulse)
        {
            device.SendHapticImpulse(0u, 0.6f, 0.1f); // channel, amplitude, duration
        }
    }
}
