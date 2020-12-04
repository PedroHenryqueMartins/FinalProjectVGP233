using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDrawer : MonoBehaviour
{
    #region VARIABLES
    #region ENUM GIZMO SHAPE
    public enum GizmoShape
    {
        WireSphere = 0,
        Sphere = 1,
        Cube,
        WireCube
    }
    #endregion

    #region GIZMO COLOUR
    public enum GizmoColour
    {
        Blue,
        Cyan,
        Green,
        Magenta,
        Red,
        White,
        Yellow
    }
    #endregion

    public bool showGizmo;
    public GizmoShape gShape;
    public GizmoColour gColour;
    public float radius;
    public Vector3 scale;
    #endregion

    #region ON DRAW GIZMOS
    private void OnDrawGizmos()
    {
        if(!showGizmo)
        {
            return;
        }

        #region SWITCH COLOUR
        switch(gColour)
        {
            case GizmoColour.Blue:
                Gizmos.color = Color.blue;
                break;

            case GizmoColour.Cyan:
                Gizmos.color = Color.cyan;
                break;

            case GizmoColour.Green:
                Gizmos.color = Color.green;
                break;

            case GizmoColour.Magenta:
                Gizmos.color = Color.magenta;
                break;

            case GizmoColour.Red:
                Gizmos.color = Color.red;
                break;

            case GizmoColour.White:
                Gizmos.color = Color.white;
                break;

            case GizmoColour.Yellow:
                Gizmos.color = Color.yellow;
                break;

            default:
                Gizmos.color = Color.white;
                break;
        }
        #endregion

        #region SWITCH SHAPE
        switch (gShape)
        {
            case GizmoShape.WireSphere:
                Gizmos.DrawWireSphere(transform.localPosition, radius);
                break;

            case GizmoShape.Sphere:
                Gizmos.DrawSphere(transform.localPosition, radius);
                break;

            case GizmoShape.Cube:
                Gizmos.DrawCube(transform.localPosition, scale);
                break;

            case GizmoShape.WireCube:
                Gizmos.DrawWireCube(transform.localPosition, scale);
                break;

            default:
                break;
        }
        #endregion

    }
    #endregion

}
