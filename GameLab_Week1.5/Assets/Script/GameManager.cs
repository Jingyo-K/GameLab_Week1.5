using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameEvents gameEvents;
    public CamManager camManager;
    public PlayerControl playerControl;
    public GameObject GameOverUI;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameEvents = GetComponent<GameEvents>();
        camManager = GameObject.FindWithTag("MainCamera").GetComponent<CamManager>();
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
        gameEvents.CallOnItemCollected();
        GameEvents.GameOver += OnGameOver;
    }

    void OnGameOver(GameEvents gameEvents)
    {
        GameOverUI.SetActive(true);
    }
    void OnDestroy()
    {
        GameEvents.GameOver -= OnGameOver;
    }
}
