using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GV : MonoBehaviour
{
    public static int Elite_Appear_Score = 1000;

    // ���� ī�޶� ��ũ�� ũ��� �����ϰ� ���� >> ������ ����(1080 * 2340�̸� 9.32 * 20.19�� ��ȯ)
    // �ȼ� ������ ���������� Screen ���
    public static float MAIN_CAMERA_HEIGHT = Camera.main.orthographicSize * 2; // ���� ī�޶� ����
    public static float MAIN_CAMERA_WIDTH = MAIN_CAMERA_HEIGHT * Camera.main.aspect; // ���� ī�޶� �ʺ�

    // prefab layer ���� ������ �� ���
    public static int ENEMY_LAYER = 7;

    // ���� ���� ����
    public enum Normal_Patterns // ��� �̵� ����, �߰��� ������ ����
    {
        straight,
        diagonal,
        wave,
        rush
    }
    public const int Normal_Patterns_Size = 4;
    public enum Boss_Patterns // ���� ���� ����
    {
    }
    public const int Boss_Patterns_Size = 0;

    // S_line, G_line, E_line ���� ����
    public static int WallCnt = 4;
    public static int NORTH_WALL = 1;
    public static int EAST_WALL = 2;
    public static int WEST_WALL = 3;

    // x��ȭ��, y��ȭ��
    public static float GetDegree(float variance_x, float variance_y) {
        return Mathf.Atan(variance_y / variance_x) * Mathf.Rad2Deg;
    }
    public static float GetDegree(Vector2 vector)
    {
        return Mathf.Atan(vector.y / vector.x) * Mathf.Rad2Deg;
    }
}
