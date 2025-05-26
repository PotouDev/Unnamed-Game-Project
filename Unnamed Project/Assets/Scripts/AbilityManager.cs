using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public Ability[] abilities;
    private float[] cooldownTimers;
    void Start()
    {
        cooldownTimers = new float[abilities.Length];
    }
    void Update()
    {
        for (int i = 0; i < cooldownTimers.Length; i++)
        {
            PlayerController controller = GetComponent<PlayerController>();
            cooldownTimers[i] -= Time.deltaTime;
            Ability ability = abilities[i];

            if (ability.isHoldToUse)
            {
                // Ability activates continuously while holding the key and cooldown is ready
                if (Input.GetKey(ability.activationKey))
                {
                    
                    if (cooldownTimers[i] <= 0)
                    {
                        controller.firingAbility = true;
                        ability.Activate(gameObject);
                        cooldownTimers[i] = ability.cooldown;
                    }
                } else
                {
                    controller.firingAbility = false;
                    ability.Deactivate(gameObject);
                }
            } else
            {
                // For normal abilities activated on key press
                if (Input.GetKeyDown(abilities[i].activationKey))
                {
                    TryUseAbility(i);
                }
            }
        }
    }
    public void TryUseAbility(int index)
    {
        if (index < 0 || index >= abilities.Length) return;
        if (cooldownTimers[index] <= 0)
        {
            abilities[index].Activate(gameObject);
            cooldownTimers[index] = abilities[index].cooldown;
        }
        else
        {
            Debug.Log("Ability on Cooldown");
        }
    }
}
