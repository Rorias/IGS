using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefabPlayer;

    public GameObject prefabBasePlatform;

    private PlatformManager pm;
    private InputHandler ih;

    private void Awake()
    {
        pm = new PlatformManager();
        ih = new InputHandler();
    }

    private void Start()
    {
        Vector2 initSpawn = new Vector2(0, -5.5f);

        for (int i = 0; i < pm.platformCount; i++)
        {
            pm.GeneratePlatforms(i, ref initSpawn, Instantiate(prefabBasePlatform));

            if (i == 0)
            {
                ih.Start(Instantiate(prefabPlayer, new Vector2(initSpawn.x, initSpawn.y + 0.5f), Quaternion.identity));
            }
        }
    }

    private void Update()
    {
        ih.HandleInput();
    }

    private void FixedUpdate()
    {
        pm.UpdatePlatforms();
        ih.FixedUpdate();

    }
}
