using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    private Animator aniCamera;


    void Start()
    {
        aniCamera = GameObject.FindWithTag("MainCamera").GetComponent<Animator>();
    }

    public void goToShop()
    {
        aniCamera.SetTrigger("goToShop");
    }

    public void shopToMainMenu()
    {
        aniCamera.SetTrigger("shopToMainMenu");
    }

    public void goToRecord()
    {
        aniCamera.SetTrigger("goToRecord");
    }

    public void recordToMainMenu()
    {
        aniCamera.SetTrigger("recordToMainMenu");
    }


}
