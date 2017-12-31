using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateMultiScreen : MonoBehaviour {
    public MultiScreen master;
    public string signal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            master.receiveSignal(signal);
    }
}
