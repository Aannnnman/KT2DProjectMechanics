using UnityEngine;

public class ShootingEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _shootForce;
    [SerializeField] private bool _shootingDirectionIsRight;
    [SerializeField] private bool _shootingModeIsAiming;

    private Transform _player;
    private float _nextFireTime;

    private void Start()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();

        if (player != null)
            _player = player.transform;

        _nextFireTime = Time.time + _fireRate;
    }

    private void Update()
    {
        if (Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + _fireRate;
        }
    }

    private void Shoot()
    {
        Vector2 direction = ((Vector2)_player.position - (Vector2)_firePoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _shootingModeIsAiming ? rotation : _firePoint.rotation);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        if (bulletRigidbody != null)
        {
            bulletRigidbody.linearVelocity = _shootingModeIsAiming ?
                direction * _shootForce :
                (_shootingDirectionIsRight ? 1 : -1) * (Vector2)_firePoint.right * _shootForce;
        }
    }
}
