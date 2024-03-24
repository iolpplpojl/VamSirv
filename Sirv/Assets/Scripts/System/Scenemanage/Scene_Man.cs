using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Man : MonoBehaviour
{
    // Start is called before the first frame update
    public int PlayerType;
    public GameObject[] PlayerPrefs;
    public GameObject cam;
    public GameObject Spawner;
    public GameObject AmmoUI;
    public GameObject SkillUI;
    public GameObject Turret_Manager;
    public GameObject Moneymanager;
    public GameObject Roundmanager;
    public GameObject Rewardmanager;
    public GameObject HealthUI;
    public GameObject Systems;
    public GameObject SFXmanager;
    public GameObject DamagePopup;
    public void GetData(int PlayerType)
    {
        this.PlayerType = PlayerType;
        Debug.Log("GetData");
        SceneManager.LoadScene("Legitgame", LoadSceneMode.Additive);
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        while (true)
        {
            if (SceneManager.GetSceneByName("Legitgame").isLoaded)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("Legitgame"));

                GameObject m_System = Instantiate(Systems);
                GameObject m_Player = Instantiate(PlayerPrefs[PlayerType]);
                GameObject m_cam = Instantiate(cam);
                m_cam.GetComponentInChildren<Cam_MouseFollow>().FindPlayer();
                GameObject m_Popup = Instantiate(DamagePopup);  
                GameObject m_spawn = Instantiate(Spawner, m_System.transform);
                GameObject m_moneymanager = Instantiate(Moneymanager, m_System.transform);
                m_spawn.GetComponent<EnemySpawner>().SetMoneyManager(m_moneymanager.GetComponent<Moneymanager>()) ;
                GameObject m_ammoui =Instantiate(AmmoUI, m_System.transform);
                m_ammoui.GetComponentInChildren<AmmoUI>().getData();
                GameObject m_skillui = Instantiate(SkillUI, m_System.transform);
                m_skillui.GetComponent<SkillUI>().getData();
                GameObject m_turret = Instantiate(Turret_Manager, m_System.transform);
                m_turret.GetComponent<TurretManager>().GetPlayer(m_Player.GetComponent<Player>());
                GameObject m_Round = Instantiate(Roundmanager, m_System.transform);
                m_Round.GetComponent<Roundsystem>().SetSpawner(m_spawn.GetComponent<EnemySpawner>());
                m_Round.GetComponent<Roundsystem>().GetTurret(m_turret.GetComponent<TurretManager>());
                GameObject m_Reward = Instantiate(Rewardmanager, m_System.transform);
                m_Reward.GetComponent<Rewardsystem>().GetSystem(m_Player.GetComponent<Player>(), m_Round.GetComponent<Roundsystem>(), m_turret.GetComponent<TurretManager>(),m_moneymanager.GetComponent<Moneymanager>());
                GameObject m_health = Instantiate(HealthUI, m_System.transform);
                m_health.GetComponent<Health>().GetPlayer(m_Player.GetComponent<Player>());
                GameObject m_SFX = Instantiate(SFXmanager);
                m_Player.GetComponent<Player>().GetRewardsystem(m_Reward.GetComponent<Rewardsystem>());
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
