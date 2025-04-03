using UnityEngine;
using UnityEngine.XR;

public class ControllerVelocity : MonoBehaviour
{
    public static Vector3 LeftControllerVelocity { get; private set; }
    public static Vector3 RightControllerVelocity { get; private set; }

    private InputDevice leftDevice;
    private InputDevice rightDevice;

    void Start()
    {
        TryInitializeDevices();
    }

    void Update()
    {
        if (!leftDevice.isValid || !rightDevice.isValid)
        {
            TryInitializeDevices();
        }

        UpdateInput();
    }

    void TryInitializeDevices()
    {
        leftDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        rightDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void UpdateInput()
    {
        leftDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 leftVel);
        rightDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 rightVel);

        LeftControllerVelocity = leftVel;
        RightControllerVelocity = rightVel;
    }
}
