using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticEnemyController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _chaseSpeed;

    private Transform _player;
    private bool _isChasing = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null)
        {
            PlayerController player = collider.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                _player = player.transform;
                _isChasing = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_isChasing || _player == null)
            return;

        Vector2 direction = ((Vector2)_player.position - _rigidbody.position).normalized;

        _rigidbody.MovePosition(_rigidbody.position + direction * _chaseSpeed * Time.fixedDeltaTime);
    }
}
