using System.Collections;
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
        SetPlatformPosition(platform, ref _pos);
        Platform linkedPlatform = CreatePlatform(_pos);
        platform.GetComponent<SpriteRenderer>().color = new Color(
                                                        linkedPlatform.color.r > 1 ? linkedPlatform.color.r / 3 : linkedPlatform.color.r,
                                                        linkedPlatform.color.g > 1 ? linkedPlatform.color.g / 3 : linkedPlatform.color.g,
                                                        linkedPlatform.color.b > 1 ? linkedPlatform.color.b / 3 : linkedPlatform.color.b, 1);
        Debug.Log(linkedPlatform.bounceHeight + ", " + linkedPlatform.jumpCount);
        platformPool.Add(linkedPlatform);
        platformObjectPool.Add(platform);
    }

    private Platform CreatePlatform(Vector2 _pos)
    {
        IPlatform platform = new Platform(20, 100, _pos, Color.black);
        int prevRnd = -1;

        for (int i = 0; i < 2; i++)
        {
            int rnd = Random.Range(0, 5);

            if (rnd == prevRnd)
            {
                rnd = 0;
            }

            prevRnd = rnd;

            switch (rnd)
            {
                case 1:
                    HighBounceDec hBounce = new HighBounceDec(20, 0);
                    platform = hBounce.Decorate(platform);
                    platform.color += new Color(1, 1, 0, 0);
                    break;
                case 2:
                    BrittleDec bDec = new BrittleDec(0, 2);
                    platform = bDec.Decorate(platform);
                    platform.color += new Color(1, 0, 0, 0);
                    break;
                case 3:
                    BrokenDec brDec = new BrokenDec(0, 1);
                    platform = brDec.Decorate(platform);
                    platform.color += new Color(0, 1, 0, 0);
                    break;
                case 4:
                    SpringDec sBounce = new SpringDec(100, 0);
                    platform = sBounce.Decorate(platform);
                    platform.color += new Color(0, 0, 1, 0);
                    break;
                case 0:
                default:
                    platform.color += new Color(0, 0, 0, 0);
                    break;
            }
        }

        return (Platform)platform;
    }

    public GameObject RequestPlatform(Vector2 _pos)
    {
        for (int i = 0; i < platformObjectPool.Count; i++)
        {
            if (!platformObjectPool[i].activeInHierarchy)
            {
                SetPlatformPosition(platformObjectPool[i], ref _pos);
                Platform newPlatform = CreatePlatform(_pos);
                platformObjectPool[i].GetComponent<SpriteRenderer>().color = new Color(
                                                                             newPlatform.color.r > 1 ? newPlatform.color.r / 3 : newPlatform.color.r,
                                                                             newPlatform.color.g > 1 ? newPlatform.color.g / 3 : newPlatform.color.g,
                                                                             newPlatform.color.b > 1 ? newPlatform.color.b / 3 : newPlatform.color.b, 1);
                Debug.Log(newPlatform.bounceHeight + ", " + newPlatform.jumpCount);
                platformPool[i] = newPlatform;
                platformObjectPool[i].SetActive(true);
                return platformObjectPool[i];
            }
        }

        return null;

        ////if platformpool is already used up but player needs new platform anyway
        //GameObject newPlatform = Instantiate(prefabBasePlatform);
        //platformPool.Add(newPlatform);
        //newPlatform.name = "BP" + platformPool.Count;

        //return newPlatform;
    }

    private void SetPlatformPosition(GameObject _platform, ref Vector2 _pos)
    {
        _pos.x = Mathf.Max(Mathf.Min(_pos.x + Random.Range(-6, 7), 8f), -8f);
        _pos.y += Random.Range(1f, 3.5f);
        _platform.transform.position = new Vector2(_pos.x, _pos.y);
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
