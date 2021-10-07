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

    // 라디안을 각도로 변환
    public static float radian_to_degree(float radian) {
        return radian * 180f / Mathf.PI;
    }
    // x변화량, y변화량
    public static float GetRadian(float variance_x, float variance_y) {
        return Mathf.Atan(variance_y / variance_x);
    }
}
