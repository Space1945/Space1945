using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_Patterns : MonoBehaviour
{
    Vector2 start_point;
    Vector2 end_point;
    Vector2 player_point; // 군집 내에서는 모두 동일한 player_point로 이동
    int area; // 1이 왼쪽벽 -1이 오른쪽벽
    int speed;
    float mob_position;
    float velocity_x;
    float velocity_y;
    float y_dir; // diagonal 패턴에 사용
    float z_angle; // 회전 각도
    bool check_set = false; // true이면 Update() 동작
    
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
    public void Diagonal() // 대각선 이동
    {
        if (area == 0) // 위쪽에서 생성
        {
            // 화면 중앙에서 멀어질수록 기울기 절댓값 감소, 속도 감소
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
        if (area == 0) // 위쪽에서 생성
        {
            velocity_x = Mathf.Sin(this.gameObject.GetComponent<Rigidbody2D>().position.y) * 2;
            velocity_y = -speed;
//            z_angle = GV.radian_to_degree(-GV.GetRadian(velocity_x, velocity_y));
        }
        else
        {
            velocity_x = speed * area;
            velocity_y = Mathf.Sin(this.gameObject.GetComponent<Rigidbody2D>().position.x) * 2;

/*            if (area > 0) // 왼쪽에서 나오는 경우
                z_angle = GV.radian_to_degree(GV.GetRadian(velocity_y, velocity_x)) + 90f;
            else
                z_angle = 270f - GV.radian_to_degree(GV.GetRadian(velocity_y, -velocity_x));*/
        }
    }
    public void Rush() // 플레이어에게 돌진
    {
        float dx = player_point.x - start_point.x;
        float dy = player_point.y - start_point.y;
        float scalar = Mathf.Sqrt(dx * dx + dy * dy);

        velocity_x = dx / scalar * speed;
        velocity_y = dy / scalar * speed;

 /*       if (velocity_x == 0) // 일직선으로 내려올 때
            z_angle = 0;
        else
            z_angle = GV.radian_to_degree(GV.GetRadian(velocity_x, velocity_y));*/
    }

    private void FixedUpdate()
    {
        if (check_set)
        {
            // 진행 방향에 맞춰서 z_angle만큼 이미지 각도를 돌림
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
