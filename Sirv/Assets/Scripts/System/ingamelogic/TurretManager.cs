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
    public List<GameObject> Turrets_OBJ;
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
    public void GetTurret(int idx, int Rarity)
    {
        if (TurretMaxCount != TurretCount){
            GameObject turret = null;

            switch (Rarity)
            {
                case 0:
                    switch (idx)
                    {
                        case 0:
                            turret = Turret[0];
                            break;
                        case 1:
                            turret = Turret[1];
                            break;
                        case 2:
                            turret = Turret[2];
                            break;
                    }
                    break;
                case 1:
                    switch (idx)
                    {
                        case 0:
                            turret = Turret[4];
                            break;
                        case 1:
                            turret = Turret[2];
                            break;
                        case 2:
                            turret = Turret[2];
                            break;
                    }
                    break;
                case 2:
                    switch (idx)
                    {
                        case 0:
                            turret = Turret[0];
                            break;
                        case 1:
                            turret = Turret[1];
                            break;
                        case 2:
                            turret = Turret[2];
                            break;
                    }
                    break;
                case 3:
                    switch (idx)
                    {
                        case 0:
                            turret = Turret[0];
                            break;
                        case 1:
                            turret = Turret[1];
                            break;
                        case 2:
                            turret = Turret[2];
                            break;
                    }
                    break;
                case 4:
                    switch (idx)
                    {
                        case 0:
                            turret = Turret[0];
                            break;
                        case 1:
                            turret = Turret[1];
                            break;
                        case 2:
                            turret = Turret[2];
                            break;
                    }
                    break;
                case 1000: //engi
                    {
                        switch (idx)
                        {
                            case 0:
                                turret = Turret[3];
                                break;
                        }
                    }
                    break;
            }
            GameObject OBJ = Instantiate(turret);
            Turret Tur = OBJ.GetComponent<Turret>();
            Turrets_OBJ.Add(OBJ);
            Turrets.Add(Tur);
            Tur.GetPlayerComp(player);
            Tur.Player = TurretPos[TurretCount];
            TurretCount++;
        }
    }
    public void ResetPos()
    {
        for(int i = 0; i < TurretCount; i++)
        {
            Turrets[i].Player = TurretPos[i];
        }
    }
    public int Remove(int idx)
    {
        int m_money = Turrets[idx].price;
        int m_Rairty = Turrets[idx].Rarity;
        switch (m_Rairty)
        {
            case 0:
                m_Rairty = 1;
                break;
            case 1:
                m_Rairty = 3;
                break;
            case 2:
                m_Rairty = 7;
                break;
            case 3:
                m_Rairty = 15;
                break;
        }

        GameObject temp = Turrets_OBJ[idx];
        Destroy(temp);
        Turrets_OBJ.Remove(temp);
        Turrets.Remove(Turrets[idx]);
        TurretCount--;
        ResetPos();
        Debug.Log((int)((m_money * m_Rairty) * 0.66));
        return (int)((m_money * m_Rairty) * 0.66);
    }
    public bool Upgrade(int n, int rair)
    {
        GameObject turret = null;

        switch (rair) {
            case 0:
                switch (n)
                {
                    case 0:
                        turret = Turret[0];
                        break;
                    case 1:
                        turret = Turret[1];
                        break;
                    case 2:
                        turret = Turret[2];
                        break;
                }
                break;
            case 1:
                switch (n)
                {
                    case 0:
                        turret = Turret[4];
                        break;
                    case 1:
                        turret = Turret[2];
                        break;
                    case 2:
                        turret = Turret[2];
                        break;
                }
                break;
            case 2:
                switch (n)
                {
                    case 0:
                        turret = Turret[0];
                        break;
                    case 1:
                        turret = Turret[1];
                        break;
                    case 2:
                        turret = Turret[2];
                        break;
                }
                break;
            case 3:
                switch (n)
                {
                    case 0:
                        turret = Turret[0];
                        break;
                    case 1:
                        turret = Turret[1];
                        break;
                    case 2:
                        turret = Turret[2];
                        break;
                }
                break;
            case 4:
                switch (n)
                {
                    case 0:
                        turret = Turret[0];
                        break;
                    case 1:
                        turret = Turret[1];
                        break;
                    case 2:
                        turret = Turret[2];
                        break;
                }
                break;
            case 1000: //engi
                {
                    switch (n)
                    {
                        case 0:
                            turret = Turret[3];
                            break;
                    }
                }
                break;
        }

        Turret m_turret = turret.GetComponent<Turret>();
        Debug.Log(m_turret);

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
