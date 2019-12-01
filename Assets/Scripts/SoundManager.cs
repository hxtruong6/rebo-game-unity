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

    private AudioSource _playerAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        _playerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlayEnemySound(AudioSource audioSource, SoundType soundType, EnemyType enemyType)
    {
        switch (soundType)
        {
            case SoundType.BeAttacked:
                PlayEnemyBeAttackedSound(audioSource, enemyType);
                break;
            case SoundType.Attack:
                PlayEnemyAttackSound(audioSource, enemyType);
                break;
            case SoundType.Die:
                PlayEnemyDieSound(audioSource, enemyType);
                break;
        }
    }

    public void PlayPlayerSound(SoundType type)
    {
        switch (type)
        {
            case SoundType.BeAttacked:
                _playerAudioSource.PlayOneShot(PlayerBeAttackedSound);
                break;
            case SoundType.Attack:
                _playerAudioSource.PlayOneShot(PlayerAttackSound);
                break;
            case SoundType.Die:
                _playerAudioSource.PlayOneShot(PlayerDieSound);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    private void PlayEnemyDieSound(AudioSource audioSource, EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Solider:
            case EnemyType.Slug:
            default:
                audioSource.PlayOneShot(EnemyDieSound);
                break;
        }
    }

    private void PlayEnemyAttackSound(AudioSource audioSource, EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Solider:
            case EnemyType.Slug:
            default:
                audioSource.PlayOneShot(EnemyAttackSound);
                break;
        }
    }

    private void PlayEnemyBeAttackedSound(AudioSource audioSource, EnemyType type)
    {
        //TODO: Play right audio of enemy
        switch (type)
        {
            case EnemyType.Solider:
            case EnemyType.Slug:
            default:
                audioSource.PlayOneShot(EnemyBeAttackedSound);
                break;
//                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    // TODO: Audio source from weapon or bullet?
    public void PlayWeaponSound(AudioSource audioSource, WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Machinegun:
                audioSource.PlayOneShot(BulletSound);
                break;
            case WeaponType.Bow:
                audioSource.PlayOneShot(BowSound);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, "No weapon type");
        }
    }
}