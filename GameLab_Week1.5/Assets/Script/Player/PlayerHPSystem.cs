using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHPSystem : MonoBehaviour
{
    static int playerHP = 3;
    GameEvents gameEvents;
    private bool isInvincible = false;
    void Start()
    {
        gameEvents = GameObject.Find("GameEventManager").GetComponent<GameEvents>();
        playerHP = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(!isInvincible)
        {
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EpicEnemy")
            {
                playerHP--;
                gameEvents.CallOnPlayerHPChange();
                Destroy(other.gameObject);
                StartCoroutine("Invincible");
            }
            else if (other.gameObject.tag == "Razer")
            {
                playerHP--;
                gameEvents.CallOnPlayerHPChange();
                StartCoroutine("Invincible");
            }
        }
        if(playerHP <= 0)
        {
            StartCoroutine("GameOver");
        }

    }

    public int GetPlayerHP()
    {
        return playerHP;
    }

    IEnumerator Invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(0.5f);
        isInvincible = false;
    }
    
    IEnumerator GameOver()
    {
        GetComponent<BulletGen>().stop();
        GetComponent<PlayerControl>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        gameEvents.CallOnGameOver();
    }
}
