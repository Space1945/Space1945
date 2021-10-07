using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight : MonoBehaviour
{
    float velocity_x;
    float velocity_y;
    
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        string instantiate_point = GetComponent<Mob_info>().instantiate_point;

        if (instantiate_point == "LEFT")
        {
            velocity_x = -speed;
            velocity_y = 0;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        else if (instantiate_point == "RIGHT")
        {
            velocity_x = speed;
            velocity_y = 0;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        else // dir == "TOP"
        {
            velocity_x = 0;
            velocity_y = -speed;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(velocity_x, velocity_y);
    }
}
