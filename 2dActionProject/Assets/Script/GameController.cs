using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class GameController : MonoBehaviour
{
    #region serializePrivate
    [SerializeField]
    private Text timeLeftText;

    [SerializeField]
    private GameObject pauseMenuObj;

    [SerializeField]
    private float timeLeft = 300;
    #endregion


    #region private
    private readonly ReactiveProperty<float> _timeLeft = new FloatReactiveProperty(0);

    private readonly ReactiveProperty<bool> _pauseSwitch = new BoolReactiveProperty(false);

    private List<IDisposable> disposableList = new List<IDisposable>();
    #endregion


    public ReadOnlyReactiveProperty<bool> _Pause
    {
        get { return _pauseSwitch.ToReadOnlyReactiveProperty(); }
    }

    void Start()
    {
        _timeLeft.Value = timeLeft;

        this.UpdateAsObservable()
            .Where(_ => Input.GetKeyDown(KeyCode.Escape))
            .Subscribe(_ => _pauseSwitch.Value = !_pauseSwitch.Value);

        this.UpdateAsObservable()
            .Subscribe(_ => timeLeftText.text = "あと " + _timeLeft.Value.ToString("F1") + " 秒");

        _pauseSwitch
            .Subscribe(b => PauseMenu(b));

        _timeLeft
            .Where(t => t <= 0)
            .Subscribe(_ => Debug.Log("GameEnd"));
    }

    /// <summary>
    /// disposableテスト用
    /// </summary>
    // カウント開始
    private void ObservableStart()
    {
        var disposable = Observable.Interval(TimeSpan.FromMilliseconds(1))
            .Subscribe(_ => _timeLeft.Value -= 0.01f);
        disposableList.Add(disposable);
    }

    private void PauseMenu(bool onOff)
    {
        pauseMenuObj.SetActive(onOff);
        // ポーズした場合 Dispose()
        if (onOff)
        {
            foreach(var disposable in disposableList) {
                disposable.Dispose();
            }
            Time.timeScale = 0;
        }
        else
        {
            ObservableStart();
            Time.timeScale = 1;
        }
    }

}
