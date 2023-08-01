using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private IntReactiveProperty hp = new IntReactiveProperty(25);

    // Start is called before the first frame update
    void Start()
    {
        hp
            .Where(_ => hp.Value == 0)
            .Subscribe(_ => DestroyMe());

        this.OnTriggerEnter2DAsObservable()
            .Where(col => col.CompareTag("player_bullet"))
            .Subscribe(_ => hp.Value --);
    }

    private void DestroyMe()
    {
        gameController.score.Value += 100;
        Destroy(gameObject);
    }
}
