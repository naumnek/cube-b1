using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCollision : MonoBehaviour
{
    public GameObject obj;
    public ControllerPlayer cp;

    void OnCollisionEnter2D(Collision2D collision) //активируется при столкновении обьекта и записывает его в collision
    {
        obj = collision.gameObject;
        cp.checkCollision(obj.tag);
        collision = null;
    }
}
