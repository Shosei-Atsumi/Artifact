using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class GameController : MonoBehaviour
{
    #region インスペクター表示

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text playerDeadText;

    #endregion

    public ReactiveProperty<int> score = new ReactiveProperty<int>(0);

    // Start is called before the first frame update
    void Start()
    {
        score.Subscribe(x => scoreText.text = x.ToString("0"));

        playerController.Player_die
            .Where(x => x == true)
            .Subscribe(_ => PlayerDead());
    }

    private void PlayerDead()
    {
        playerDeadText.text = "Player Dead!!";
        StartCoroutine(ResetScene());
    }

    private IEnumerator ResetScene()
    {
        yield return new WaitForSeconds(3);
        // 現在のScene名を取得する
        Scene loadScene = SceneManager.GetActiveScene();
        // Sceneの読み直し
        SceneManager.LoadScene(loadScene.name);
    }
}
