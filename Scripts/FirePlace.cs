using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlace : MonoBehaviour
{
    public int maxHealth = 1000;
    public int startHealth = 100;
    public int currentHealth;
    public bool continueGame = false;

    public GameObject fire0;
    public GameObject fire1;
    public GameObject fire2;
    public GameObject fire3;
    public GameObject fire4;

    private void Start() {
        currentHealth = startHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
            case "Enemy":
                var controller = collision.gameObject.GetComponent<EnemyController>();
                LoseHP(controller.damage);
                controller.Die();
                break;
            default:
                break;
        }
    }
    public void LoseHP(int damage) {
        currentHealth -= damage;
        UpdateTexture();
        if (currentHealth <= 0) LevelController.instance.PlayerLost();
    }
    public void GainHP(int health) {
        currentHealth += health;
        UpdateTexture();
        if (currentHealth >= maxHealth && !continueGame) LevelController.instance.PlayerWon();
    }

    private void UpdateTexture() {
        if (currentHealth >= 800) {
            fire4.SetActive(true);
            fire3.SetActive(false);
            fire2.SetActive(false);
            fire1.SetActive(false);
            fire0.SetActive(false);
        }
        else if (currentHealth >= 500) {
            fire4.SetActive(false);
            fire3.SetActive(true);
            fire2.SetActive(false);
            fire1.SetActive(false);
            fire0.SetActive(false);
        }
        else if (currentHealth >= 300) {
            fire4.SetActive(false);
            fire3.SetActive(false);
            fire2.SetActive(true);
            fire1.SetActive(false);
            fire0.SetActive(false);
        }
        else if (currentHealth >= 100) {
            fire4.SetActive(false);
            fire3.SetActive(false);
            fire2.SetActive(false);
            fire1.SetActive(true);
            fire0.SetActive(false);
        }
        else {
            fire4.SetActive(false);
            fire3.SetActive(false);
            fire2.SetActive(false);
            fire1.SetActive(false);
            fire0.SetActive(true);
        }
    }
}
