using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem bloodSplash;
    [SerializeField] private int bleedingTime = 2;

    [SerializeField] private float initialHealth;
    [SerializeField] private float deathForce;

    private float currentHealth;

    void Start()
    {
        currentHealth = initialHealth;

        setRigidbodyState(true);
        setColliderState(false);
    }


    public void dealDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            die(Vector3.forward);
    }

    private void die(Vector3 shotPos)
    {
        setRigidbodyState(false);
        setColliderState(true);
        push(shotPos);
    }


    private void push(Vector3 shotPos)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider objects in colliders)
        {
            Rigidbody rigidbody = objects.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.AddForce((transform.position - shotPos) * deathForce, ForceMode.Force);
            }
        }
        bloodSplash.Play();
        StartCoroutine(bloodForSecs(bleedingTime));
    }

    IEnumerator bloodForSecs(int bleedingTime)
    {
        yield return new WaitForSeconds(bleedingTime);
        bloodSplash.Stop();
    }

    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
        GetComponent<Collider>().enabled = !state;
    }

}