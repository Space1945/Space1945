using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fix : MonoBehaviour
{
    Vector2 end_point;
    bool stopped = false;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        end_point = new Vector2(Random.Range(-(GV.MAIN_CAMERA_WIDTH / 2), GV.MAIN_CAMERA_WIDTH / 2), Random.Range(0, GV.MAIN_CAMERA_HEIGHT / 2));
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Rigidbody2D>().position == end_point)
            stopped = true;
        else
            gameObject.GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(gameObject.GetComponent<Rigidbody2D>().position, end_point, speed * Time.deltaTime);
    }

    public bool Stopped()
    {
        return stopped;
    }
}
