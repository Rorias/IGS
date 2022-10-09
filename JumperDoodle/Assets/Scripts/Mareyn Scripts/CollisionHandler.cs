using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler
{
    private PlatformManager pm;
    private Rigidbody2D player;

    public CollisionHandler(PlatformManager _pm, Rigidbody2D _player)
    {
        pm = _pm;
        player = _player;
    }

    public void OnJumpDetect()
    {
        if (player.velocity.y < 0)
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();

            Physics2D.Linecast(new Vector2(player.transform.position.x, player.transform.position.y - (player.GetComponent<BoxCollider2D>().size.y / 2) - 0.01f), new Vector2(player.transform.position.x, player.transform.position.y - (player.GetComponent<BoxCollider2D>().size.y / 2) - 0.2f), new ContactFilter2D().NoFilter(), hits);
            Debug.DrawLine(new Vector2(player.transform.position.x, player.transform.position.y - (player.GetComponent<BoxCollider2D>().size.y / 2) - 0.01f), new Vector2(player.transform.position.x, player.transform.position.y - (player.GetComponent<BoxCollider2D>().size.y / 2) - 0.2f), Color.red);

            for (int i = 0; i < hits.Count; i++)
            {
                for (int j = 0; j < pm.platformObjectPool.Count; j++)
                {
                    if (hits[i].collider.gameObject == pm.platformObjectPool[j])
                    {
                        player.velocity = new Vector2(player.velocity.x, 0);
                        player.AddForce(new Vector2(0, pm.platformPool[j].Touched()));
                        Debug.Log(player.velocity.y);
                        break;
                    }
                }
            }
        }
    }

}
