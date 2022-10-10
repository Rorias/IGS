using System.Collections.Generic;
using UnityEngine;

public class PlatformManager
{
    public List<GameObject> platformObjectPool = new List<GameObject>();
    public List<Platform> platformPool = new List<Platform>();
    public int platformCount { get; private set; } = 12;

    public void GeneratePlatforms(int _count, ref Vector2 _pos, GameObject _platform)
    {
        GameObject platform = _platform;
        platform.name = "BP" + _count;
        platformObjectPool.Add(platform);
        CreatePlatform(platform, ref _pos, 0);
    }

    public GameObject RequestPlatform(Vector2 _pos)
    {
        for (int i = 0; i < platformObjectPool.Count; i++)
        {
            if (!platformObjectPool[i].activeInHierarchy)
            {
                CreatePlatform(platformObjectPool[i], ref _pos, i);
                platformObjectPool[i].SetActive(true);
                return platformObjectPool[i];
            }
        }

        return null;
    }

    public void UpdatePlatforms()
    {
        for (int i = 0; i < platformObjectPool.Count; i++)
        {
            if (platformObjectPool[i].transform.position.y - Camera.main.transform.position.y < -5.5f)
            {
                ReplacePlatform(i);
                break;
            }

            if (platformPool[i].jumpCount <= 0)
            {
                ReplacePlatform(i);
                break;
            }
        }
    }

    private void CreatePlatform(GameObject _gamePlatform, ref Vector2 _pos, int _item)
    {
        SetGamePlatformPosition(_gamePlatform, ref _pos);
        Platform platform = CreateNewMemPlatform(_pos);
        _gamePlatform.GetComponent<SpriteRenderer>().color = new Color(
                                                        platform.color.r > 1 ? platform.color.r / 3 : platform.color.r,
                                                        platform.color.g > 1 ? platform.color.g / 3 : platform.color.g,
                                                        platform.color.b > 1 ? platform.color.b / 3 : platform.color.b, 1);

        if (platformPool.Count < platformObjectPool.Count)
        {
            platformPool.Add(platform);
        }
        else
        {
            platformPool[_item] = platform;
        }
    }

    private Platform CreateNewMemPlatform(Vector2 _pos)
    {
        IPlatform platform = new Platform(500, 100, _pos, Color.black);

        for (int i = 0; i < 2; i++)
        {
            int rnd = Random.Range(0, 9);

            if ((platform.platformTypes == IPlatform.PlatformType.broken && rnd == 6) ||
                (platform.platformTypes == IPlatform.PlatformType.brittle && (rnd == 4 || rnd == 5)) ||
                (platform.platformTypes == IPlatform.PlatformType.highBounce && (rnd == 2 || rnd == 3)) ||
                (platform.platformTypes == IPlatform.PlatformType.spring && (rnd == 7 || rnd == 8)))
            {
                rnd = 0;
            }

            if ((platform.platformTypes == IPlatform.PlatformType.broken || platform.platformTypes == IPlatform.PlatformType.brittle) && rnd >= 4 && rnd <= 6)
            {
                rnd = 2;
            }

            switch (rnd)
            {
                case 2:
                case 3:
                    HighBounceDec hBounce = new HighBounceDec(250, 0);
                    platform = hBounce.Decorate(platform);
                    platform.color += new Color(1, 1, 0, 0);
                    break;
                case 4:
                case 5:
                    BrittleDec bDec = new BrittleDec(0, 2);
                    platform = bDec.Decorate(platform);
                    platform.color += new Color(1, 0, 0, 0);
                    break;
                case 6:
                    BrokenDec brDec = new BrokenDec(0, 1);
                    platform = brDec.Decorate(platform);
                    platform.color += new Color(0, 1, 0, 0);
                    break;
                case 7:
                case 8:
                    SpringDec sBounce = new SpringDec(800, 0);
                    platform = sBounce.Decorate(platform);
                    platform.color += new Color(0, 0, 1, 0);
                    break;
                case 0:
                case 1:
                default:
                    platform.color += new Color(0, 0, 0, 0);
                    break;
            }
        }

        return (Platform)platform;
    }

    private void SetGamePlatformPosition(GameObject _platform, ref Vector2 _pos)
    {
        _pos.x = Mathf.Max(Mathf.Min(_pos.x + Random.Range(-6, 7), 8f), -8f);
        _pos.y += Random.Range(1f, 3f);
        _platform.transform.position = new Vector2(_pos.x, _pos.y);
    }

    private void ReplacePlatform(int _item)
    {
        platformObjectPool[_item].SetActive(false);

        //always pick the highest platform in the list to place a new one from
        float highestY = float.NegativeInfinity;
        GameObject highestPlatform = null;

        for (int j = 0; j < platformObjectPool.Count; j++)
        {
            if (platformObjectPool[j].transform.position.y > highestY)
            {
                highestY = platformObjectPool[j].transform.position.y;
                highestPlatform = platformObjectPool[j];
            }
        }

        RequestPlatform(highestPlatform.transform.position);
    }
}
