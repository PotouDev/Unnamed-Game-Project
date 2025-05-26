using System.Collections;
using UnityEditor.SettingsManagement;
using System.Net;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Shoot")]
public class ShootAbility : Ability
{
    public float damage;
    public float range;
    public LayerMask hitLayers;
    private Transform firePoint;
    public float tracerDuration = 0.05f;
    private Camera cam;
    public LineRenderer lineRenderer;
    public override void Activate(GameObject user)
    {
        firePoint = user.transform.Find("FirePoint");
        cam = Camera.main;
        if (firePoint != null && cam != null)
        {
            user.GetComponent<MonoBehaviour>().StartCoroutine(FireShot());
            lineRenderer = user.transform.Find("FirePoint").GetComponent<LineRenderer>();
            PlayerController controller = user.GetComponent<PlayerController>();
        }
    }
    IEnumerator FireShot()
    {
        // Get a ray from the center of the screen (crosshair)
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
        Ray screenRay = Camera.main.ScreenPointToRay(screenCenter);

        // Default end point in case we hit nothing
        Vector3 targetPoint = screenRay.origin + screenRay.direction * range;

        // Cast from camera to get the exact crosshair aim point
        if (Physics.Raycast(screenRay, out RaycastHit aimHit, range, hitLayers))
        {
            targetPoint = aimHit.point;
        }

        // Calculate the direction from the firePoint to the targetPoint
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        // Cast a ray from the firePoint in that direction
        Vector3 endPoint = firePoint.position + direction * range;
        if (Physics.Raycast(firePoint.position, direction, out RaycastHit hit, range, hitLayers))
        {
            endPoint = hit.point;

            Health health = hit.transform.GetComponent<Health>();
            if (health != null)
            {
                health.hitPoints -= damage;
            }
        }

        // Draw tracer line from firePoint to endPoint
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, endPoint);
            lineRenderer.enabled = true;

            yield return new WaitForSeconds(tracerDuration);

            lineRenderer.enabled = false;
        }
    }
}
