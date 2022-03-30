using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AnimationDatabase : MonoBehaviourPun
{
    Animator anim;
 
    public FireGuyController FireAnimationcontroller;
    public WaterGuyController WaterAnimationcontroller;
    public EarthAnimController EarthAnimationcontroller;
    public AirGuyController AirGuycontroller;
    public AudioManager AudioManager;
  
    void Start()
    {
        anim = GetComponent<Animator>();
        FireAnimationcontroller = FindObjectOfType<FireGuyController>();
        WaterAnimationcontroller = FindObjectOfType<WaterGuyController>();
    }

    public void playAnim(string animname)
    {
        if (animname == "Punch")
        {
            FireAnimationcontroller = FindObjectOfType<FireGuyController>();
            FireAnimationcontroller.NormalAttack();
            AudioManager.Play("FireAttack");

        }
        if (animname == "Burn")
        {
            FireAnimationcontroller = FindObjectOfType<FireGuyController>();
            FireAnimationcontroller.NormalAttack();
            AudioManager.Play("FireAttack");
        }
        if (animname == "Fire Nova")
        {
            FireAnimationcontroller = FindObjectOfType<FireGuyController>();
            FireAnimationcontroller.BigAttack();
            AudioManager.Play("FireSpecial");
        }
        if (animname == "Fire Drain")
        {
            FireAnimationcontroller = FindObjectOfType<FireGuyController>();
            FireAnimationcontroller.FireGuyHeal();
            AudioManager.Play("Heal");
        }

        if (animname == "Kick")
        {
            WaterAnimationcontroller = FindObjectOfType<WaterGuyController>();
            WaterAnimationcontroller.NormalAttack();
            AudioManager.Play("WaterSmall");
        }
        if (animname == "Water Whip")
        {
            WaterAnimationcontroller = FindObjectOfType<WaterGuyController>();
            WaterAnimationcontroller.MediumAttack();
            AudioManager.Play("WaterAttackBig");
        }
        if (animname == "Tsunami")
        {
            WaterAnimationcontroller = FindObjectOfType<WaterGuyController>();
            WaterAnimationcontroller.BigAttack();
            AudioManager.Play("WaterAttackBig");
        }
        if (animname == "Water Shield")
        {
            WaterAnimationcontroller = FindObjectOfType<WaterGuyController>();
            WaterAnimationcontroller.waterHeal();
            AudioManager.Play("Heal");
        }
        if (animname == "Dropkick")
        {
            EarthAnimationcontroller = FindObjectOfType<EarthAnimController>();
            EarthAnimationcontroller.NormalAttack();
            AudioManager.Play("EarthAttack");
        }
        if (animname == "Rock Blast")
        {
            EarthAnimationcontroller = FindObjectOfType<EarthAnimController>();
            EarthAnimationcontroller.MediumAttack();
            AudioManager.Play("EarthAttack");
        }
        if (animname == "Earthquake")
        {
            EarthAnimationcontroller = FindObjectOfType<EarthAnimController>();
            EarthAnimationcontroller.BigAttack();
            AudioManager.Play("EarthAttack");
        }
        if (animname == "Rock Armour")
        {
            EarthAnimationcontroller = FindObjectOfType<EarthAnimController>();
            EarthAnimationcontroller.earthHeal();
            AudioManager.Play("Heal");
        }
        if (animname == "Headbutt")
        {
            AirGuycontroller = FindObjectOfType<AirGuyController>();
            AirGuycontroller.NormalAttack();
            AudioManager.Play("WindSmall");
        }
        if (animname == "Areial Assault")
        {
            AirGuycontroller = FindObjectOfType<AirGuyController>();
            AirGuycontroller.MediumAttack();
            AudioManager.Play("WindSmall");
        }
        if (animname == "Tornado")
        {
            AirGuycontroller = FindObjectOfType<AirGuyController>();
            AirGuycontroller.BigAttack();
            AudioManager.Play("WindBig");
        }
        if (animname == "Air Wall")
        {
            AirGuycontroller = FindObjectOfType<AirGuyController>();
            AirGuycontroller.airHeal();
            AudioManager.Play("Heal");
        }




    }




}
