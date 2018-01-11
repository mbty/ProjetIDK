using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This whole class is ugly, may be rewritten somewhere in the future (eval_rooms...)

public class RoomOut : MonoBehaviour {
    public int id;

    public bool[] state_in;
    public bool[] state_out;

    public int color_big_if;
    public int color_small_if;

    public GameObject[] cA_in;
    public GameObject[] cB_in;
    public GameObject[] cC_in;
    public GameObject[] cD_in;

    public GameObject[] cA_out;
    public GameObject[] cB_out;
    public GameObject[] cC_out;
    public GameObject[] cD_out;

    public GameObject[] big_text;
    public GameObject[] small_text;

    delegate void delegates();
    delegates[] evaluators;

    public RoomOut()
    {
        evaluators = new delegates[] { eval_room_1, eval_room_2, eval_room_3, eval_room_4, eval_room_5 };
    }

    public Material on;
    public Material off_text;
    public Material off;

    public void Activate()
    {
        UpdateCables(cA_in, state_in[0] == true ? on : off);
        UpdateCables(cB_in, state_in[1] == true ? on : off);
        UpdateCables(cC_in, state_in[2] == true ? on : off);
        UpdateCables(cD_in, state_in[3] == true ? on : off);
        evaluators[id]();
        UpdateCables(cA_out, state_out[0] == true ? on : off);
        UpdateCables(cB_out, state_out[1] == true ? on : off);
        UpdateCables(cC_out, state_out[2] == true ? on : off);
        UpdateCables(cD_out, state_out[3] == true ? on : off);

        UpdateCables(big_text, state_out[color_big_if] == true ? on : off_text);
        UpdateCables(small_text, state_out[color_small_if] == false ? on : off_text);
    }

    private void UpdateCables(GameObject[] arr_cables, Material mt)
    {
        foreach (GameObject g in arr_cables)
            g.GetComponent<MeshRenderer>().material = mt;
    }

    private void eval_room_1()
    {
        state_out[0] = (state_in[0] && !state_in[3]) || (!state_in[0] && state_in[3]);
        state_out[1] = state_in[2];
        state_out[2] = !state_in[1];
        state_out[3] = state_out[0];
    }

    private void eval_room_2()
    {
        state_out[0] = (state_in[0] && state_in[2]);
        state_out[1] = state_in[3];
        state_out[2] = state_out[0];
        state_out[3] = !state_in[1];
    }

    private void eval_room_3()
    {
        state_out[0] = state_in[0];
        state_out[1] = !state_in[3];
        state_out[2] = state_out[0];
        state_out[3] = (state_in[1] && !state_in[2]) || (!state_in[1] && state_in[2 ]);
    }

    private void eval_room_4()
    {
        state_out[0] = !state_in[0];
        state_out[1] = (state_in[1] && state_in[2]);
        state_out[2] = state_out[0];
        state_out[3] = state_out[1];
    }

    private void eval_room_5()
    {
        state_out[0] = !state_in[2];
        state_out[1] = state_in[3];
        state_out[2] = state_in[3];
        state_out[3] = (state_in[0] && !state_in[1]) || (!state_in[0] && state_in[1]);
    }
}
