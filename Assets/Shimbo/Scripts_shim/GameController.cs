using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    enum State
    {
        Ready,
        Play,
        GameOver
    }

    State state;
    public PlayerController pc;
    public RightHand hand;

    public Text timeLabel;
    public Text scoreLabel;
    public Text stateLabel;
    public HpPanel hpPanel;
    float timeCount = 30f;//時間制限
    int runningDistance;//走った距離

    // Start is called before the first frame update
    void Start()
    {
        Ready();//説明文
    }

    void Update()
    {
        if (state == State.Play)
        {
            timeCount -= Time.deltaTime;
            timeLabel.text = "残り" + timeCount.ToString("0") + "秒";
        }
        
        //スコア更新
        scoreLabel.text = "倒した数 : " + pc.GetAttackCount();
        runningDistance = (int)pc.transform.position.z + 12;//走った距離
        int score = pc.GetAttackCount() + runningDistance;//総スコア

        //HP更新
        hpPanel.UpdateHp(pc.Hp());

        //ハイスコア
        if (PlayerPrefs.GetInt("HighScoreSHIMBO") < score)
        {
            PlayerPrefs.SetInt("HighScoreSHIMBO", score);
            PlayerPrefs.Save();
        }

        if (pc.Hp() <= 0)
        {
            enabled = false;
        }
    }
    
    void LateUpdate()
    {
        switch (state)
        {
            case State.Ready:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GameStart();
                }
                break;

            case State.Play:
                if (pc.IsStan() || timeCount < 0)//プレイヤーが倒れるorタイムアップで終了
                {
                    GameOver();
                }
                break;

            case State.GameOver:
                if (Input.GetKeyDown(KeyCode.A))
                {
                    //リロード
                    string currentSceneName = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(currentSceneName);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    //終了画面へ
                    //SceneManager.GetSceneByName("");
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    //title画面へ
                    SceneManager.GetSceneByName("MainTitle");
                    
                }
                break;
        }
    }

    void Ready()
    {
        state = State.Ready;
        pc.enabled = false;

        stateLabel.text =
            "敵を倒して進もう！\n倒した数と進んだ距離が得点になるよ。\n\nジャンプ...[Space Key]\nパンチ攻撃...[F key]\n\n敵の攻撃に３回当たる\nor\nタイムオーバーでゲーム終了\n\n[Space Key]でスタート";

    }

    void GameStart()
    {
        state = State.Play;
        pc.enabled = true;
        stateLabel.enabled = false;
    }

    void GameOver()
    {
        state = State.GameOver;
        pc.enabled = false;
        stateLabel.enabled = true;
        stateLabel.text =
            "GAME SET！\n\n倒した数 : " + pc.GetAttackCount() + "\n進んだ距離 : " + runningDistance + "m\n(HighScore : " + PlayerPrefs.GetInt("HighScoreSHIMBO") + ")\n\n[A key] 再挑戦\n[S key] タイトル画面へ\n[D key] ゲーム終了";

    }
    
}
