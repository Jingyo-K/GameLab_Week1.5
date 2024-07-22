using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    void Start()
    {
        GameEvents.PlayerHPChange += OnPlayerHPChange;
    }

    void OnPlayerHPChange(GameEvents gameEvents)
    {
        PlayerHPSystem playerHPSystem = GameObject.FindWithTag("Player").GetComponent<PlayerHPSystem>();
        int hp = playerHPSystem.GetPlayerHP();
        if(hp == 1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else if(hp == 2)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
        }
        else if(hp == 3)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else if(hp == 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }
    void OnDestroy()
    {
        GameEvents.PlayerHPChange -= OnPlayerHPChange;
    }
}
