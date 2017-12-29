using UnityEngine;

public class ActionSlab : MonoBehaviour {
    private int triggerCount = 0;
    public Switch src;
    public Teleporter srcT;
    public Switch dest;
    public Teleporter destT;


    private void OnTriggerEnter(Collider c) {
        if (c.tag == "PhysicsCube")
            ++triggerCount;
        if (triggerCount == 1)
            toggleState();
	}

    private void OnTriggerExit(Collider c)
    {
        if (c.tag == "PhysicsCube")
            --triggerCount;
        if (triggerCount == 0)
            toggleState();
    }

    private void toggleState()
    {
        Debug.Log("toggle");

        Teleporter tempT = srcT.friend;
        srcT.friend = destT;
        destT = tempT;

        Switch temp = src.friend;
        src.friend = dest;
        dest = temp;
    }
}