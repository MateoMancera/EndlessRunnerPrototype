using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterScript : MonoBehaviour
{
    public Transform limitLeft, limitRight, character, targetRotationLeft, targetRotationRight, targetRotationDefault;

    public bool goLeft, goRight, alive;

    public float speed, step, RotationSpeed;

    public Quaternion startRot, rightRot, actualRot;

    public Animator animator;

    public float vidas;

    public Text vidasTxt;

    public laneMove lane1, lane2, lane3;

    public float TimeOver, timeCollider;

    public SkinnedMeshRenderer meshCharacter;

    public int titileo;

    public Material gridMaterial;

    public BoxCollider colliderUp, colliderDown;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3.0f;
        RotationSpeed = 2.0f;

        vidas = 3;

        titileo = 0;

        gridMaterial.SetFloat("Speed", 2.3f);

        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        vidasTxt.text = "Vidas = " + vidas;

        if (animator.GetBool("slide"))
        {
            animator.SetBool("slide", false);
        }
        if (animator.GetBool("jump"))
        {
            animator.SetBool("jump", false);
        }

        actualRot = transform.rotation;

        step = speed * Time.deltaTime; // calculate distance to move

        if (alive)
        {
            if (Input.GetKey(KeyCode.A) && !goRight)
            {
                goLeft = true;
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                goLeft = false;
            }

            if (Input.GetKey(KeyCode.D) && !goLeft)
            {
                goRight = true;
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                goRight = false;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                animator.SetBool("slide", true);
                StopCoroutine(slideMoment());
                StartCoroutine(slideMoment());

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("jump", true);
                StopCoroutine(jumpMoment());
                StartCoroutine(jumpMoment());

            }
        }
        
        

        if (goRight)
        {
            character.transform.position = Vector3.MoveTowards(character.transform.position, limitRight.position, step);
            targetRotationDefault.transform.position = Vector3.MoveTowards(targetRotationDefault.transform.position, targetRotationRight.position, step);
            

            // Determine which direction to rotate towards
            Vector3 targetDirection = targetRotationRight.position - character.transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = RotationSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(character.transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(character.transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            character.transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else if (goLeft)
        {
            character.transform.position = Vector3.MoveTowards(character.transform.position, limitLeft.position, step);
            targetRotationDefault.transform.position = Vector3.MoveTowards(targetRotationDefault.transform.position, targetRotationLeft.position, step);

            // Determine which direction to rotate towards
            Vector3 targetDirection = targetRotationLeft.position - character.transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = RotationSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(character.transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(character.transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            character.transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            // Determine which direction to rotate towards
            Vector3 targetDirection = targetRotationDefault.position - character.transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = RotationSpeed * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(character.transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(character.transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            character.transform.rotation = Quaternion.LookRotation(newDirection);
        }

        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("obstaculo"))
        {
            other.enabled = false;
            hit(false);
            
        }else if (other.gameObject.CompareTag("obstaculoBajo"))
        {
            other.enabled = false;
            hit(true);

        }
    }

    public void hit(bool islow)
    {
        vidas--;
        if (vidas > 0)
        {
            StartCoroutine(hitMoment());
        }
        else
        {
            death(islow);
        }
    }

    public void death(bool islow)
    {
        if (!islow)
        {
            animator.SetBool("death", true);
        }
        else
        {
            animator.SetBool("lowDeath", true);
        }
        lane1.floorSpeed = 0;
        lane2.floorSpeed = 0;
        lane3.floorSpeed = 0;
        Debug.Log("Grid speed = " + gridMaterial.GetFloat("Speed"));
        gridMaterial.SetFloat("Speed",0f);
        Debug.Log("Grid speed post = " + gridMaterial.GetFloat("Speed"));
        alive = false;
    }
    IEnumerator hitMoment()
    {
        colliderUp.enabled = false;
        colliderDown.enabled = false;
        titileo++;
        meshCharacter.enabled = false;
        yield return new WaitForSeconds(TimeOver);
        meshCharacter.enabled = true;
        yield return new WaitForSeconds(TimeOver);
        if (titileo < 3)
        {
            StartCoroutine(hitMoment());
        }
        else
        {
            titileo = 0;
            colliderUp.enabled = true;
            colliderDown.enabled = true;
        }
    }

    IEnumerator slideMoment()
    {
        colliderUp.enabled = false;

        yield return new WaitForSeconds(timeCollider);

        colliderUp.enabled = true;
    }
    IEnumerator jumpMoment()
    {
        colliderDown.enabled = false;

        yield return new WaitForSeconds(timeCollider);

        colliderDown.enabled = true;
    }

}
