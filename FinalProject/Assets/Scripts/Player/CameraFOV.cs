using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{

    public float sensitivity = 100.0f;
    public Transform parentTransform;

    float rotationInX = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        rotationInX -= mouseY;
        rotationInX = Mathf.Clamp(rotationInX, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(rotationInX, 0.0f, 0.0f);
        parentTransform.Rotate(Vector3.up * mouseX);
    }
}
