using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronicsMaster : MonoBehaviour {
    public RoomIn[]  rooms_in;
    public RoomOut[] rooms_out;
    public ElectronicsPortal portal;

    private const int ROOMS_IN_NUMBER = 8;
    private const int ROOMS_OUT_NUMBER = 5;

    private int state_in = 0;
    private int state_out = 0;

    public Material on;
    public Material off;

    public GameObject[] mid_cables;
    public GameObject Door;

    public void ReceiveSignal(int signal)
    {
        if (signal == 0)
        {
            rooms_in[state_in].transform.Translate(new Vector3(0,0,100));
            state_in = (state_in + 1) % ROOMS_IN_NUMBER;
            rooms_in[state_in].transform.Translate(new Vector3(0, 0, -100));
        }
        else if (signal == 1)
        {
            rooms_out[state_out].transform.Translate(new Vector3(0, 0, -100));
            state_out = (state_out + 1) % ROOMS_OUT_NUMBER;
            rooms_out[state_out].transform.Translate(new Vector3(0, 0, 100));
        }
        Activate();
    }

    private void Start()
    {
        Activate();
    }

    void Activate () {
        rooms_in[state_in].Activate();

        for (int i = 0; i < 4; ++i)
        {
            mid_cables[i].GetComponent<MeshRenderer>().material = rooms_in[state_in].state_out[i] ? on : off;
        }

        rooms_out[state_out].state_in[0] = rooms_in[state_in].state_out[1];
        rooms_out[state_out].state_in[1] = rooms_in[state_in].state_out[0];
        rooms_out[state_out].state_in[2] = rooms_in[state_in].state_out[3];
        rooms_out[state_out].state_in[3] = rooms_in[state_in].state_out[2];

        rooms_out[state_out].Activate();

        portal.SetState(rooms_out[state_out].state_out[0] && rooms_out[state_out].state_out[1] && rooms_out[state_out].state_out[2] && rooms_out[state_out].state_out[3]);
    }
}
