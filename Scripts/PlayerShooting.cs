using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject projectilePrefab;
    public PlayerController player;
    public float shootSpeed = 0.5f;
    private float lastShot = 0f;
    private bool canShoot = true;

    public float projectileForce = 20f;
    // Start is called before the first frame update
    private void Awake() {
        player = GetComponent<PlayerController>();
    }
    private void Start() {
        lastShot = shootSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot) {
            lastShot += Time.deltaTime;
            if (Input.GetButton("Fire1")) {
                if (lastShot * player.firerateMult > shootSpeed) {
                    ShootProjectile();
                    lastShot = 0;
                }
            }
        }
    }

    private void ShootProjectile() {
        GameObject bullet = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D body = bullet.GetComponent<Rigidbody2D>();
        body.AddForce(shootPoint.up*projectileForce,ForceMode2D.Impulse);
        Destroy(bullet, 10f);
    }
}
