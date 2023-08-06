using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] List<Enemy> enemies;
    float bestTime = 9999f;
    float timer = 0f;
    public bool isStart = false;
    [SerializeField] Player player;
    [SerializeField] List<TMP_Text> hud;
    float timerReload = 2f;
    [SerializeField] Slider reloadSlider;
    void Start()
    {
        hud[3].text = $"Health:{player.health}";
        hud[2].text = $"Ammo:{player.ammo}/9";
        hud[4].text = CountEnemyDone();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart)
        {
            timer += Time.deltaTime;
            hud[0].text = Timer(timer);          
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(player.ammo < 9) // Перезарядка
            {
                player.isReload = true;
                timerReload = 2f;
                reloadSlider.gameObject.SetActive(true);
            }
        }
        if(player.isReload)
        {
            timerReload -= Time.deltaTime;
            reloadSlider.value = 1 - (timerReload / 2);
            if(timerReload <= 0)
            {
                player.ammo = 9;
                player.isReload = false;
                SetAmmo();
                reloadSlider.gameObject.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        isStart = true;
        timer = 0;
        foreach (Enemy enemy in enemies)
        {
            enemy.StartGame();
        }
    }

    public void EndGame()
    {
        if(allEnemyDone())
        {
            isStart = false;
            if(bestTime > timer)
            {
                bestTime = timer;
                hud[1].text = $"Best time:{Timer(bestTime)}";
            }
        }
    }
    bool allEnemyDone()
    {
        bool isDone = false;
        foreach(Enemy enemy in enemies)
        {    
            if(enemy.isDown())
            {
                isDone = true;
            }
            else
            {
                return false;
            }
        }
        return isDone;
    }

    string CountEnemyDone()
    {
        int count = 0;
        foreach (Enemy enemy in enemies)
        {
            if (enemy.isDown())
            {
                count++;
            }
        }
        return $"{count}/{enemies.Count} enemy done.";
    }

    string Timer(float time)
    {
        int min = Mathf.FloorToInt(time / 60f);
        int sec = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", min, sec);
    }

    public void SetAmmo()
    {
        hud[2].text = $"Ammo:{player.ammo}/9";
    }

    public void CheckEnemy()
    {
        hud[4].text = CountEnemyDone();
    }
}
