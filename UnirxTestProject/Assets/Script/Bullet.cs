using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        this.UpdateAsObservable()
            .Where(_ => transform.position.y > 10)
            .Subscribe(_ => Destroy(gameObject));

        this.FixedUpdateAsObservable()
            .Subscribe(_ => Move());

        this.OnTriggerEnter2DAsObservable()
            .Where(col => col.CompareTag("enemy"))
            .Subscribe(_ => Destroy(gameObject));
    }

    private void Move()
    {
        rb.velocity = Vector2.up * speed;
    }
}
