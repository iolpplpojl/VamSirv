using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Roundsystem : MonoBehaviour
{
    // Start is called before the first frame update

    public static Roundsystem instance;

    public int Round = 0;
    public bool Playing = false;
    EnemySpawner Spawner;
    TurretManager Turret;
    Moneymanager moneymanager;
    public TMP_Text Counter;
    public TMP_Text Rounder;
    int counter = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }    
    }


    IEnumerator Timer(float Time)
    {
        counter = (int)Time;
        Rounder.text = string.Format("ROUND {0}",Round.ToString());
        Spawner.SetSpawning(Round);
        Resultsystem.instance.setRound(Round);

        for (int i = 0; i <= Time; i++) {
            SetText();
            yield return new WaitForSeconds(1.0f);
            counter--;
        }
        Spawner.RoundClear();
        moneymanager.RoundOver();
        Towersystem.instance.RoundDone();
        StartCoroutine(EndThreeCount());
    }
    IEnumerator EndThreeCount()
    {

        yield return new WaitForSeconds(1.0f);
        ExpSystem.instance.Open();
        Playing = false;
    }
    IEnumerator EndThreeCountBoss()
    {
        yield return new WaitForSeconds(3.0f);
        Spawner.RoundClear();
        moneymanager.RoundOver();
        Towersystem.instance.RoundDone();
        ExpSystem.instance.Open();
        Playing = false;
    }
    IEnumerator Threecount(float Time)
    {
        counter = 3;
        Playing = true;
        for (int i = 0; i < 3; i++)
        {
            SetText();
            yield return new WaitForSeconds(0.75f);
            counter--;
        }
        if (Round % 1 == 0)
        {
            setBoss();
        }
        else
        {
            StartCoroutine(Timer(Time));
        }
    }

    public void setBoss()
    {
        Counter.text = "-";
        Spawner.SpawnBoss(Round);
    }
    public void BossDead()
    {

        StartCoroutine(EndThreeCountBoss());
    }
    public void GetTurret(TurretManager Tur)
    {
        Turret = Tur;
    }
    public void SetText()
    {
        if (counter <= 5) {
            Counter.color = new Color(1, 0.2783019f, 0.2783019f);
            Counter.text = counter.ToString();
        }
        else
        {
            Counter.color = Color.white;
            Counter.text = counter.ToString();
        }

    }
    public void StartRound()
    {
        Round++;
        float m_Time;
        float[] m_SpawnTime = new float[3];
        switch(Round)
        {
            case 1:
                m_Time = 15;
                m_SpawnTime[0] = 0.5f;
                m_SpawnTime[1] = 2.0f;
                m_SpawnTime[2] = 4.0f;
                break;
            case 2:
                m_Time = 20;
                m_SpawnTime[0] = 0.45f;
                m_SpawnTime[1] = 1.85f;
                m_SpawnTime[2] = 3.85f;
                break;
            case 3:
                m_Time = 25;
                m_SpawnTime[0] = 0.4f;
                m_SpawnTime[1] = 5f;
                m_SpawnTime[2] = 3.6f;
                break;
            case 4:
                m_Time = 25;
                m_SpawnTime[0] = 0.4f;
                m_SpawnTime[1] = 4.8f;
                m_SpawnTime[2] = 3.6f;
                break;
            case 5:
                m_Time = 25;
                m_SpawnTime[0] = 0.4f;
                m_SpawnTime[1] = 4.65f;
                m_SpawnTime[2] = 7f;
                break;
            case 6:
                m_Time = 25;
                m_SpawnTime[0] = 0.4f;
                m_SpawnTime[1] = 4.5f;
                m_SpawnTime[2] = 6.7f;
                break;
            case 7:
                m_Time = 25;
                m_SpawnTime[0] = 0.4f;
                m_SpawnTime[1] = 4.4f;
                m_SpawnTime[2] = 6.5f;
                break;
            case 8:
                m_Time = 25;
                m_SpawnTime[0] = 0.33f;
                m_SpawnTime[1] = 4.3f;
                m_SpawnTime[2] = 6.3f;
                break;
            case 9:
                m_Time = 25;
                m_SpawnTime[0] = 0.28f;
                m_SpawnTime[1] = 4.2f;
                m_SpawnTime[2] = 6.2f;
                break;
            default:
                m_Time = 35;
                m_SpawnTime[0] = 0.22f;
                m_SpawnTime[1] = 4.15f;
                m_SpawnTime[2] = 6.1f;
                break;
        }
        Spawner.SetSpawnTime(m_SpawnTime);
        Towersystem.instance.RoundStart();
        StartCoroutine(Threecount(m_Time));
    }
    public void SetSpawner(EnemySpawner Spawnner)
    {
        Spawner = Spawnner;
    }

    public void SetMoneymanager(Moneymanager m)
    {
        this.moneymanager = m;
    }

}
