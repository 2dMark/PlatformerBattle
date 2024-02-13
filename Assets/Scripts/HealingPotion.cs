using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private int _recoverableHealth;

    public int GetRecoverableHealth => _recoverableHealth;
}
