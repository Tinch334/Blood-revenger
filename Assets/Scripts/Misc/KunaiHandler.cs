using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiHandler : MonoBehaviour
{
    [SerializeField] private int projectileDamage;

    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.transform.GetComponent<EnemyHealthHandler>().dealDamage(projectileDamage);
            Debug.Log("colision proyectil");
        }
        if (!collision.transform.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
