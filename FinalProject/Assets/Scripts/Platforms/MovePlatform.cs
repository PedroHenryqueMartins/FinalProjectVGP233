using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed;
    public GameObject[] waypoints;
    int currentPoint = 0;
    float pointRadius = 1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(waypoints[currentPoint].transform.position, transform.position) < pointRadius)
        {
            currentPoint++;
            if (currentPoint >= waypoints.Length)
            {
                currentPoint = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentPoint].transform.position, speed * Time.deltaTime);
    }

    
}
