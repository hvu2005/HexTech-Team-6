using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{

    float vertical;
    [SerializeField] float climbSpeed;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(Vector2.up, ForceMode2D.Impulse);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
            if (other != null && other.CompareTag("Player"))
            {
                PlayerControl player = other.gameObject.GetComponent<PlayerControl>();
                player.Rb.gravityScale = 0f;
                vertical = Input.GetAxis("Vertical");
                if (vertical > 0f)
                {
                    //rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
                    //Instantiate()
                    player.Rb.velocity = new Vector2(player.Rb.velocity.x, climbSpeed);
                }
                else if (vertical < 0f)
                {
                    player.Rb.velocity = new Vector2(player.Rb.velocity.x, -climbSpeed);
                }
                else
                {
                    player.Rb.velocity = new Vector2(player.Rb.velocity.x, 0f);
                }
            }
            //else
            //{
            //    rb.gravityScale = defaultGravityScale;
            //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        {
            //Player player = other.gameObject.GetComponent<Player>();
            //PlayerController playerController = other.gameObject.GetComponent<PlayerController>();


            //else if (playerController.yMove < 0f)
            //{
            //    player.Rb.velocity = new Vector2(player.Rb.velocity.x, -climbSpeed);
            //}
            //else
            //{
            //    player.Rb.velocity = new Vector2(player.Rb.velocity.x, 0f);
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Player"))
            if (other != null && other.CompareTag("Player"))
            {
                //Debug.Log("CanClimb: " + canClimb);
                PlayerControl player = other.gameObject.GetComponent<PlayerControl>();
                player.Rb.gravityScale = 3f;
                player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.Rb.velocity.y);


            }
    }
}