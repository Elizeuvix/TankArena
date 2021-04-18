using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject prefabExplosion;
    public float destroyTime = 5.0f;
    public Vector3 direction;

    private void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {

        }
        Instantiate(prefabExplosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
