using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TurretManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public List<Transform> TurretPos;
    public GameObject[] Turret;
    public int TurretMaxCount;
    public int TurretCount = 0;
    public List<Turret> Turrets;
    public bool on = false;
    void Awake()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Turretpos");
        for (int i = 0; i < temp.Length; i++)
        {
            TurretPos.Add(temp[i].GetComponent<Transform>());
        }
        TurretMaxCount = TurretPos.Count;
    }
    public void GetPlayer(Player player)
    {
        this.player = player;

    }
    private void Update()
    {
        if (on)
        {
            GetTurret(0,0);
            on = false;
        }
    }
    public void GetTurret(int idx, int Rarity)
    {
        if (TurretMaxCount != TurretCount){
            Turret Tur = Instantiate(Turret[idx]).GetComponent<Turret>();
            Turrets.Add(Tur);
            Tur.GetPlayerComp(player);
            Tur.Rarity = Rarity;
            Tur.Player = TurretPos[TurretCount];
            TurretCount++;
        }
    }

    public bool Upgrade(GameObject turret)
    {
        Turret m_turret = turret.GetComponent<Turret>();
        List<Turret> Filtered = Turrets.Where(n => n.TurretNum == m_turret.TurretNum).ToList();
        if(Filtered.Count != 0)
        {
            Filtered[0].GetComponent<Turret>().
            Upgrade();
            return true;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    
}
