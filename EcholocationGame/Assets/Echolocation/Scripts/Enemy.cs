using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Enemy : MonoBehaviour
{
    public GameObject canvas;
    public GameObject loserScreen;

    public FirstPersonController fp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            fp.GetComponent<FirstPersonController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Debug.Log("Player was caught by an enemy!");
            canvas.SetActive(true);
            loserScreen.SetActive(true);
        }
    }
}
