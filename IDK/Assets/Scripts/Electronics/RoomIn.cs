using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomIn : MonoBehaviour {
    public bool[] state_in;
    public bool[] state_out;

    public GameObject[] cA;
    public GameObject[] cB;
    public GameObject[] cC;
    public GameObject[] cD;

    public Material on;
    public Material off;

    public void Activate()
    {
        UpdateCables(cA, state_in[0] == true ? on : off);
        UpdateCables(cB, state_in[1] == true ? on : off);
        UpdateCables(cC, state_in[2] == true ? on : off);
        UpdateCables(cD, state_in[3] == true ? on : off);
    }

    private void UpdateCables(GameObject[] arr_cables, Material mt)
    {
        foreach (GameObject g in arr_cables)
            g.GetComponent<MeshRenderer>().material = mt;
    }
}
