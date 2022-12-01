using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(fileName = "Player Values", menuName = "Scriptable Objects/Player Values")]
public class PlayerValuesSO : ScriptableObject
{
    

    public int bulletCount;
    public float moveSpeed;
    [Range(0, 3)]
    public int playerTeam;
    public Material playerTeamMaterial;
    public float playerMaxHealt = 100;
    public float playerMinHealt = 0;
    public float playerCurrentHealt;



}
