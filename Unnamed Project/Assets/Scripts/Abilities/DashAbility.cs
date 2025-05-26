using UnityEditor.SettingsManagement;
using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Abilities/Dash")]
public class DashAbility : Ability
{
    public float speedMultiplier = 2.5f;
    public float dashDuration = 1.5f;
    public override void Activate(GameObject user)
    {
        // For Now Use This For Dash
        PlayerController controller = user.GetComponent<PlayerController>();
        controller.StartCoroutine(DashBoost(controller));

        // FIX THIS SHIT LATER

        //Transform cam = user.GetComponent<PlayerController>().cam;
        //Rigidbody rb = user.GetComponent<Rigidbody>();
        //if (rb != null && cam != null)
        //{
        //   Vector3 dashDirection = cam.transform.forward;
        //    rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);
        //}
    }

    private IEnumerator DashBoost(PlayerController controller)
    {
        float originalSpeed = controller.moveSpeed;
        controller.moveSpeed = originalSpeed * speedMultiplier;
        yield return new WaitForSeconds(dashDuration);
        controller.moveSpeed = originalSpeed;
    }
}
