using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Creature Data", menuName = "Creature Data")]
public class CreatureData : ScriptableObject
{
    public float hp;
    public float jumpForce;
    public float speed;
    public float damage;
    public float defence;
}
