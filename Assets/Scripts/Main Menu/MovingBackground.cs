using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{

    [SerializeField] GameObject background1;
    [SerializeField] GameObject background2;
    [SerializeField] float speed = 10;

    public bool moving = true;


    private Vector3 teleport_position;

    void Start()
    {
        moving = true;
        teleport_position = background2.transform.position;
    }

    void Update()
    {
        if (moving)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            if (background1.transform.position.x <= -16.4)
            {
                background1.transform.position = teleport_position;
            }

            if (background2.transform.position.x <= -16.4)
            {
                background2.transform.position = teleport_position;
            }


        }
    }
}
