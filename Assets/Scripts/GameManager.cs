using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

static class GenConstants
{
    public const float X_POS = 0.0f;
    public const float Y_POS = 3.5f;
    public const float INTERVAL = 2.0f;
}

public class GameManager : MonoBehaviour
{
    // 他のクラスから使用するGameManagerインスタンス
    public static GameManager Instance { get; private set; }
    // 次のボールを生成可能かどうか
    // Ball.Drop()でtrueにする
    public bool isNext { get; set; }
    // ボールの種類数
    public int ballsLength { get; private set; }

    // ボール全種類の配列
    [SerializeField] private GameObject[] Balls;
    // スコアを表示するUIテキスト
    [SerializeField] private Text scoreTxt;
    // ゲームオーバーパネル
    [SerializeField] private GameObject GameOverPanel;
    // ゲームオーバー時のスコアを表示するUIテキスト
    [SerializeField] private Text gameOverScoreTxt;

    // スコア
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        isNext = false;
        ballsLength = Balls.Length;
        score = 0;
        SetScore();
        GenerateBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNext)
        {
            isNext = false;
            Invoke("GenerateBall", GenConstants.INTERVAL);
        }
    }

    // 落とすボールをランダムに生成する
    private void GenerateBall()
    {
        int index = Random.Range(0, ballsLength - 2);
        Ball ballInstance = Instantiate(Balls[index],
            new Vector2(GenConstants.X_POS, GenConstants.Y_POS),
            Quaternion.identity).GetComponent<Ball>();
        ballInstance.id = index;
    }

    // ボールを合体する（ひとつ大きいボールを生成する）
    public void MergeBalls(Vector3 genPos, int parentId)
    {
        // 一番大きいボールだったら何も生成しない
        if (parentId == ballsLength - 1)
        {
            return;
        }
        Ball newBall = Instantiate(Balls[parentId + 1], genPos,
            Quaternion.identity).GetComponent<Ball>();
        newBall.id = parentId + 1;
        newBall.isDropping = true;
        newBall.GetComponent<Rigidbody2D>().simulated = true;
        CalcScore(parentId);
    }

    // スコアを計算する
    private void CalcScore(int parentId)
    {
        score += (int)Mathf.Pow(2, parentId);
        SetScore();
    }

    // スコアをUIテキストにセットする
    private void SetScore()
    {
        scoreTxt.text = score.ToString();
    }

    // ゲームオーバー処理
    public void GameOver()
    {
        gameOverScoreTxt.text = "SCORE: " + score.ToString();
        GameOverPanel.SetActive(true);
    }
}
