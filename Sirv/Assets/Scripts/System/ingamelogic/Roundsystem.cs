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
    IEnumerator Timer(float Time)
    {
        Spawner.SetSpawning(true);
        yield return new WaitForSeconds(Time);
        Playing = false;
        Spawner.RoundClear();
    }
    IEnumerator Threecount(float Time)
    {
        Playing = true;
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(Timer(Time));
    }
    public void GetTurret(TurretManager Tur)
    {
        Turret = Tur;
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
