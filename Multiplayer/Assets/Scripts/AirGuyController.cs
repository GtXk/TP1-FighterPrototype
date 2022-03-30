using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AirGuyController : MonoBehaviourPun
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();

    }
    public void NormalAttack()
    {
        photonView.RPC("airNormalAttack", RpcTarget.Others);
    }
    [PunRPC]
    public void airNormalAttack()
    {
        anim.SetTrigger("AirGuyNormalHit");

    }
    public void MediumAttack()
    {
        photonView.RPC("airMediumAttack", RpcTarget.Others);
    }
    [PunRPC]
    public void airMediumAttack()
    {
        anim.SetTrigger("AirGuyNormalHit");

    }
    public void BigAttack()
    {
        photonView.RPC("airBigAttack", RpcTarget.Others);
    }
    [PunRPC]
    public void airBigAttack()
    {
        anim.SetTrigger("AirGuySpecialHit");

    }
    public void airHeal()
    {
        photonView.RPC("Heal", RpcTarget.Others);
    }
    [PunRPC]
    public void Heal()
    {
        anim.SetTrigger("AirGuyHealing");

    }
}