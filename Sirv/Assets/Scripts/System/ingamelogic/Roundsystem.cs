using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Roundsystem : MonoBehaviour
{
    // Start is called before the first frame update
    public int Round = 0;
    public bool Playing = false;
    EnemySpawner Spawner;
    TurretManager Turret;
    public TMP_Text Counter;
    public TMP_Text Rounder;
    int counter = 0;
    IEnumerator Timer(float Time)
    {
        counter = (int)Time;
        Rounder.text = string.Format("ROUND {0}",Round.ToString());
        Spawner.SetSpawning(Round);
        for (int i = 0; i <= Time; i++) {
            SetText();
            yield return new WaitForSeconds(1.0f);
            counter--;
        }
        Spawner.RoundClear();
        StartCoroutine(EndThreeCount());
    }
    IEnumerator EndThreeCount()
    {
        yield return new WaitForSeconds(1.0f);
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
        StartCoroutine(Timer(Time));
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
                m_SpawnTime[1] = 1.70f;
                m_SpawnTime[2] = 3.6f;
                break;
            case 4:
                m_Time = 25;
                m_SpawnTime[0] = 0.4f;
                m_SpawnTime[1] = 1.70f;
                m_SpawnTime[2] = 3.6f;
                break;
            case 5:
                m_Time = 25;
                m_SpawnTime[0] = 0.4f;
                m_SpawnTime[1] = 1.70f;
                m_SpawnTime[2] = 3.6f;
                break;
            case 6:
                m_Time = 25;
                m_SpawnTime[0] = 0.4f;
                m_SpawnTime[1] = 1.70f;
                m_SpawnTime[2] = 3.6f;
                break;
            case 7:
                m_Time = 25;
                m_SpawnTime[0] = 0.4f;
                m_SpawnTime[1] = 1.70f;
                m_SpawnTime[2] = 3.6f;
                break;
            case 8:
                m_Time = 25;
                m_SpawnTime[0] = 0.33f;
                m_SpawnTime[1] = 1.70f;
                m_SpawnTime[2] = 3.6f;
                break;
            case 9:
                m_Time = 25;
                m_SpawnTime[0] = 0.28f;
                m_SpawnTime[1] = 1.70f;
                m_SpawnTime[2] = 3.6f;
                break;
            default:
                m_Time = 35;
                m_SpawnTime[0] = 0.22f;
                m_SpawnTime[1] = 1.7f;
                m_SpawnTime[2] = 3.6f;
                break;
        }
        Spawner.SetSpawnTime(m_SpawnTime);
        StartCoroutine(Threecount(m_Time));
    }
    public void SetSpawner(EnemySpawner Spawnner)
    {
        Spawner = Spawnner;
    }

    void Update()
    {
        
    }
}
