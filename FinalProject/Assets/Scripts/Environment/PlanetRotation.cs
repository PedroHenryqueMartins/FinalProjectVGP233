using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    #region VARIABLES
    // Create three different rotations, just in case we decide to add more planets, as not all planets rotate the same
    public float rotationX = 0.0f;
    public float rotationY = 0.0f;
    public float rotationZ = 0.0f;
    #endregion

    #region UPDATE
    void Update()
    {
        // For planet Earth, we will rotate just in the Y axis
        transform.Rotate(rotationX, rotationY, rotationZ);
    }
    #endregion
}
