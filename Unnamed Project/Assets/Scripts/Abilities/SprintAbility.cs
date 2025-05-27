using UnityEditor.SettingsManagement;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "Abilities/Sprint")]
public class SprintAbility : Ability
{
    private float originalSpeed;
    public float speedMultiplier = 1.25f;
    PlayerController controller;


    public override void Activate(GameObject user)
    {
        controller = user.GetComponent<PlayerController>();
        controller.moveSpeed = originalSpeed;
        originalSpeed = controller.moveSpeed;
        if (controller != null)
        {
         
            if (controller.moveSpeed <= 10f)
            {
                controller.moveSpeed *= speedMultiplier;
               
            }
            else
            {
                controller.moveSpeed = originalSpeed;
            }
        }
    }
    public override void Deactivate(GameObject user)
    {
        controller.moveSpeed = originalSpeed;
    }
}