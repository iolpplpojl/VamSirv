using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "Scripeable Object/Enemy Data", order = int.MaxValue)]
public class EnemyData : ScriptableObject
{
    // Start is called before the first frame update
    public int HP;
    public float speed;
    public float Value; //Drop µ· °¡Ä¡
    public float DropPer; // Drop µ· È®·ü
    public int exp;
    public int Damage;

}

