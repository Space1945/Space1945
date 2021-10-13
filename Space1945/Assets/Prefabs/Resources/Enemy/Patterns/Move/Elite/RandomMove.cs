using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    Vector2 end_point;
    bool change_dir;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        end_point = new Vector2(Random.Range(-(GV.MAIN_CAMERA_WIDTH / 2), GV.MAIN_CAMERA_WIDTH / 2), Random.Range(0, GV.MAIN_CAMERA_HEIGHT / 2));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.GetComponent<Rigidbody2D>().position == end_point)
        {
            change_dir = true;
            end_point = new Vector2(Random.Range(-(GV.MAIN_CAMERA_WIDTH / 2), GV.MAIN_CAMERA_WIDTH / 2), Random.Range(0, GV.MAIN_CAMERA_HEIGHT / 2));
        }
        else
        {
            change_dir = false;
            gameObject.GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(gameObject.GetComponent<Rigidbody2D>().position, end_point, speed * Time.deltaTime);
        }
    }

    public bool ChangingDir()
    {
        return change_dir;
    }
}
