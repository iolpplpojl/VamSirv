using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roundsystem : MonoBehaviour
{
    // Start is called before the first frame update
    public int Round = 0;
    bool Playing = false;
    EnemySpawner Spawner;
    IEnumerator Timer(float Time)
    {
        Spawner.SetSpawning(true);
        yield return new WaitForSeconds(Time);
        Spawner.SetSpawning(false);
    }

    public void StartRound()
    {
        Round++;
        float m_Time;

        switch(Round)
        {
            case 1:
                m_Time = 15;
                break;
            case 2:
                m_Time = 20;
                break;
            case 3:
                m_Time = 25;
                break;
            default:
                m_Time = 40;
                break;
        }
        StartCoroutine(Timer(m_Time));
    }
    public void SetSpawner(EnemySpawner Spawnner)
    {
        Spawner = Spawnner;
    }

    void Update()
    {
        
    }
}
