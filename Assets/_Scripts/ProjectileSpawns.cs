using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawns : MonoBehaviour
{

    private WarpController wp;

    void Awake()
    {
        wp = GetComponent<WarpController>();
    }
    
    public enum SpawnLocations {staff, leftHand, rightHand }
    [Header("SpawnLocations")]
    public Transform staff;
    public Transform handR;
    public Transform handL;

    [Header("Projectiles")]
    public GameObject blueFireBall;
    public GameObject fireBolt;
    public GameObject meteor;
    public GameObject electricBall;
    public GameObject pinkBolt;
    public GameObject frostBolt;
    public GameObject frostMeteor;
    public GameObject greenBall;
    public GameObject blackFireBall;

    void ShootProjectile(AnimationEvent Stats)
    {
        if (wp.target == null)
        {
            return;
        }

        GameObject attack = blueFireBall;
        SpawnLocations sp = SpawnLocations.rightHand;

        switch (Stats.intParameter)
        {
            case 1:
                sp = SpawnLocations.rightHand;
                break;
            case 2:
                sp = SpawnLocations.leftHand;
                break;
            case 3:
                sp = SpawnLocations.staff;
                break;
            default:
                break;
        }
        switch (Stats.stringParameter)
        {
            case "blueFireBall":
                attack = blueFireBall;
                break;
            case "fireBolt":
                attack = fireBolt;
                break;
            case "meteor":
                attack = meteor;
                break;
            case "electricBall":
                attack = electricBall;
                break;
            case "pinkBolt":
                attack = pinkBolt;
                break;
            case "frostBolt":
                attack = frostBolt;
                break;
            case "frostMeteor":
                attack = frostMeteor;
                break;
            case "greenBall":
                attack = greenBall;
                break;
            case "blackFireBall":
                attack = blackFireBall;
                break;
            default:
                Debug.LogWarning("Wrong string");
                break;
        }
        Spawn_Projectile(attack, sp);
    }

    private void Spawn_Projectile(GameObject projectile, SpawnLocations spawmLocation)
    {
        EffectSettings es;
        switch (spawmLocation)
        {
            case SpawnLocations.staff:
                es = Instantiate(projectile, staff.transform.position, staff.transform.rotation).GetComponent<EffectSettings>();
                es.Target = wp.target.gameObject;
                break;
            case SpawnLocations.leftHand:
                es = Instantiate(projectile, handL.transform.position, handL.transform.rotation).GetComponent<EffectSettings>();
                es.Target = wp.target.gameObject;
                break;
            case SpawnLocations.rightHand:
                es = Instantiate(projectile, handR.transform.position, handR.transform.rotation).GetComponent<EffectSettings>();
                es.Target = wp.target.gameObject;
                break;
            default:
                break;
        }
    }

}