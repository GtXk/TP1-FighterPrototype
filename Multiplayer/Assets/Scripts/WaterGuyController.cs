using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaterGuyController : MonoBehaviourPun
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

    }
    
    public void NormalAttack()
    {
        photonView.RPC("waterNormalAttack", RpcTarget.Others);
    }
    [PunRPC]
    public void waterNormalAttack()
    {
        anim.SetTrigger("WaterGuyNormalHit");

    }
    public void MediumAttack()
    {
        photonView.RPC("waterMediumAttack", RpcTarget.Others);
    }
    [PunRPC]
    public void waterMediumAttack()
    {
        anim.SetTrigger("WaterGuyMediumHit");

    }
    public void BigAttack()
    {
        photonView.RPC("waterBigAttack", RpcTarget.Others);
    }
    [PunRPC]
    public void waterBigAttack()
    {
        anim.SetTrigger("WaterGuySpecialHit");

    }
    public void waterHeal()
    {
        photonView.RPC("Heal", RpcTarget.Others);
    }
    [PunRPC]
    public void Heal()
    {
        anim.SetTrigger("WaterGuyHealing");

    }

}
