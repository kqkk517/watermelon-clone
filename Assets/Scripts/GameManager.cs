using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private GameObject[] Balls;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        ballsLength = Balls.Length;
        isNext = false;
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

    // ボールを合体する
    public void MergeBalls(Vector3 genPos, int parentId)
    {
        if (parentId == ballsLength - 1)
        {
            return;
        }
        Ball newBall = Instantiate(Balls[parentId + 1], genPos,
            Quaternion.identity).GetComponent<Ball>();
        newBall.id = parentId + 1;
        newBall.isDropping = true;
        newBall.GetComponent<Rigidbody2D>().simulated = true;
    }
}
