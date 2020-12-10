using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    #region CLASS PATH
    [Serializable]
    public class Path
    {
        public int Id;
        public List<Transform> Waypoints;
    }
    #endregion

    #region VARIABLES
    public List<Path> Paths;
    #endregion

    #region GET PATH
    public Path GetPath(int id)
    {
        return Paths[id];
    }
    #endregion
}
