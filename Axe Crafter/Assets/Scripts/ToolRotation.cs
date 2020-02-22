using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolRotation : MonoBehaviour
{
    float RotationSpeed = 200f;
    public void RotateForward() { transform.Rotate(Vector3.forward * (RotationSpeed * Time.deltaTime)); }
    public void RotateBack() { transform.Rotate(Vector3.back * (RotationSpeed * Time.deltaTime)); }
}
