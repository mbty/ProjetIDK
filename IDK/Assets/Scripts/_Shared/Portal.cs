using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    public GameObject player;
    public string level;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Application.LoadLevel(level);
        }
    }
}
