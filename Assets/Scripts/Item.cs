using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    COIN,
    MED_KIT
}

[RequireComponent(typeof(AudioSource))]
public class Item : MonoBehaviour
{
    private Animator animator;

    public AudioClip impact;
    AudioSource audioSource;

    const string PICK_UP = "PickUp";

    public float HitAniDelayTime = 0.1f;
    public float VolumeScale = 0.7f;

    private ItemType itemType;

    public void InitState(ItemType type)
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        itemType = type;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayImpactSound();
        OnImpactType();
        StartCoroutine(PlayPickUpAniThenDetroy());
    }

    private void OnImpactType()
    {
        // TODO: impact on player
        switch (itemType)
        {
            case ItemType.COIN:
                break;
            case ItemType.MED_KIT:
                break;
            default:
                break;
        }
    }

    IEnumerator PlayPickUpAniThenDetroy()
    {
        animator.SetTrigger(PICK_UP);
        AnimatorStateInfo currInfo = animator.GetCurrentAnimatorStateInfo(0);

        yield return new WaitForSeconds(currInfo.length + HitAniDelayTime);
        Destroy(gameObject);
    }

    public void PlayImpactSound()
    {
        audioSource.PlayOneShot(impact, VolumeScale);
    }


}
