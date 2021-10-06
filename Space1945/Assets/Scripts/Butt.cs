using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butt : MonoBehaviour
{
    GameObject Target;
    Vector2 dir;
    float rotatespeed = 10;

    private void Start()
    {
        Target = Camera.main.GetComponent<Ingame_manager>().GetPlayer(); // �÷��̾� ��ü �޾ƿ�
    }
    void Update()
    {
        dir = Target.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward); 
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotatespeed * Time.deltaTime);
        transform.rotation = rotation; // ���ٶ󺸱� ���ͳݿ��� �ۿ�
    }
}

//�ѱ��� �� 3���� �տ� 2�� �ڿ� 1��
//2���� ����Ҷ��� �⺻ �Ѿ˿� rortate���ǵ带 ������ �༭ �߻��ϰ� 
//1���� ����Ҷ��� �������� rotate���ǵ带 ������ �༭ �ΰ��� ������ �ϳ��� �̹����� ���ĸ�������