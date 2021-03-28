using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    private List<Key> keys;

    private void Awake()
    {
        keys = new List<Key>();
    }

    public void AddKey(Key key)
    {
        Debug.Log("Picked up a key");
        keys.Add(key);
    }

    private void OnTriggerEnter(Collider collision)
    {
        Key key = collision.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key);
            Destroy(key.gameObject);
        }

        Door door = collision.GetComponent<Door>();
        if (door != null)
        {
            door.OpenDoor();
        }
    }
}
