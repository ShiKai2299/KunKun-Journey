using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class triggerExplosionController : MonoBehaviour
{
    public GameObject explosion;
    public Tilemap tilemap;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Vector3 hitPosition = transform.position;

            GameObject e = Instantiate(explosion, hitPosition, Quaternion.identity);


            if (tilemap != null)
            {
                tilemap.gameObject.SetActive(false);


            }

            gameObject.SetActive(false);
            Destroy(gameObject, 0.1f);
            Destroy(e, 0.6f);

        }
    }
}
