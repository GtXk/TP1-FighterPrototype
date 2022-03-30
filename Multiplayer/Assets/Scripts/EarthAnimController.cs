using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EarthAnimController : MonoBehaviourPun
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }
    public void NormalAttack()
    {
        photonView.RPC("earthNormalAttack", RpcTarget.Others);
    }
    [PunRPC]
    public void earthNormalAttack()
    {
        anim.SetTrigger("EarthGuyNormalHit");

    }
    public void MediumAttack()
    {
        photonView.RPC("earthMediumAttack", RpcTarget.Others);
    }
    [PunRPC]
    public void earthMediumAttack()
    {
        anim.SetTrigger("EarthGuyMediumHit");

    }
    public void BigAttack()
    {
        photonView.RPC("earthBigAttack", RpcTarget.Others);
    }
    [PunRPC]
    public void earthBigAttack()
    {
        anim.SetTrigger("EarthGuySpecialHit");

    }
    public void earthHeal()
    {
        photonView.RPC("Heal", RpcTarget.Others);
    }
    [PunRPC]
    public void Heal()
    {
        anim.SetTrigger("EarthGuyHealing");

    }
}

