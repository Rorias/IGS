using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject prefabPlayer;

    public GameObject prefabBasePlatform;

    private PlatformManager pm;
    private InputHandler ih;
    private CollisionHandler ch;

    private GameObject madePlayer;

    private GameObject endMenu;
    private GameObject startMenu;

    private void Awake()
    {
        pm = new PlatformManager();
        ih = new InputHandler();

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
    }

    private void Update()
    {
        ih.HandleInput();
    }

    private void FixedUpdate()
    {
        pm.UpdatePlatforms();
        ih.FixedUpdate();
        ch.OnJumpDetect();
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
            Time.timeScale = 0;
            endMenu.SetActive(true);
            GameObject.Find("Score").GetComponent<Text>().text = "Score: " + Camera.main.transform.position.y;
        }
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
