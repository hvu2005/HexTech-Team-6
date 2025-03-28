using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    float defaultGravityScale;
    [SerializeField] float climbSpeed;

    private PlayerController _playerController;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Climb();
        //rb.AddForce(Vector2.up, ForceMode2D.Impulse);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            player.Rb.gravityScale = 3f;
            player.Rb.velocity = new Vector2(player.Rb.velocity.x, player.Rb.velocity.y);


        }
    }
}
