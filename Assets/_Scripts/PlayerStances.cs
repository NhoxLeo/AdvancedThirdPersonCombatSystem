using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStances : MonoBehaviour
{
    [Header("Player Colours")]
    public Renderer player;
    public Material fireMaterial;
    public Material electricMaterial;
    public Material waterMaterial;
    public Material darkMagic;

    public enum Stances {water, fire, electric, dark }

    public void ChangeStance(Stances stance, PlayerBehaviour pb)
    {
        switch (stance)
        {
            case Stances.water:
                player.material = waterMaterial;
                pb.comboEquipped = PlayerBehaviour.combos.combo1;
                break;
            case Stances.fire:
                player.material = fireMaterial;
                pb.comboEquipped = PlayerBehaviour.combos.combo2;
                break;
            case Stances.electric:
                player.material = electricMaterial;
                pb.comboEquipped = PlayerBehaviour.combos.combo3;
                break;
            case Stances.dark:
                player.material = darkMagic;
                pb.comboEquipped = PlayerBehaviour.combos.combo4;
                break;
            default:
                break;
        }
    }

}
