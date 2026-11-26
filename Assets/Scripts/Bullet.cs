using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifetime;
    [SerializeField] private LayerMask _whatCanToHit;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (((1 << collider.gameObject.layer) & _whatCanToHit) != 0f)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Destroy(collider.gameObject);
                Destroy(gameObject);
            }

            if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }

            if (collider.gameObject.layer == LayerMask.NameToLayer("Box"))
            {
                Box box = collider.GetComponentInParent<Box>();

                if (box != null)
                {
                    box.TakeDamage(1);
                    Destroy(gameObject);
                }
            }
        }
    }
}
