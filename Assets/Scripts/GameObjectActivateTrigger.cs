using UnityEngine;

public class GameObjectActivateTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _activateObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_activateObject.gameObject.activeSelf == true)
            return;

        if (collision != null)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                _activateObject.SetActive(true);
            }
        }
    }
}
