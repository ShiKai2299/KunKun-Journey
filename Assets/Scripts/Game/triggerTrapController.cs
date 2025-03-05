using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerTrapController : MonoBehaviour
{
    public Rigidbody2D trap;
    public float speed;
    //AudioSource 
    AudioSource[] audioSources;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            Vector2 v = new Vector2(speed, 0);
            trap.velocity = v;
            audioSources = GetComponents<AudioSource>();
            audioSources[0].Play();
        }
    }
}
