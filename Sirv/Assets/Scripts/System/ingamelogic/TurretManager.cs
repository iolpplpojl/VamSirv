using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    public List<Transform> TurretPos;
    public GameObject[] Turret;
    public int TurretMaxCount;
    public int TurretCount = 0;
    public bool on = false;
    void Start()
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
            GetTurret(0);
            on = false;
        }
    }
    public void GetTurret(int idx)
    {
        if (TurretMaxCount != TurretCount){
            Turret Tur = Instantiate(Turret[idx]).GetComponent<Turret>();
            Tur.GetPlayerComp(player);
            Tur.Player = TurretPos[TurretCount];
            TurretCount++;
        }
    }

    // Update is called once per frame
    
}
