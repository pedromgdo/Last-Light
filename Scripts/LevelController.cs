using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    [Header("Level Settings")]
    public int initialSpawns = 100;
    public float spawnSpeed = 1f;
    public float spawnSpeedDecrease = 0.002f;
    private float lastSpawn = 0f;
    public float maxRange = 10f;
    public float minRange = 3f;
    [Header("Level Components")]
    public List<EnemyInfo> enemyInfos;
    private bool canSpawn = false;
    private bool hasLost = false;
    public TextMeshProUGUI timerText;
    private float timeSurvived = 0f;
    public TextMeshProUGUI currencyText;
    private int currency = 0;
    [Header("Other")]
    public GameObject shopScreen;
    public GameObject shopButton;
    public GameObject pauseScreen;
    public GameObject winScreen;
    public GameObject loseScreen;


    public void DefaultState() {
        shopButton.SetActive(true);
        shopScreen.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    private void Awake() {
        if (instance!=null) {
            Destroy(this);
            return;
        }
        else instance = this;

        timerText.text = "Time : 0";
        currencyText.text = " <size=120%>:</size> 0";
        canSpawn = true;
    }
    private void Start() {
        if(canSpawn) InitialEnemySpawn();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            if (pauseScreen.activeInHierarchy) {
                pauseScreen.SetActive(false);
                shopScreen.SetActive(false);
                shopButton.SetActive(true);
                UnpauseGame();
            }
            else {
                pauseScreen.SetActive(true);
                shopScreen.SetActive(false);
                shopButton.SetActive(false);
                PauseGame();
            }
        }
        if (canSpawn) {
            lastSpawn += Time.deltaTime;
            if (lastSpawn > spawnSpeed) {
                if (spawnSpeed >= 0.1) spawnSpeed -=spawnSpeedDecrease;
                lastSpawn = 0f;
                SpawnEnemy();
            }
        }
        if (!hasLost) {
            timeSurvived += Time.deltaTime;
            var ts = TimeSpan.FromSeconds(timeSurvived);
            timerText.text = "Time : " + string.Format("{0:00}:{1:00}.{2:000}", ts.Minutes, ts.Seconds, ts.Milliseconds);
        }
    }
    private void InitialEnemySpawn() {
        for(int i = 0; i < initialSpawns; i++) {
            SpawnEnemy();
        }
    }
    private void SpawnEnemy() {
        GameObject prefab = FindSpawnPrefab();
        if (!prefab) {
            Debug.LogError("Error: Could not spawn enemies as there were not prefabs attached to the Level controller.");
            return;
        }
        Vector2 spawnPosition = FindSpawnPosition();
        Instantiate(prefab,spawnPosition,Quaternion.identity);
    }
    private Vector2 FindSpawnPosition() {
        var v = UnityEngine.Random.insideUnitCircle;
        Vector2 spawnPosition = v.normalized * minRange + v * (maxRange - minRange);
        float percentPos = Mathf.Floor(Mathf.Abs(UnityEngine.Random.Range(0f, 1f) - UnityEngine.Random.Range(0f, 1f)) * (1 + maxRange - minRange) + minRange);
        return spawnPosition *= percentPos;
    }
    /*
     * Find the spawn prefab accordint to it's weight, starts by calculating the total weight
      Calculates a random weight between 0 and total weight, removes for the random wright the values of weight of the items
      returns when it is lower than zero
      */
    private GameObject FindSpawnPrefab() {
        int totalWeight = 0;
        foreach (EnemyInfo item in enemyInfos)
            totalWeight += item.spawnWeight;
        int randWeight = UnityEngine.Random.Range(0, totalWeight+1);
        foreach (EnemyInfo item in enemyInfos) {
            randWeight -= item.spawnWeight;
            if (randWeight <= 0) return item.enemyPrefab;
        }
        return null;
    }

    public void PlayerKilledEnemy(int currency = 0) {
        this.currency += currency;
        this.currencyText.text = " <size=120%>:</size> "+this.currency;
    }
    public bool removeCurrency(int value) {
        if (currency >= value) {
            currency -= value;
            this.currencyText.text = " <size=120%>:</size> " + this.currency;
            return true;
        }
        else {
            return false;
        }
    }

    public void PauseGame() {
        Debug.Log("Game Paused.");
        canSpawn = false;
        Time.timeScale = 0f;
    }
    public void UnpauseGame() {
        Debug.Log("Game Unpaused.");
        canSpawn = true;
        Time.timeScale = 1f;
    }

    public void PlayerWon() {
        PauseGame();
        DefaultState();
        shopButton.SetActive(false);
        winScreen.SetActive(true);
    }
    public void PlayerLost() {
        PauseGame();
        DefaultState();
        shopButton.SetActive(false);
        loseScreen.SetActive(true);
    }

}

[System.Serializable]
public class EnemyInfo
{
    public int spawnWeight = 1;
    public GameObject enemyPrefab;
}