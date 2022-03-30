using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FireGuyController : MonoBehaviourPun
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

    }
    private void Update()
    {
       // anim.SetTrigger("FireGuyNormalAttack");
    }
    // Update is called once per frame
    public void FireGuyHeal()
    {

        photonView.RPC("fireGuyHeal", RpcTarget.Others);


    }
    [PunRPC]
    public void fireGuyHeal()
    {
        anim.SetTrigger("FireGuyHeal");

    }
    public void NormalAttack()
    {
        photonView.RPC("fireGuyNormalAttack", RpcTarget.Others);
    }
    [PunRPC]
    public void fireGuyNormalAttack()
    {
        anim.SetTrigger("FireGuyNormalAttack");

    }
    public void MediumAttack()
    {

    }
    public void BigAttack()
    {
         photonView.RPC("fireGuyBigAttack", RpcTarget.Others);


    }
    [PunRPC]
    public void fireGuyBigAttack()
    {
        anim.SetTrigger("FireGuySpecial");

    }
}
