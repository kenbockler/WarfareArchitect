using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int Health = 2;
	//public int Damage = 1;
	public float Speed = 50f;
	public Sprite Sprite = null;
}
