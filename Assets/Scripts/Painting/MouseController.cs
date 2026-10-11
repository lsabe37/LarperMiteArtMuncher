using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    public bool mouse_held = false;


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {

            mouse_held = true;
        }
        else
        {
            mouse_held = false;
        }
    }

    
}
