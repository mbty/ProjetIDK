using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicsPressurePlate : MonoBehaviour {
    public ElectronicsMaster master;
    public int signal;

    private void OnTriggerEnter(Collider other)
    {
        master.ReceiveSignal(signal);
    }
}
