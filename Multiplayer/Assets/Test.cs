
using UnityEngine;
using Photon.Pun;

public class Test : MonoBehaviour
{
    public float MovementSpeed = 2;
   // public float jumpSpeed = 5;
   // private Rigidbody2D rb;
    PhotonView view;
    private void Start()
    {
        view = GetComponent<PhotonView>();
       // rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {


        if (view.IsMine)
        {

            var movement = Input.GetAxis("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        }
    }
    //public void jumpButton()
    //{
    //    if (view.IsMine)
    //    {
    //        if (rb.velocity.y == 0)
    //        {
    //            rb.velocity = Vector2.up * jumpSpeed;
    //        }
    //    }
    //}
}
