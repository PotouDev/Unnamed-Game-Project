using UnityEditor.SettingsManagement;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "Abilities/Sprint")]
public class SprintAbility : Ability
{
    private float originalSpeed;
    public float speedMultiplier = 1.25f;
    public override void Activate(GameObject user)
    {
        PlayerController controller = user.GetComponent<PlayerController>();
        if (controller != null)
        {
            originalSpeed = controller.moveSpeed;
            controller.moveSpeed *= speedMultiplier;
        }
    }
    public override void Deactivate(GameObject user)
    {
        PlayerController controller = user.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.moveSpeed = originalSpeed;
        }
    }
}