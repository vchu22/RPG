using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public GameObject canvasUI, currentTree;
    public Text txt;
    
    public Rigidbody2D rb;
    public Animator anim;
    public float movementSpeed = 5f;
    public bool canChop = false;

    private Vector2 _input;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this._input.x = Input.GetAxisRaw("Horizontal");
        this._input.y = Input.GetAxisRaw("Vertical");
        this._input.Normalize();

        anim.SetFloat("Horizontal", _input.x);
        anim.SetFloat("Vertical", _input.y);
        anim.SetFloat("Speed", _input.sqrMagnitude);

        if (canChop) {
            if (Input.GetKey("f")) {
                StartCoroutine(DoDialogue("You've collected some wood!", 1.5f));
                Destroy(currentTree);
                canChop = false;
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + this._input * movementSpeed * Time.fixedDeltaTime);
        if (_input.x > 0) {
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        } else {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        canChop = true;
        currentTree = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canChop = false;
        currentTree = null;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        switch (other.gameObject.tag) {
            case "chest":
                StartCoroutine(ShakeChest(other.gameObject, 1.5f, 0.05f));
                StartCoroutine(DoDialogue("Opening Chest", 1.5f));
                break;
            case "npc":
                if (other.gameObject.name == "fred") {
                    StartCoroutine(DoDialogue("You are not allowed to leave", 4f));
                }
                break;
            case "tree":
                if (other.gameObject.name == "choppable_tree") {
                    StartCoroutine(DoDialogue("Press F to chop tree.", 4f));
                }
                break;
            default:
                print("You can't go this way");
                break;
        }
    }

    public IEnumerator DoDialogue(System.String display, float duration) {
        txt.text = display;
        canvasUI.SetActive(true);

        float elapsed = 0f;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            yield return 0;
        }

        canvasUI.SetActive(false);
    }

    public IEnumerator ShakeChest(GameObject obj, float duration, float magnitude)
    {
        Destroy(obj.GetComponent<BoxCollider2D>());
        Vector3 orignalPosition = obj.transform.position;
        float elapsed = 0f;
        bool back = false;
        
        while (elapsed < duration)
        {
            if (back) {
                obj.transform.position = orignalPosition;

                back = false;
                continue;
            }
            back = true;
            float dx = Random.Range(-1f, 1f) * magnitude;
            float dy = Random.Range(-1f, 1f) * magnitude;

            obj.transform.position += new Vector3(dx, dy, 0);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        Destroy(obj);
    }
}
