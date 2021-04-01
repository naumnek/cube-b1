using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using System.Linq;

public class SpawnLogic : MonoBehaviour
{
    public bool start = false;
    public GameMenu menu;
    public Timer timer;
    public ControllerPlayer cPlayer;
    public MoveCamera mainCamera;
    public int score = 0;
    public Text scoreboard;
    public bool spawn = false;
    public int[] randomSpawnTree = new int[] { 5, 10}; //min_x max_x
    public int[] randomSpawnEnemy = new int[] { 12, 24, 6, 12 }; //min_x max_x min_y max_y 
    public GameObject[] trees = new GameObject[] { };
    public List<GameObject> grounds = new List<GameObject>();
    public List<GameObject> enemys = new List<GameObject>();
    public GameObject ground;
    public GameObject enemy;
    public GameObject player;
    public int viewRadius = 0;
    public int destroyRadius = 3;
    private System.Random ran = new System.Random();
    private GameObject emptyEnemys;

    void Start()
    {

        mainStart();
    }

    void Update()
    {
        if(start == true)
        {
            start = false;
            mainStart();
        }
        if(spawn == true)
        {
            countGround();
            countEnemy();
            posGround();
            posEnemy();
        }
        if(score == 1000)
        {
            scoreboard.text = "WIN!";
            menu.gameover();
        }
    }

    private void mainStart()
    {
        emptyEnemys = new GameObject("Enemys");
        spawnObject(ref enemys, player, emptyEnemys, 0, -3.6f);
        enemys.First().GetComponent<GetCollision>().cp = cPlayer;
        cPlayer.player = enemys.First();
        mainCamera.obj = enemys.First();
        mainCamera.start = true;
        timer.start = true;
        cPlayer.start = true;
        spawn = true;
    }

    private void posGround()
    {
        Transform lastGround = grounds.Last().transform.GetChild(0).transform;
        if (enemys.First().transform.position.x > lastGround.localScale.x / 2 + lastGround.position.x)
        {
            logicGround(2, grounds.First());
        }
    }

    private void countGround()
    {
        switch (grounds.Count)
        {
            case (0):
                logicGround(3, null);
                break;
            case (3):
                logicGround(1, grounds.First());
                break;
        }
    }

    private void logicGround(int id, GameObject target)
    {
        switch (id)
        {
            case (1):
                delObject(ref grounds, target);
                break;
            case (2):
                spawnObject(ref grounds, ground, "ground", target.transform.localScale.x + target.transform.position.x, target.transform.position.y);
                spawnTree(grounds.Last());
                break;
            case (3):
                spawnObject(ref grounds, ground, "ground", 0, -4f);
                spawnTree(grounds.Last());
                break;
        }
    }

    private void spawnTree(GameObject target)
    {
        int i = 0;
        Transform lastGround = target.transform.GetChild(0).transform;
        float tree_max = lastGround.localScale.x + lastGround.position.x;
        for (float tree_x = 0; tree_x < tree_max ; )
        {
            i += ran.Next(randomSpawnTree[0], randomSpawnTree[1]);
            GameObject obj = Instantiate(trees[ran.Next(0, trees.Length)], new Vector2(lastGround.transform.position.x + i, lastGround.transform.position.y), Quaternion.identity);
            obj.transform.SetParent(grounds.Last().transform, false);
            tree_x = obj.transform.position.x;
        }
    }

    private void posEnemy()
    {
        foreach (GameObject copy in enemys)
        {
            if (copy.gameObject.tag == "Enemy" && copy.transform.position.x < enemys.First().transform.position.x - destroyRadius )
            {
                logicEnemy(1, copy);
                score += 1;
                scoreboard.text = score.ToString();
                break;
            }
        }
    }

    private void countEnemy()
    {
        switch (enemys.Count)
        {
            case (1):
                logicEnemy(3, null);
                break;
            case (2):
                logicEnemy(2, enemys.Last());
                break;
            case (4):
                logicEnemy(1, enemys[1]);
                break;
        }
    }

    private void logicEnemy(int id, GameObject target)
    {        
        switch (id)
        {
            case (1):
                delObject(ref enemys, target);
                break;
            case (2):
                spawnObject(ref enemys, enemy, emptyEnemys, target.transform.position.x + ran.Next(randomSpawnEnemy[0], randomSpawnEnemy[1]), ran.Next(randomSpawnEnemy[2], randomSpawnEnemy[2]));
                break;
            case (3):
                spawnObject(ref enemys, enemy, emptyEnemys, ran.Next(randomSpawnEnemy[0], randomSpawnEnemy[1]), ran.Next(randomSpawnEnemy[2], randomSpawnEnemy[2]));
                break;
        }
    }

    private void spawnObject(ref List<GameObject> list, GameObject prefab, float x, float y)
    {
        list.Add(Instantiate(prefab, new Vector2(x, y), Quaternion.identity));
    }

    private void spawnObject(ref List<GameObject> list, GameObject prefab, GameObject parent, float x, float y)
    {
        GameObject obj = Instantiate(prefab, new Vector2(x, y), Quaternion.identity);
        obj.transform.SetParent(parent.transform, false);
        list.Add(obj);
    }

    private void spawnObject(ref List<GameObject> list, GameObject prefab, string name, float x, float y)
    {
        GameObject parent = new GameObject(name);
        GameObject obj = Instantiate(prefab, new Vector2(x, y), Quaternion.identity);
        obj.transform.SetParent(parent.transform, false);
        list.Add(parent);
    }

    private void delObject(ref List<GameObject> list, GameObject target)
    {
        list.Remove(target);
        Destroy(target);
    }
}
