using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roundsystem : MonoBehaviour
{
    // Start is called before the first frame update
    public int Round = 0;
    public bool Playing = false;
    EnemySpawner Spawner;
    TurretManager Turret;
<<<<<<< Updated upstream
    IEnumerator Timer(float Time)
    {
        Spawner.SetSpawning(true);
        Playing = true;
        yield return new WaitForSeconds(Time);
        Playing = false;
        Spawner.RoundClear();
=======
    Moneymanager moneymanager;
    public TMP_Text Counter;
    public TMP_Text Rounder;
    int counter = 0;
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
>>>>>>> Stashed changes
    }
    public void GetTurret(TurretManager Tur)
    {
        Turret = Tur;
    }
    public void TurretSelect()
    {
        Turret.GetTurret(0);
    }
    public void StartRound()
    {
        Round++;
        float m_Time;
        float m_SpawnTime;
        switch(Round)
        {
            case 1:
                m_Time = 15;
                m_SpawnTime = 1.0f;
                break;
            case 2:
                m_Time = 20;
                m_SpawnTime = 0.7f;
                break;
            case 3:
                m_Time = 25;
                m_SpawnTime = 0.5f;
                break;
            default:
                m_Time = 20;
                m_SpawnTime = 0.3f;
                break;
        }
        Spawner.SetSpawnTime(m_SpawnTime);
        StartCoroutine(Timer(m_Time));
    }
    public void SetSpawner(EnemySpawner Spawnner)
    {
        Spawner = Spawnner;
    }

    void Update()
    {
        
    }
<<<<<<< Updated upstream
=======
    void Update()
    {
        
    }
>>>>>>> Stashed changes
}
