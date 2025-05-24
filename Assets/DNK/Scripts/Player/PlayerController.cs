using UnityEngine;
using DNK.Player;
using DNK.Item;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private Collider2D[] results = new Collider2D[3];
    [SerializeField] private GameObject itemSeat;

    [Header("PlayerSprite")]
    [SerializeField] private SpriteRenderer srTop;
    [SerializeField] private Sprite[] topSprites;
    [SerializeField] private SpriteRenderer srBottom;
    [SerializeField] private Sprite[] bottomSprites;
    private float animTimer = 0f;
    [SerializeField] private float animSpeed = 0.2f;

    [Header("PlayerControl")]
    [SerializeField] private bool canControl = true;
    [Header("PlayerControlKey")]
    [SerializeField] private bool isUp = false;
    [SerializeField] private bool isDown = false;
    [SerializeField] private bool isLeft = false;
    [SerializeField] private bool isRight = false;
    [SerializeField] private bool isGrab = false;

    [Header("PlayerState")]
    [SerializeField] private PlayerState playerState = PlayerState.Idle;
    [SerializeField] private PlayerGrabState playerGrabState = PlayerGrabState.Idle;
    
    [Header("Item")]
    [SerializeField] private Collider2D[] hitItems = new Collider2D[1];
    [SerializeField] private ItemBehaviour item;
    [SerializeField] private ItemNames grabItem = ItemNames.None;
    private bool isMoving = false;
    private bool isGrabbing = false;

    [Header("MovementValue")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float friction = 0.9f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }


    private void Update()
    {
        KeyControl();
    }
    private void KeyControl()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            isUp = true;
        }
        else
        {
            isUp = false;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            isDown = true;
        }
        else
        {
            isDown = false;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            isLeft = true;
        }
        else
        {
            isLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isGrab = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isGrab = false;
        }
    }

    void FixedUpdate()
    {
        if (canControl)
        {
            PlayerControl();
        }
    }
    private void PlayerControl()
    {
        if (isUp)
        {
            Up();
        }
        if (isDown)
        {
            Down();
        }
        if (isRight)
        {
            Right();
        }
        if (isLeft)
        {
            Left();
        }

        //摩擦
        if (!isLeft && !isRight)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * friction, rb.linearVelocity.y);
        }
        if (!isUp && !isDown)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * friction);
        }

        if (isGrab)
        {
            Grab(true);
        }
        else
        {
            Grab(false);
        }

        if (rb.linearVelocity.magnitude < 0.1f)
        {   
            isMoving = false;
            playerState = PlayerState.Idle;
        }
        else
        {
            isMoving = true;
            playerState = PlayerState.Walk;
            WalkAnimation();
        }
    }

    private void Up()
    {
        if (rb.linearVelocity.y < maxSpeed)
        {
            rb.AddForce(new Vector2(0, speed));
        }
    }

    private void Down()
    {
        if (rb.linearVelocity.y > -maxSpeed)
        {
            rb.AddForce(new Vector2(0, -speed));
        }
    }

    private void Right()
    {
        if (rb.linearVelocity.x < maxSpeed)
        {
            rb.AddForce(new Vector2(speed, 0));
        }
    }

    private void Left()
    {
        if (rb.linearVelocity.x > -maxSpeed)
        {
            rb.AddForce(new Vector2(-speed, 0));
        }
    }

    private void Grab(bool isEnable)
    {
        if (isEnable)
        {
            isGrabbing = true;
            playerGrabState = PlayerGrabState.Grab;
            srTop.sprite = topSprites[1];

            PickUp();
        }
        else
        {
            isGrabbing = false;
            playerGrabState = PlayerGrabState.Idle;
            srTop.sprite = topSprites[0];

            Drop();
        }
    }

    private void WalkAnimation()
    {
        animTimer += Time.deltaTime;
        if (animTimer >= animSpeed)
        {
            srBottom.sprite = bottomSprites[0];
        }
        if (animTimer >= animSpeed * 2)
        {
            srBottom.sprite = bottomSprites[1];
            animTimer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemBehaviour obj = other.GetComponent<ItemBehaviour>();
        if (obj == null) return;

        if (obj != null)
        {
            hitItems[0] = other;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (hitItems[0] = other)
        {
            hitItems[0] = null;
        }
    }
    private void PickUp()
    {
        if (hitItems[0] == null) return;

        if (grabItem != ItemNames.None) return;

        item = hitItems[0].GetComponent<ItemBehaviour>();
        if (item == null) return;

        grabItem = item.itemName;
        item.SetItemState(ItemState.Grab);
        playerGrabState = PlayerGrabState.Grab;

        //オブジェクト操作
        GameObject itemObject = item.transform.gameObject;
        
        itemObject.transform.SetParent(itemSeat.transform);
        itemObject.transform.localPosition = Vector3.zero;

        grabItem = item.itemName;
    }

    private void Drop()
    {
        if (grabItem == ItemNames.None) return;

        grabItem = ItemNames.None;
        item.SetItemState(ItemState.Idle);

        //オブジェクト操作
        GameObject itemObject = item.transform.gameObject;

        itemObject.transform.SetParent(null);
        itemObject.transform.position = transform.position;

        item = null;
    }
}