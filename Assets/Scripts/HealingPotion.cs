using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private int _recoverableHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStatus playerStatus))
        {
            if (playerStatus.Health == playerStatus.MaxHealth)
                return;

            playerStatus.Heal(_recoverableHealth);

            Destroy(gameObject);
        }
    }
}
