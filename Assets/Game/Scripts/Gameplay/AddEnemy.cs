using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemy : MonoBehaviour
{

    public SpawnLogic sl;
    // Start is called before the first frame update
    void Start()
    {
        sl.enemys.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
