using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerController : MonoBehaviour
{
    #region インスペクター表示

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float shootIntervalSeconds = 0.25f;

    [SerializeField]

    private Vector2 rightShootPosition = new Vector2(0.13f, 0.4f),
                    leftShootPosition = new Vector2(-0.13f, 0.4f);

    #endregion

    #region private

    private Vector2 force;

    #endregion


    private readonly ReactiveProperty<bool> player_die = new BoolReactiveProperty(false);

    public ReadOnlyReactiveProperty<bool> Player_die
    {
        get { return player_die.ToReadOnlyReactiveProperty(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => !player_die.Value)
            .Select(_ => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")))
            .Subscribe(v => force = v);

        this.FixedUpdateAsObservable()
            .Subscribe(_ => Move(force));

        this.FixedUpdateAsObservable()
            .Where(_ => Input.GetKey(KeyCode.Space) && !player_die.Value)
            .ThrottleFirst(System.TimeSpan.FromSeconds(shootIntervalSeconds))
            .Subscribe(_ => Shoot());

        this.OnCollisionEnter2DAsObservable()
            .Where(col => col.collider.CompareTag("enemy") || col.collider.CompareTag("enemy_bullet"))
            .Subscribe(_ => player_die.Value = true);

        player_die
            .Where(b => b)
            .Subscribe(_ => animator.SetInteger("State", 1));
    }

    private void Move(Vector2 v2)
    {
        if (!player_die.Value)
        {
            rb.velocity = v2 * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    private void Shoot()
    {
        Instantiate(bullet, new Vector2(transform.position.x + rightShootPosition.x, transform.position.y + rightShootPosition.y), Quaternion.identity);
        Instantiate(bullet, new Vector2(transform.position.x + leftShootPosition.x, transform.position.y + leftShootPosition.y), Quaternion.identity);
    }
}
