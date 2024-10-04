using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControl : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpPower = 300;
    private Rigidbody rigid;
    public static GameObject player;
    public static int dir = 0;
    float h;
    private ParticleSystem particle;
    public static bool isJump = true;
    private ParticleSystem pat;
    private AudioSource landing;
    [SerializeField] private AudioSource jumpAud;
    [SerializeField] private AudioSource clear;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        if (player == null) player = gameObject;
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        pat = transform.GetChild(2).GetComponent<ParticleSystem>();
        landing = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (dir != 0)
        {
            transform.Translate(Vector3.right * dir * Time.deltaTime * speed);
            ParticleSystem.EmissionModule emission = particle.emission;
            emission.rateOverTime = 70;
        }
        else if (Mathf.Sqrt(rigid.velocity.x * rigid.velocity.x + rigid.velocity.y * rigid.velocity.y) > 1)
        {
            ParticleSystem.EmissionModule emission = particle.emission;
            emission.rateOverTime = 70;
        }
        else
        {
            ParticleSystem.EmissionModule emission = particle.emission;
            emission.rateOverTime = 0;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length > 0)
        {
            if (GameManager.Instance.isPlaying == true)
            {
                Vector3 collisionPoint = collision.contacts[0].point;
                pat.transform.position = new Vector3(collisionPoint.x, collisionPoint.y, -70);
                pat.Play();
                landing.PlayOneShot(landing.clip);
            }
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            clear.Play();
            collision.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
            collision.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            StartCoroutine(GameManager.Instance.ClearStage(collision.gameObject.GetComponent<MeshRenderer>()));
        }
        else if (collision.gameObject.CompareTag("Die"))
        {
            GameManager.Instance.Die();
        }
        else if (collision.gameObject.name =="GameEnd")
        {
            GameManager.Instance.Ending();
        }
    }
    public void OnClickJump()
    {
        if (isJump)
        {
            jumpAud.Play();
            rigid.AddForce(Vector3.up * jumpPower);
            isJump = false;
        }
    }
}
