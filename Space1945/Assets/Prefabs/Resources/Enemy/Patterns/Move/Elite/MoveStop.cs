using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStop : MonoBehaviour
{
    Vector2 end_point;
    int random_move_timer = 0;
    bool stopped;

    public float speed;
    public int move_period; // ���� �����̴� �ֱ�(������ ����)

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
            gameObject.GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(gameObject.GetComponent<Rigidbody2D>().position, end_point, speed * Time.deltaTime); // Elite�� �� �������� �ٸ� �������� ��� �̵���
        }
    }

    public bool Stopped()
    {
        return stopped;
    }
}
