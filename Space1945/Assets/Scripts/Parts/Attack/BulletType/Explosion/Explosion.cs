using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //������ źȯ
    //�浹�� ���� ���� ���� ������ ������
    //�ش� źȯ�� ź�η� ������ �������� ���� �� ���� Mob info�� �浹���� �Ʒ��� �浹�� �� ������ ������
    //���� ź�� ��ü�� crash_damage�δ� ������ �������� ���� �� �����Ƿ� BoomŬ���� ���� ���ĵ������� ������ �������� ��������

    public GameObject boom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            boom.transform.position = gameObject.transform.position;
            Instantiate(boom);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "end_line")
            Destroy(gameObject);
    }
}