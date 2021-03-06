using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV : MonoBehaviour
{
    public static int Elite_Appear_Score = 1000;

    // 메인 카메라를 스크린 크기와 동일하게 적용 >> 스케일 기준(1080 * 2340이면 9.32 * 20.19로 변환)
    // 픽셀 단위로 가져오려면 Screen 사용
    public static float MAIN_CAMERA_HEIGHT = Camera.main.orthographicSize * 2; // 메인 카메라 높이
    public static float MAIN_CAMERA_WIDTH = MAIN_CAMERA_HEIGHT * Camera.main.aspect; // 메인 카메라 너비

    // prefab layer 수동 변경할 때 사용
    public static int ENEMY_LAYER = 7;

    // 패턴 관련 변수
    public enum Normal_Patterns // 잡몹 이동 패턴, 추가시 사이즈 변경
    {
        straight,
        diagonal,
        wave,
        rush
    }
    public const int Normal_Patterns_Size = 4;
    public enum Boss_Patterns // 보스 공격 패턴
    {
    }
    public const int Boss_Patterns_Size = 0;

    // S_line, G_line, E_line 관련 변수
    public static int WallCnt = 4;
    public static int NORTH_WALL = 1;
    public static int EAST_WALL = 2;
    public static int WEST_WALL = 3;

    // x변화량, y변화량
    public static float GetDegree(float variance_x, float variance_y) {
        return Mathf.Atan(variance_y / variance_x) * Mathf.Rad2Deg;
    }
    public static float GetDegree(Vector2 vector)
    {
        return Mathf.Atan(vector.y / vector.x) * Mathf.Rad2Deg;
    }
    public static float GetDegree(Vector2 start, Vector2 end)
    {
        Vector2 v2 = end - start;
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }
 /*   public static float GetDegree(Vector2 from, Vector2 to) // from부터 to까지 회전(총알이 토네이도마냥 돌아가면서 날아감)
    {
        return Quaternion.FromToRotation(Vector2.up, to - from).eulerAngles.z;
    }*/
    public static Vector2 GetVector2(float degree)
    {
        return new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad));
    }
}
