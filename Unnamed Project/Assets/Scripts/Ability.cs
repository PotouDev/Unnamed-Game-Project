using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Scriptable Objects/Ability")]
public abstract class Ability : ScriptableObject
{
    public string abiltiyName = "New Ability";
    public float cooldown;
    public KeyCode activationKey = KeyCode.None;
    public bool isHoldToUse = false;
    public abstract void Activate(GameObject user);
    public virtual void Deactivate(GameObject user)
    {
        // Do Nothing
    }
}
