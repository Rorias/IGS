using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

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
        ih.Start(player);

        Vector2 initSpawn = new Vector2(0, -5.5f);

        for (int i = 0; i < pm.platformCount; i++)
        {
            pm.GeneratePlatforms(i, ref initSpawn, Instantiate(prefabBasePlatform));
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
