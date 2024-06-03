using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] int life = 2;
    [SerializeField] GameObject hat;
    Position Position;

    private void Start()
    {
        Position = GetComponentInParent<Position>();
    }
    private void OnParticleCollision(GameObject other)
    {
        Destroy(hat);
        life -= 1;
        if (life < 0)
        {
            Position.isDead = true;
            Destroy(gameObject);
        }
    }
}
