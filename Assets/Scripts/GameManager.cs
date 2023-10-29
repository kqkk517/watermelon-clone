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
    public static GameManager Instance { get; private set; }
    // DropBall.Drop()でtrueにする
    public bool isNext { get; set; }

    [SerializeField] private GameObject Ball;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
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

    private void GenerateBall()
    {
        Instantiate(Ball, new Vector2(GenConstants.X_POS, GenConstants.Y_POS), Quaternion.identity);
    }
}
