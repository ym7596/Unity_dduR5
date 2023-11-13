using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RaccoonSO/raccoons",fileName = "RaccoonSO")]
public class RaccoonSO : ScriptableObject
{
    public List<Raccoons> RaccoonsList;
}

[Serializable]
public class Raccoons
{
    public string name;
    public float moveSpeed;
    public float normalMissileSpeed;
    public int hp;
    public int special;
    public float gSpecialGage;
}
