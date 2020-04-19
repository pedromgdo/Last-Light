using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float maxHP = 100f;
    private float currentHP;
    [SerializeField] private float currencyReward = 10f;
    public int damage = 50;


    private PlayerController player;

    private void Awake() {
        var rot = transform.rotation;
        if (transform.position.x > 0) rot.y = -180;
        transform.rotation = rot;
        currentHP = maxHP;
        player = FindObjectOfType<PlayerController>();
        if (!player) Debug.LogError("Error: Enemy could not find player.");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
            case "Projectile":
                var projectile = collision.GetComponent<Projectile>();
                if (projectile) TakeDamage(projectile.projectileDamage*player.damageMult);
                break;
            default:
                break;
        }
    }

    private void TakeDamage(float damage) {
        currentHP -= damage;
        if (currentHP <= 0) Die();
    }

    public void Die(bool giveCurrency = true) {
        if(giveCurrency) LevelController.instance.PlayerKilledEnemy((int)(currencyReward * player.currencyMult));
        Destroy(gameObject);

    }

}
