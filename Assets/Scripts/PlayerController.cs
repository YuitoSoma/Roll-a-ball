using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;                    // プレイヤーの速さ
    [SerializeField] public TextMeshProUGUI scoreText;      // スコアのテキスト
    [SerializeField] public GameObject GameOverPanel;       // ゲームオーバ時に表示するUI
    [SerializeField] public GameObject ClearPanel;          // クリア時に表示するUI
    [SerializeField] public GameObject FinishButton;
    [SerializeField] public GameObject RetryButton;
    [SerializeField] public DataManager dataManager;        // データ管理のクラス
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    ParticleSystem particle;
    Rigidbody rb;                                   // rigidbody
    Vector3 movement;                               // 進行方向
    int score;                                      // スコア
    int attackwall;                                  // 壁に当たった回数
    AudioSource pickupsound;                                // AudioSource
    Color color;                                            // オブジェクトの色

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();                     // rigidbodyの取得
        movement = new Vector3(0.0f, 0.0f, 0.0f);           // 進行方向の初期化
        score = 0;                                          // scoreの初期化
        scoreText.text = "Score:" + score.ToString();       // 表示するスコアの更新
        ClearPanel.SetActive(false);                        // クリア時に表示するUIを非表示にする
        FinishButton.SetActive(false);                      // ボタン表示
        RetryButton.SetActive(false);                       // ボタン表示
        GameOverPanel.SetActive(false);                      // ゲームオーバ時に表示するUIを表示する
        pickupsound = GetComponent<AudioSource>();          // AudioSourceの取得
        pickupsound = GetComponent<AudioSource>();          // AudioSourceの取得
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(movement*speed);                                        // プレイヤーに力を加える(物理演算)
    }

    void OnMove(InputValue value)
    {
        Vector2 moveDirection = value.Get<Vector2>();                       // キー入力の方向を取得
        movement = new Vector3(moveDirection.x, 0.0f, moveDirection.y);     // 3次元に変換
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            attackwall += 1;
            Debug.Log("当たった回数：" + attackwall);

            if (attackwall == 8)
            {
                GameOverPanel.SetActive(true);                                 // ゲームオーバ時に表示するUIを表示する
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("PickUp"))                           // PickUpタグに接触したとき
        {
            Destroy(other.gameObject);                                                      // オブジェクトを消去する
            pickupsound.Play();                                                             // サウンド再生
            this.gameObject.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV();    // プレイヤーの色を変える
            color = this.gameObject.GetComponent<Renderer>().material.color;


            ParticleSystem newParticle = Instantiate(particle);                             // パーティクルシステムのインスタンスを生成する。
            newParticle.transform.position = this.transform.position;                       // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
            newParticle.Play();                                                             // パーティクルを発生させる。

            // インスタンス化したパーティクルシステムのGameObjectを削除する。(任意)
            // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
            Destroy(newParticle.gameObject, 3.0f);

            score += 100;                                                   // スコアに100ポイント加点する
            scoreText.text = "Score:" + score.ToString();                   // 表示するスコアの更新
            if(score >= 1200)
            {
                
                ClearPanel.SetActive(true);                                 // クリア時に表示するUIを表示する
                FinishButton.SetActive(true);                               // ボタン表示
                RetryButton.SetActive(true);                                // ボタン表示
            }

            dataManager.UpdateData(score.ToString() + "," + DateTime.Now.ToString("yyyyMMddHHmmss") + "," +color.ToString()); // csvファイルに書き込むデータを追加
        }
    }
}
