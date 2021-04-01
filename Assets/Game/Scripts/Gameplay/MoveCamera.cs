using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public bool start = false;
    public GameObject obj;

    void Update()
    {
        if(start == true)
        {
            this.transform.position = new Vector3((float)(obj.transform.position.x + 1.5), this.transform.position.y, this.transform.position.z);
        }
    }
}
