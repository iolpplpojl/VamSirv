using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> TurretPos;
    public GameObject[] Turret;
    int TurretCount = 0;
    public bool on = false;
    void Start()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Turretpos");
        for(int i = 0; i < temp.Length; i++)
        {
            TurretPos.Add(temp[i].GetComponent<Transform>());
        }
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
        if (TurretPos.Count != TurretCount){
            Turret Tur = Instantiate(Turret[idx]).GetComponent<Turret>();
            Tur.Player = TurretPos[TurretCount];
            TurretCount++;
        }
    }

    // Update is called once per frame
    
}
