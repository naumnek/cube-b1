﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ButtonStart()
    {
        SceneManager.LoadScene("Level1");
    }
	
	public void ButtonExit() //выход из игры
    {
        Application.Quit();
    }
}
