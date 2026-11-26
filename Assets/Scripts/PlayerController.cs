using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _moveSpeed;

    [Header("Shooting")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _shootForce;

    private float _nextFireTime;


    private Vector2 _movement;

    private void Update()
    {
        _movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        AimAtMouse();

        if (Input.GetMouseButton(0) && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + _fireRate;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }

    private void AimAtMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -_camera.transform.position.z;

        Vector3 mouseWorldPosition =_camera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (Vector2)(mouseWorldPosition - transform.position);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _firePoint.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        if (bulletRigidbody != null)
        {
            bulletRigidbody.linearVelocity = (Vector2)_firePoint.right * _shootForce;
        }
    }
}
