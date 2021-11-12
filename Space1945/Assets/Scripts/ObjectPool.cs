using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    List<List<GameObject>> bulletenemy;

    Dictionary<GameObject, Queue<GameObject>> bullets_using = new Dictionary<GameObject, Queue<GameObject>>();
    Dictionary<GameObject, Queue<GameObject>> bullets_not_using = new Dictionary<GameObject, Queue<GameObject>>();

    Ingame_manager ims;
    public void Make_Pool()
    {
        ims = GetComponent<Ingame_manager>();
    }
    public GameObject GetBullet(GameObject bullet)
    {
        GameObject bullet_clone;
        if (!bullets_not_using.ContainsKey(bullet))
        {
            bullets_not_using.Add(bullet, new Queue<GameObject>());
            bullets_using.Add(bullet, new Queue<GameObject>());
        }
        if (bullets_not_using[bullet].Count != 0)
        {
            bullet_clone = bullets_not_using[bullet].Dequeue();
            bullets_using[bullet].Enqueue(bullet_clone);
            return bullet_clone;
        }

        bullet_clone = Instantiate(bullet, bullet.transform);
        bullets_using[bullet].Enqueue(bullet_clone);
        return bullet_clone;
    }
    public void ReturnBullet(GameObject bullet)
    {
        GameObject bullet_clone = bullets_using[bullet].Dequeue();
        bullets_not_using[bullet].Enqueue(bullet_clone);
        bullet_clone.SetActive(false);
    }
}
