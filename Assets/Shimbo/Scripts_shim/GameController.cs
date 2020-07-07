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
        
        //スコア更新(倒したEnemy数がスコア)
        scoreLabel.text = "Score : " + pc.GetAttackCount();

        //HP更新
        hpPanel.UpdateHp(pc.Hp());

        //ハイスコア
        if (PlayerPrefs.GetInt("HighScore") < pc.GetAttackCount())
        {
            PlayerPrefs.SetInt("HighScore", pc.GetAttackCount());
            PlayerPrefs.Save();

            Debug.Log(PlayerPrefs.GetInt("HighScore"));
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
                    //シーン移動
                    //SceneManager.LoadScene();
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    //title画面へ
                    //SceneManager.LoadScene(0);
                }
                break;
        }
    }

    void Ready()
    {
        state = State.Ready;
        pc.enabled = false;

        stateLabel.text =
            "敵を倒して進もう！\n\nジャンプ・・・[Space Key]\nパンチ攻撃・・・[F key]\n\n敵の攻撃に３回当たる\nor\nタイムオーバーでゲーム終了\n\n[Space Key]でスタート";

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
        stateLabel.text = "GAME SET！\n\nScore : " + pc.GetAttackCount() + "\n(HighScore : " + PlayerPrefs.GetInt("HighScore") + ")\n\n[A key] 再挑戦\n[S key] タイトル画面へ\n[D key] ゲーム終了";

    }
    
}
