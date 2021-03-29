using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Door : MonoBehaviour
{
    public GameObject canvas;
    public GameObject winnerScreen;

    public FirstPersonController fp;

    public 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDoor()
    {
        gameObject.SetActive(false);
        fp.GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Debug.Log("Player escaped!");
        canvas.SetActive(true);
        winnerScreen.SetActive(true);
    }
}
