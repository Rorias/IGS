using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefabBasePlatform;

    private PlatformManager pm;

    private void Awake()
    {
        pm = new PlatformManager();
    }

    private void Start()
    {
        Vector2 initSpawn = new Vector2(0, -5.5f);

        for (int i = 0; i < pm.platformCount; i++)
        {
            pm.GeneratePlatforms(i, ref initSpawn, Instantiate(prefabBasePlatform));
        }
    }

    private void FixedUpdate()
    {
        pm.UpdatePlatforms();
    }
}
