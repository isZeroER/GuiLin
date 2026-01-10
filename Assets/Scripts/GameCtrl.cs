using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCtrl : UnitySingleton<GameCtrl>
{
    public static float volume;
    private static float theGameTime = 30;
    private static bool volumeIsOn = true;
    public static int nanDu = 0; // 0是容易，1是一般，2是困难

    public MeshFilter planeMesh;
    public Transform planeTransform;
    [HideInInspector] public float maxPosX, minPosX, maxPosZ, minPosZ;
    public Transform ballparent;
    bool canEat = false;
    int score = 0;
    int ballNum = 10;
    int remainingBallNum = 10;
    float gameTime = 30;
    float remainingGameTime = 30;
    bool isGameOn = true;
    [Header("结算界面")]
    public GameObject panelWin, panelLose;

    [Header("游戏中UI")]
    public Text nanduText;
    public Text timerText;
    public Text scoreText;
    public Text[] resultScoreTexts;
    [Header("移动")]
    private Rigidbody rb;
    public float moveSpeed = 10f;
    Vector3 inputDir;

    [Header("游戏内控制")]
    public Color makeColor;
    private AudioSource audioSource;
    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.volume = volume;
        panelWin.SetActive(false);
        panelLose.SetActive(false);

        if (nanDu == 0)
        {
            theGameTime = 60;
            nanduText.text = "难度：容易";
            planeTransform.localScale = new Vector3(1, 1, 1);
        }

        if (nanDu == 1)
        {
            theGameTime = 45;
            nanduText.text = "难度：一般";
            planeTransform.localScale = new Vector3(5, 5, 5);
        }

        if (nanDu == 2)
        {
            theGameTime = 30;
            nanduText.text = "难度：困难";
            planeTransform.localScale = new Vector3(5, 5, 5);
        }
        remainingGameTime = theGameTime; // 获取设置的难度
        InitRange(planeTransform);
        InitBalls(ballNum);
    }

    void Update()
    {
        if (!isGameOn) return;

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        inputDir = new Vector3(h, 0, v);

        if (inputDir.sqrMagnitude > 0.01f)
        {
            canEat = true;
        }

        remainingGameTime -= Time.deltaTime;

        if (remainingBallNum == 0 || remainingGameTime <= 0)
        {
            GameOver();
        }

        timerText.text = "剩余时间：" + remainingGameTime.ToString("0");
        scoreText.text = "得分： " + score.ToString();
    }
    void FixedUpdate()
    {
        if (!isGameOn) return;

        var deltaMove = moveSpeed * Time.fixedDeltaTime * inputDir.normalized;
        Vector3 targetPos = rb.position + deltaMove;
        rb.MovePosition(targetPos);
    }

    
    void InitBalls(int num)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject tempBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            float tempPosX = Random.Range(minPosX, maxPosX);
            float tempPosZ = Random.Range(minPosZ, maxPosZ);
            Vector3 tempPosition = new Vector3(tempPosX, -0.2f, tempPosZ);
            Quaternion tempRotation = Random.rotation;
            float tempScaleNum = Random.Range(0.6f, 0.7f);
            tempBall.transform.position = tempPosition;
            tempBall.transform.rotation = tempRotation;
            tempBall.transform.localScale = new Vector3(tempScaleNum, tempScaleNum, tempScaleNum);
            tempBall.transform.parent = ballparent;
            tempBall.GetComponent<MeshRenderer>().material.color = makeColor;
            tempBall.tag = "balls";
            tempBall.AddComponent<Rigidbody>();
            tempBall.GetComponent<Collider>().isTrigger = true;
            tempBall.GetComponent<Rigidbody>().isKinematic = true;
            tempBall.AddComponent<BallTrigger>();
        }
    }
    public void InitRange(Transform targetTransform)
    {
        Vector3[] vertices = planeMesh.mesh.vertices;

        Vector3 min = vertices[0];
        Vector3 max = vertices[0];

        for (int i = 1; i < vertices.Length; i++)
        {
            min = Vector3.Min(min, vertices[i]);
            max = Vector3.Max(max, vertices[i]);
        }

        // 转为世界坐标
        Vector3 worldMin = targetTransform.TransformPoint(min);
        Vector3 worldMax = targetTransform.TransformPoint(max);

        minPosX = Mathf.Min(worldMin.x, worldMax.x);
        maxPosX = Mathf.Max(worldMin.x, worldMax.x);
        minPosZ = Mathf.Min(worldMin.z, worldMax.z);
        maxPosZ = Mathf.Max(worldMin.z, worldMax.z);
    }

    void GameOver()
    {
        isGameOn = false;
        JudgeResult();
    }

    void JudgeResult()
    {
        if (remainingBallNum == 0)
        {
            panelWin.SetActive(true);
        }
        else
        {
            panelLose.SetActive(true);
        }

        foreach (var resultScoreText in resultScoreTexts)
        {
            resultScoreText.text = "最终得分："+score.ToString("0");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("balls") && canEat)
        {
            Destroy(other.gameObject);
            score += 10;
            remainingBallNum--;
            transform.localScale *= 1.15f;
        }
    }
}