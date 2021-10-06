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
        Target = Camera.main.GetComponent<Ingame_manager>().GetPlayer(); // 플레이어 객체 받아옴
    }
    void Update()
    {
        dir = Target.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward); 
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, rotatespeed * Time.deltaTime);
        transform.rotation = rotation; // 대상바라보기 인터넷에서 퍼옴
    }
}

//총구가 총 3개임 앞에 2개 뒤에 1개
//2개를 사용할때는 기본 총알에 rortate스피드를 빠르게 줘서 발사하고 
//1개를 사용할때는 레이져에 rotate스피드를 느리게 줘서 두가지 패턴을 하나의 이미지로 떨쳐먹을수도