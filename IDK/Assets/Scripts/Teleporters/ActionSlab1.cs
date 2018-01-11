using UnityEngine;

public class ActionSlab1 : MonoBehaviour {
    private int triggerCount = 0;
    public GameObject friend;
    public Material alter;

    private void OnTriggerEnter(Collider c) {
        if (c.tag == "PhysicsCube")
        {
            ++triggerCount;
            if (triggerCount == 1)
                toggleState();
        }
	}

    private void OnTriggerExit(Collider c)
    {
        if (c.tag == "PhysicsCube")
        {
            --triggerCount;
            if (triggerCount == 0)
                toggleState();
        }
    }

    private void toggleState()
    {
        Renderer rd = friend.GetComponent<Renderer>();
        rd.material.color = new Color(rd.material.color.r, rd.material.color.g, rd.material.color.b, 1f - rd.material.color.a);
        Collider co = friend.GetComponent<Collider>();
        co.enabled = !co.enabled;
            
        Material mt = this.GetComponent<MeshRenderer>().material;
        this.GetComponent<MeshRenderer>().material = alter;
        alter = mt;
    }
}