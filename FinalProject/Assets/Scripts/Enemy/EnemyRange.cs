using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            this.transform.parent.gameObject.SendMessage("Chase");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.transform.parent.gameObject.SendMessage("Patrol");
    }
}
