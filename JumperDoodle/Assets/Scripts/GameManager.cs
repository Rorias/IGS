using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefabPlayer;

    public GameObject prefabBasePlatform;

    private GameObject madePlayer;

    private PlatformManager pm;
    private InputHandler ih;
    private CollisionHandler ch;

    private void Awake()
    {
        pm = new PlatformManager();
        ih = new InputHandler();
    }

    private void Start()
    {
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
    }
}
