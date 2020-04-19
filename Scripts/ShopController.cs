using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private PlayerController player;
    public FirePlace goal;
    public static ShopController instance;
    // Start is called before the first frame update

    private void Awake() {
        if (instance != null) {
            Destroy(this);
            return;
        }
        else instance = this;
    }
    private void Start() {
        player = FindObjectOfType<PlayerController>();
        if (player == null) Debug.LogError("Error: Player could not be found.");
    }

    public void UpgradePlayerDamage(float value) {
        if (LevelController.instance.removeCurrency(100))
            player.damageMult += value;
    }
    public void UpgradePlayerSpeed(float value) {
        if (LevelController.instance.removeCurrency(100)) {
            player.speedMult += value;
            player.firerateMult += value;
        }
    }
    public void UpgradePlayerSight(float value) {
        if (LevelController.instance.removeCurrency(100)) {
            player.sightMult += value;
            player.firerateMult += value;
            Camera.main.orthographicSize = Camera.main.orthographicSize + value;
        }
    }
    public void UpdateEnemyReward(float value) {
        if (LevelController.instance.removeCurrency(100))
            player.currencyMult += value;
    }
    public void UpgradeFireplace(int value) {
        if (LevelController.instance.removeCurrency(200))
            goal.GainHP(value);
    }
    public void PauseGame() {
        LevelController.instance.PauseGame();
    }
    public void ResumeGame() {
        LevelController.instance.UnpauseGame();
    }
    public void ContinueGame() {
        LevelController.instance.UnpauseGame();
        goal.continueGame = true;
    }

}
