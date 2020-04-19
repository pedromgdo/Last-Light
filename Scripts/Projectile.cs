using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileDamage = 10f;
    public GameObject particles;

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
            case "Enemy":
                var particle = Instantiate(particles,transform.position,Quaternion.identity);
                Destroy(particle, 2f);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
