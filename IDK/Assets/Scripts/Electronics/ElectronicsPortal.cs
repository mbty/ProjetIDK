using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicsPortal : MonoBehaviour {
    private bool is_on = false;
    public GameObject player;
    public string level;
    public Material alter;

    public void SetState(bool new_state)
    {
        if (is_on != new_state)
        {
            Material mt = this.GetComponent<MeshRenderer>().material;
            this.GetComponent<MeshRenderer>().material = alter;
            alter = mt;

            is_on = new_state;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (is_on && other.gameObject == player)
        {
            Application.LoadLevel(level);
        }
    }
}
