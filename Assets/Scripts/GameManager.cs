﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public float levelStartDelay = 2f;
    public float turnDelay = .1f;
    public static GameManager instance = null;
    public BoardManager boardScript;
    public BoardManager1 boardScript1;
    public BoardManager2 boardScript2;
    public BoardManager3 boardScript3;
    public int healthPoints = 100;
    public int enemyHealthPoints = 100;
    public int level = 0;
    [HideInInspector] public bool playersTurn;

    private Text levelText;
    private GameObject levelImage;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup;

    void Awake()
    {
            if (instance == null)
                        instance = this;
                    else if (instance != this)
                        Destroy(gameObject);
                    DontDestroyOnLoad(gameObject);
                    enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();
        boardScript1 = GetComponent<BoardManager1>();
        boardScript2 = GetComponent<BoardManager2>();
        boardScript3 = GetComponent<BoardManager3>();
        InitGame();

        
    }

    //this is called only once, and the parameter tells it to be called only after the scene was loaded
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void CallbackInitialization()
    {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //This is called each time a scene is loaded.
    private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        instance.level++;
        instance.InitGame();
    }

    void Update()
    {
        if (playersTurn || enemiesMoving || doingSetup)
            return;

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    public void GameOver()
    {
        levelText.text = "Game Over\n" + "\nPress 'Esc' to return to Main Menu";
        levelImage.SetActive(true);
        enabled = false;
    }

    public void YouWin()
    {
        levelText.text = "You win!\n";  // + "Press 'Esc' to return to Main Menu"
        levelImage.SetActive(true);
        enabled = false;
    }

    void InitGame()
    {
        doingSetup = true;
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find(name: "LevelText").GetComponent<Text>();
        levelText.text = "LEVEL " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        enemies.Clear();
        BoardManagerSelector();
    }

    private void BoardManagerSelector()
    {
        if (level < 5) { boardScript.SetupScene(level); }
        else if (level > 4 && level < 10) { boardScript1.SetupScene(level); }
        else if (level > 9 && level < 15) { boardScript2.SetupScene(level); }
        else if (level > 14) { boardScript3.SetupScene(level); }
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playersTurn = true;
        enemiesMoving = false;
    }
}
