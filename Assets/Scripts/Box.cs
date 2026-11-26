using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject _woodChipPrefab;
    [SerializeField] private int _chipCount = 8;
    [SerializeField] private float _explosionForce = 4f;
    [SerializeField] private float _chipLifetime = 2f;
    [SerializeField] private int _maxHealth;

    private int _currentHealth;
    private bool _isDestroyed;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_isDestroyed || damage <= 0)
            return;

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            BreakBox();
        }
    }

    private void BreakBox()
    {
        if (_isDestroyed)
            return;

        _isDestroyed = true;

        if (_woodChipPrefab != null)
        {
            for (int i = 0; i < _chipCount; i++)
            {
                GameObject chip = Instantiate(_woodChipPrefab, transform.position, Random.rotation);

                Rigidbody2D chipRigidbody = chip.GetComponent<Rigidbody2D>();

                if (chipRigidbody != null)
                {
                    Vector2 direction = Random.insideUnitCircle.normalized;

                    chipRigidbody.AddForce(direction * _explosionForce, ForceMode2D.Impulse);
                    chipRigidbody.AddTorque(Random.Range(-_explosionForce, _explosionForce), ForceMode2D.Impulse);
                }

                Destroy(chip, _chipLifetime);
            }
        }

        Destroy(gameObject);
    }
}
