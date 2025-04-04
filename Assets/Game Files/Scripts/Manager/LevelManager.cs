using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private GameObject[] levels;


    [Header("Settings")]
    private int levelIndex;


    [Header("Debug")]
    [SerializeField] private bool PreventLevelSpawn;

    private void Awake()
    {
        LoadData();
        if (!PreventLevelSpawn)
            SpawnLevel();
        GameManager.OnGameStateChanged += GameStateChangeCallback;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangeCallback;

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GameStateChangeCallback(GameState state)
    {
        switch (state)
        {
            case GameState.LevelComplete:
                levelIndex++;
                SaveData();
                break;
        }
    }

    private void SpawnLevel()
    {
        if (levelIndex >= levels.Length)
            levelIndex = 0;

        GameObject levelInstance = Instantiate(levels[levelIndex], transform);

        StartCoroutine(EnableLevelCoroutine());

        IEnumerator EnableLevelCoroutine()
        {
            yield return new WaitForSeconds(Time.deltaTime);
            levelInstance.SetActive(true);
        }
    }

    private void LoadData()
    {
        levelIndex = PlayerPrefs.GetInt("Level");
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Level", levelIndex);
    }
}
