using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SoundType
{
    BeAttacked,
    Attack,
    Die
}

public class SoundManager : MonoBehaviour
{
    public AudioClip EnemyBeAttackedSound;
    public AudioClip PlayerBeAttackedSound;

    public AudioClip EnemyAttackSound;
    public AudioClip PlayerAttackSound;

    public AudioClip EnemyDieSound;
    public AudioClip PlayerDieSound;

    public AudioClip BulletSound;
    public AudioClip BowSound;

    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void PlayerPlayAttackSound()
    {
//        _audioSource.PlayOneShot(AttackSound);
    }

    public void PlayerPlayDieSound()
    {
//        _audioSource.PlayOneShot(DieSound);
    }

    public void EnemyPlayDieSound()
    {
//        _audioSource.PlayOneShot(DieSound);
    }

    public void PlayEnemySound(SoundType soundType, EnemyType enemyType)
    {
        switch (soundType)
        {
            case SoundType.BeAttacked:
                PlayEnemyBeAttackedSound(enemyType);
                break;
            case SoundType.Attack:
                break;
            case SoundType.Die:
                break;
        }
    }

    private void PlayEnemyBeAttackedSound(EnemyType type)
    {
        //TODO: Play right audio of enemy
        switch (type)
        {
            case EnemyType.Solider:
            case EnemyType.Slug:
            default:
                _audioSource.PlayOneShot(EnemyAttackSound);
                break;
//                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public void PlayWeaponSound(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.MACHINEGUN:
                break;
            case WeaponType.BOW:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, "No weapon type");
        }
    }
}