using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_Patterns : MonoBehaviour
{
    Vector2 start_point;
    Vector2 end_point;
    Vector2 player_point; // ���� �������� ��� ������ player_point�� �̵�
    int area; // 1�� ���ʺ� -1�� �����ʺ�
    int speed;
    float mob_position;
    float velocity_x;
    float velocity_y;
    float y_dir; // diagonal ���Ͽ� ���
    float z_angle; // ȸ�� ����
    bool check_set = false; // true�̸� Update() ����
    
    [SerializeField] [Range(0, GV.Normal_Patterns_Size - 1)] public int pattern = 0;

    public void SetPattern(int area, Vector2 start_point, Vector2 player_point, float mob_position, int speed)
    {
        this.area = (area % 2 - area / 2);
        this.start_point = start_point;
        this.end_point = start_point;
        this.player_point = player_point;
        this.mob_position = mob_position;
        this.speed = speed;
        this.y_dir = speed / mob_position;
        check_set = true;
    }

    public void Straight()
    {
        if (area == 0)
        {
            velocity_x = 0;
            velocity_y = -speed;
        }
        else
        {
            velocity_x = area * speed;
            velocity_y = 0;
            z_angle = area * 90;
        }
    }
    public void Diagonal() // �밢�� �̵�
    {
        if (area == 0) // ���ʿ��� ����
        {
            // ȭ�� �߾ӿ��� �־������� ���� ���� ����, �ӵ� ����
            velocity_x = -mob_position;
            velocity_y = y_dir < 0 ? y_dir : -y_dir;
        }
        else
        {
            velocity_x = (float)(area * speed) / 1.414f;
            velocity_y = (float)(-speed) / 1.414f;
        }
//        z_angle = GV.radian_to_degree(-GV.GetRadian(velocity_x, velocity_y));
    }
    public void Wave()
    {
        if (area == 0) // ���ʿ��� ����
        {
            velocity_x = Mathf.Sin(this.gameObject.GetComponent<Rigidbody2D>().position.y) * 2;
            velocity_y = -speed;
//            z_angle = GV.radian_to_degree(-GV.GetRadian(velocity_x, velocity_y));
        }
        else
        {
            velocity_x = speed * area;
            velocity_y = Mathf.Sin(this.gameObject.GetComponent<Rigidbody2D>().position.x) * 2;

/*            if (area > 0) // ���ʿ��� ������ ���
                z_angle = GV.radian_to_degree(GV.GetRadian(velocity_y, velocity_x)) + 90f;
            else
                z_angle = 270f - GV.radian_to_degree(GV.GetRadian(velocity_y, -velocity_x));*/
        }
    }
    public void Rush() // �÷��̾�� ����
    {
        float dx = player_point.x - start_point.x;
        float dy = player_point.y - start_point.y;
        float scalar = Mathf.Sqrt(dx * dx + dy * dy);

        velocity_x = dx / scalar * speed;
        velocity_y = dy / scalar * speed;

 /*       if (velocity_x == 0) // ���������� ������ ��
            z_angle = 0;
        else
            z_angle = GV.radian_to_degree(GV.GetRadian(velocity_x, velocity_y));*/
    }

    private void FixedUpdate()
    {
        if (check_set)
        {
            // ���� ���⿡ ���缭 z_angle��ŭ �̹��� ������ ����
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, z_angle));

            switch ((GV.Normal_Patterns)pattern)
            {
                case GV.Normal_Patterns.straight:
                    Straight();
                    break;
                case GV.Normal_Patterns.diagonal:
                    Diagonal();
                    break;
                case GV.Normal_Patterns.wave:
                    Wave();
                    break;
                case GV.Normal_Patterns.rush:
                    Rush();
                    break;
            }

            GetComponent<Rigidbody2D>().velocity = new Vector2(velocity_x, velocity_y);
        }
    }
}
