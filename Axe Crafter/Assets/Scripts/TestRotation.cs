using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    bool canAnimate = true;
    bool canRotate = true;
    float RotationSpeed = 20f;

    public void Update()
    {
        ToolAnimation();
    }

    private void ToolAnimation()
    {
        if (canAnimate)
        {
            if (canRotate)
            {
                transform.Rotate(Vector3.forward * (RotationSpeed * Time.deltaTime));
                if (transform.rotation.eulerAngles.z >= 45) { canRotate = false; }
            }
            else
            {
                transform.Rotate(Vector3.back * (RotationSpeed * Time.deltaTime));
                if (transform.rotation.eulerAngles.z <= 15) { canRotate = true; }
            }
        }
    }

    public void RotateForward() { transform.Rotate(Vector3.forward * (RotationSpeed * Time.deltaTime)); }
    public void RotateBack() { transform.Rotate(Vector3.back * (RotationSpeed * Time.deltaTime)); }
}
