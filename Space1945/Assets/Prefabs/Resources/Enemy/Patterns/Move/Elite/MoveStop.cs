using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStop : MonoBehaviour
{
    Vector2 end_point;
    int random_move_timer = 0;
    bool stopped;

    public float speed;
    public int move_period; // 몹이 움직이는 주기(프레임 단위)

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
            stopped = true;
            if (random_move_timer <= move_period)
                random_move_timer++;
            else
            {
                end_point = new Vector2(Random.Range(-(GV.MAIN_CAMERA_WIDTH / 2), GV.MAIN_CAMERA_WIDTH / 2), Random.Range(0, GV.MAIN_CAMERA_HEIGHT / 2));
                random_move_timer = 0;
            }
        }
        else
        {
            stopped = false;
            gameObject.GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(gameObject.GetComponent<Rigidbody2D>().position, end_point, speed * Time.deltaTime); // Elite는 한 지점에서 다른 지점으로 계속 이동함
        }
    }

    public bool Stopped()
    {
        return stopped;
    }
}
