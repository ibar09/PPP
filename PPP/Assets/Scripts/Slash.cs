using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public Transform car;
    [SerializeField] private float speed;
    [SerializeField] private GameObject explosion;
    private Rigidbody rb;
    private Vector3 direction;
    private float currentAltitude;
    [SerializeField] private float explosionForce = 1000;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = car.forward;
        currentAltitude = car.transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        float x;
        if (car.transform.position.y <= currentAltitude)
            x = -0.4f;
        else
            x = 2.4f;
        currentAltitude = car.transform.position.y;
        rb.velocity = direction * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, car.transform.position.y + x, transform.position.z);
        //Debug.Log((transform.position.y - car.transform.position.y).ToString());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, 10);
            other.gameObject.GetComponent<Player>().GotHit(other.transform.position);
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        SoundManager.Instance.Play("explosion");
        Destroy(gameObject);

    }
}
