using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject prefabPlayer;
    public GameObject prefabEnemy;
    public GameObject prefabBasePlatform;

    private PlatformManager pm;
    private InputHandler ih;
    private CollisionHandler ch;
    private EnemyManager em;

    private GameObject madePlayer;

    private GameObject endMenu;
    private GameObject startMenu;

    private void Awake()
    {
        pm = new PlatformManager();
        ih = new InputHandler();
        em = new EnemyManager();

        endMenu = GameObject.Find("EndMenu");
        startMenu = GameObject.Find("StartMenu");

        Time.timeScale = 0;
    }

    private void Start()
    {
        endMenu.SetActive(false);

        madePlayer = null;

        Vector2 initSpawn = new Vector2(0, -5.5f);

        for (int i = 0; i < pm.platformCount; i++)
        {
            pm.GeneratePlatforms(i, ref initSpawn, Instantiate(prefabBasePlatform));

            if (i == 0)
            {
                madePlayer = Instantiate(prefabPlayer, new Vector2(initSpawn.x, initSpawn.y + 0.75f), Quaternion.identity);
                ih.Start(madePlayer);
            }
        }

        ch = new CollisionHandler(pm, madePlayer.GetComponent<Rigidbody2D>());

        em.CreateEnemy(Instantiate(prefabEnemy, new Vector3(3, 3, 0), Quaternion.identity), madePlayer.transform);
    }

    private void Update()
    {
        ih.HandleInput();

        if (madePlayer.transform.position.x > 8.5f)
        {
            madePlayer.transform.position = new Vector2(-8.5f, madePlayer.transform.position.y);
        }
        else if (madePlayer.transform.position.x < -8.5f)
        {
            madePlayer.transform.position = new Vector2(8.5f, madePlayer.transform.position.y);
        }
    }

    private void FixedUpdate()
    {
        pm.UpdatePlatforms();
        ih.UpdatePlayerPos();
        ch.OnJumpDetect();
        if (em.UpdateEnemies())
        {
            GameOver();
        }
    }

    private void LateUpdate()
    {
        if (madePlayer.transform.position.y > Camera.main.transform.position.y)
        {
            Vector3 newPosition = new Vector3(Camera.main.transform.position.x, madePlayer.transform.position.y, Camera.main.transform.position.z);
            Camera.main.transform.position = newPosition;
        }

        if (madePlayer.transform.position.y - Camera.main.transform.position.y < -5.5f)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        endMenu.SetActive(true);
        GameObject.Find("Score").GetComponent<Text>().text = "Score: " + Camera.main.transform.position.y;
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        startMenu.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
