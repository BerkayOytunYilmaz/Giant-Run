using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    Vector3 screenPoint;
    Vector3 offset;
    Vector3 realPoint;
    Vector3 realPosition;
    public float MoveRange = 2.5f;
    public float Speed = 5f;
    bool Stop;

    public Animator PlayerAnimator;
    public Animator EnemyAnimator;
    public float PlayerScale;
    public Text Skor;
    

    // Start is called before the first frame update
    void Start()
    {
        Stop = false;
        PlayerScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Skor.text = "" + PlayerScale;
        if (Stop==false)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(0, transform.position.y +2.5f, transform.position.z - 5f), Time.deltaTime * 10);
            transform.Translate(transform.forward * Time.deltaTime * Speed, Space.World);
        }



        if (transform.position.x > -MoveRange && transform.position.x < MoveRange && Stop==false)
        {
            AxisXMovement();
        }

       
    }


    void AxisXMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        }
        if (Input.GetMouseButton(0))
        {

            realPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            realPosition = Camera.main.ScreenToWorldPoint(realPoint) + offset;

            if (realPosition.x > -MoveRange && realPosition.x < MoveRange)
            {

               
                
                    transform.position = new Vector3(realPosition.x, transform.position.y, transform.position.z);
                

            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Obstacle")
        {
            Stop = true;
            Debug.Log("girdi");
        }
        if (other.gameObject.tag=="+1")
        {
            Vector3 Scale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, transform.localScale.z + 0.1f);
            transform.DOScale(Scale, 0.1f);
            PlayerScale += 0.1f;
        }
        if (other.gameObject.tag == "+2")
        {
            Vector3 Scale = new Vector3(transform.localScale.x + 0.2f, transform.localScale.y + 0.2f, transform.localScale.z + 0.2f);
            transform.DOScale(Scale, 0.1f);
            PlayerScale += 0.2f;
        }
        if (other.gameObject.tag == "+3")
        {
            Vector3 Scale = new Vector3(transform.localScale.x + 0.3f, transform.localScale.y + 0.3f, transform.localScale.z + 0.3f);
            transform.DOScale(Scale, 0.1f);
            PlayerScale += 0.3f;
        }
        if (other.gameObject.tag == "-1")
        {
            Vector3 Scale = new Vector3(transform.localScale.x - 0.1f, transform.localScale.y - 0.1f, transform.localScale.z - 0.1f);
            transform.DOScale(Scale, 0.1f);
            PlayerScale -= 0.1f;
        }
        if (other.gameObject.tag == "-2")
        {
            Vector3 Scale = new Vector3(transform.localScale.x - 0.2f, transform.localScale.y - 0.2f, transform.localScale.z - 0.2f);
            transform.DOScale(Scale, 0.1f);
            PlayerScale -= 0.2f;
        }
        if (other.gameObject.tag == "-3")
        {
            Vector3 Scale = new Vector3(transform.localScale.x - 0.3f, transform.localScale.y - 0.3f, transform.localScale.z - 0.3f);
            transform.DOScale(Scale, 0.1f);
            PlayerScale -= 0.3f;
        }
        if (other.gameObject.tag == "Final")
        {
            Stop = true;
            PlayerAnimator.SetBool("Fight", true);
            EnemyAnimator.SetBool("EnemyFight", true);

            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(30f, transform.position.y + 0.5f, transform.position.z + 25f), Time.deltaTime * 10);
            Camera.main.transform.DORotate(Vector3.up * 270, 1f);
            if (PlayerScale>1.25f)
            {
                StartCoroutine("AnimatorCoroutine");
            }
            else
            {
                StartCoroutine("EnemyAnimatorCoroutine");

            }

        }
    }

    IEnumerator AnimatorCoroutine()
    {
        
        yield return new WaitForSeconds(3);
        EnemyAnimator.SetBool("EnemyDead", true);
    }
    IEnumerator EnemyAnimatorCoroutine()
    {

        yield return new WaitForSeconds(3);
        PlayerAnimator.SetBool("Death", true);
    }
}
