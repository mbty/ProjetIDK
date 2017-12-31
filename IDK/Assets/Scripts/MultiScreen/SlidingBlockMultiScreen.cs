using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingBlockMultiScreen : MonoBehaviour {
    private const int displacement = 6;
    public int mult;

    public void off()
    {
        this.gameObject.transform.Translate(new Vector3(0, mult * displacement, 0));
    }

    public void on()
    {
        this.gameObject.transform.Translate(new Vector3(0, mult * (-displacement), 0));
    }
}
