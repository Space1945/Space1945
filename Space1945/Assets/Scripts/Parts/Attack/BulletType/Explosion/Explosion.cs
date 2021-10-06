using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    //폭발형 탄환
    //충돌시 일정 범위 내의 적에게 데미지
    //해당 탄환은 탄두로 적에게 데미지를 입힐 수 없음 Mob info의 충돌보다 아래의 충돌이 더 빠르기 때문에
    //따라서 탄두 자체의 crash_damage로는 적에게 데미지를 입힐 수 없으므로 Boom클래스 내의 폭파데미지로 적에게 데미지를 입혀야함

    public float boom_range;
    public int boom_damage;
    public GameObject boom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            boom.GetComponent<Boom>().bullet = this.gameObject; // 총알 정보를 넘겨줌
            Instantiate(boom);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "end_line")
            Destroy(this.gameObject);
    }
}