using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public bool start = false;
    public bool run = true;
    public bool jump = false;
    public GameObject player;
    public float forcejump = 1600.0f;
    public float speed = 80.0f;
    public float max_speed = 150f;
    public float speedup = 1f;
    public float min_speedup = 0.01f;
    public float slowing = 0.001f;
    private Rigidbody2D rbPlayer;
    private bool sStart = true;

    public void speed_up()
    {
        if(speed < max_speed) speed += speedup;
        if(speedup > min_speedup) speedup -= slowing;
    }

    // Update is called once per frame
    void Update()
    {
        if(start == true)
        {
            if(sStart == true)
            {
                sStart = false;
                mainStart();
            }
            if (run == true)
            {
                rbPlayer.AddForce(player.transform.right * speed, ForceMode2D.Impulse);
            }
        }
		if (Input.GetKeyDown(KeyCode.Space))
        {
             actionJump();
        }
		if (Input.GetKeyDown(KeyCode.Escape))
        {
             SceneManager.LoadScene("MainMenu");
        }
    }

    public void Jump()
    {
        actionJump();
    }
	
	private void actionJump()
	{
        if(jump == true && run == true)
        {
            rbPlayer.AddForce(player.transform.up * forcejump, ForceMode2D.Impulse);
            //rbPlayer.AddForce(-player.transform.right * speedrun, ForceMode2D.Impulse);
            //player.transform.position = new Vector2(x, y + speedjump * Time.deltaTime);
            jump = false;
        }
        run = true;		
	}

    public void checkCollision(string tag)
    {
        if(tag == "Enemy")
        {
            run = false;
        }
        else
        {
            jump = true;
        }
    }

    public void mainStart()
    {
        rbPlayer = player.gameObject.GetComponent<Rigidbody2D>();
    }
}
