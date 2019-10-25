using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    Effect effect;
    // Start is called before the first frame update
    void Start()
    {
        effect = GetComponent<Effect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            effect.CreatePool(0, transform.position, transform.rotation, true, true);
        }
        else if(Input.GetKeyDown(KeyCode.X))
        {
            effect.Play(0);
        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            effect.Stop(0);
        }
    }
}
