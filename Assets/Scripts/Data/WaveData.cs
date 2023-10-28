using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/WaveData")]
public class WaveData : ScriptableObject
{
	public int Count = 10;
	public EnemyData EnemyData;
	public float SpawnDelay = 1f;
}
