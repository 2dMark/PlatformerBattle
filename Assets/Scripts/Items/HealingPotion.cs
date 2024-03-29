using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private int _recoverableHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            if (collision.TryGetComponent(out Health health))
            {
                if (health.Amount == health.MaxAmount || health.IsAlive == false)
                    return;

                health.Heal(_recoverableHealth);

                Destroy(gameObject);
            }
        }
    }
}