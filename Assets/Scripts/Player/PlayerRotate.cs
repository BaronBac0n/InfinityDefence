using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRotate : MonoBehaviourPunCallbacks, IPunObservable
{
    private float rotationY = 0f;
    private float rotationX = 0f;
    public float speed;

    AudioSource aud;

    public PhotonView pV;

    PlayerManager playerManager;

    #region Singleton
    public static PlayerRotate instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerRotate found");
            return;
        }
        instance = this;
    }
    #endregion

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name != " SampleScene")
        {
            pV = GetComponent<PhotonView>();
            if (!pV.IsMine)
            {
                Destroy(GetComponentInChildren<Camera>().gameObject);
            }
        }
    }

    public void FixedUpdate()
    {
        if (!pV.IsMine)
            return;
        ProcessInputs();

        if (Input.GetKey(KeyCode.W) == false && Input.GetKey(KeyCode.A) == false && Input.GetKey(KeyCode.S) == false && Input.GetKey(KeyCode.D) == false)
        {
            aud.Stop();
        }
    }

    private void ProcessInputs()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rotationY += speed * Time.deltaTime;

            if (!aud.isPlaying)
            {
                aud.Play();
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotationY -= speed * Time.deltaTime;
            if (!aud.isPlaying)
            {
                aud.Play();
            }
        }

        rotationY = Mathf.Clamp(rotationY, -90, 90);
        var rotY = transform.localEulerAngles;
        rotY.y = rotationY;
        transform.localEulerAngles = -rotY;

        if (Input.GetKey(KeyCode.W))
        {
            rotationX += speed * Time.deltaTime;
            if (!aud.isPlaying)
            {
                aud.Play();
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            rotationX -= speed * Time.deltaTime;
            if (!aud.isPlaying)
            {
                aud.Play();
            }
        }

        rotationX = Mathf.Clamp(rotationX, -90, 90);
        var rotX = transform.localEulerAngles;
        rotX.x = rotationX;
        transform.localEulerAngles = -rotX;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (stream.IsWriting)
        //{
        //    // We own this player: send the others our data
        //    stream.SendNext(this.rotationX);
        //    stream.SendNext(this.rotationY);
        //}
        //else
        //{
        //    // Network player, receive data
        //    this.rotationX = (float)stream.ReceiveNext();
        //    this.rotationY = (float)stream.ReceiveNext();
        //}

    }
}
