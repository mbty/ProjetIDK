using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiScreen : MonoBehaviour {
    private List<string> order = new List<string>(new string[] {"blue", "red", "blue", "white", "green", "red", "white", "green"});
    private int current = 0;
    private int max = 0;

    public PlayerMovementMultiScreen red;
    public PlayerMovementMultiScreen green;
    public PlayerMovementMultiScreen blue;
    public PlayerMovementMultiScreen white;

    public List<SlidingBlockMultiScreen> objects1;
    public List<SlidingBlockMultiScreen> objects2;
    public List<SlidingBlockMultiScreen> objects3;
    public List<SlidingBlockMultiScreen> objects4;

    // TODO events

    public void receiveSignal(string id)
    {
        if (current != order.Count)
        {
            red.transform.localPosition = new Vector3(0, 0, 0);
            green.transform.localPosition = new Vector3(0, 0, 0);
            blue.transform.localPosition = new Vector3(0, 0, 0);
            white.transform.localPosition = new Vector3(0, 0, 0);

            if (id == order[current])
            {
                objects1[current].off();
                objects2[current].off();
                objects3[current].off();
                objects4[current].off();
                ++current;
                if (current > max)
                {
                    max = current;
                    switch (max)
                    {
                        case 2:
                            red.ChangeMult(1, 1);
                            green.ChangeMult(-1, 1);
                            blue.ChangeMult(-1, -1);
                            white.ChangeMult(-1, -1);
                            break;
                        case 4:
                            green.transform.Rotate(90 * Vector3.up);
                            blue.transform.Rotate(180 * Vector3.up);
                            white.transform.Rotate(270 * Vector3.up);
                            break;
                        case 6:
                            Rect temp = red.cm.rect;
                            red.cm.rect = white.cm.rect;
                            white.cm.rect = temp;
                            temp = green.cm.rect;
                            green.cm.rect = blue.cm.rect;
                            blue.cm.rect = temp;
                            break;
                        case 8:
                            red.ChangeMult(-1, -1);
                            green.ChangeMult(-1, -1);
                            blue.ChangeMult(-1, -1);
                            white.ChangeMult(-1, -1);
                            break;
                    }
                }
            }
            else if (current != 0)
            {
                objects1[current - 1].on();
                objects2[current - 1].on();
                objects3[current - 1].on();
                objects4[current - 1].on();
                --current;
            }
        }
    }
}
