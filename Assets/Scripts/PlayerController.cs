using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    public PlayerData playerData;
    private float maxSpeed, acceleration = 50, deceleration = 50, currentSpeed = 0;
    private bool isSpeedReduced = false;
    [SerializeField] 
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movementInput, movementUse, pointerInput;
    [SerializeField]
    private InputActionReference movement, attack, pointerPosition;
    private Sword sword;
    public GameObject pickupRadiusObject;

    void Awake()
    {   
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sword = GetComponentInChildren<Sword>();
    }

    void Start()
    {
        playerData.maxHP = playerData.baseHP;
        playerData.currentHP = playerData.maxHP;
        playerData.currentSpeed = playerData.baseSpeed;
        playerData.attackDamage = playerData.baseAttackDamage;
        playerData.pickRadius = playerData.basePickRadius;
        playerData.armorPierce = playerData.baseArmorPierce;
        playerData.knockbackForce = playerData.baseknockbackForce;
        playerData.regen = playerData.baseRegen;
        playerData.maxXP = playerData.baseMaxXP;
        playerData.level = 1;
    }

    void Update()
    {   
        pointerInput = GetMouseInput();
        sword.PointerPosition = pointerInput;
        movementInput = movement.action.ReadValue<Vector2>();

        animatePlayer(); // Panggil animatePlayer di Update()

        pickupRadiusObject.GetComponent<CircleCollider2D>().radius = playerData.pickRadius;// Perbarui radius collider sesuai dengan playerData
    }

    void FixedUpdate()
    {   
        maxSpeed = playerData.currentSpeed;
        if(movementInput.magnitude > 0 && currentSpeed >= 0)
        {   
            animator.SetBool("isMove", true);
            movementUse = movementInput;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {   
            animator.SetBool("isMove", false);
            currentSpeed -= deceleration * maxSpeed * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0 , maxSpeed);
        rb.velocity = movementUse * currentSpeed;
    }

    private void animatePlayer()
    {   
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        
        if (lookDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private Vector2 GetMouseInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.CompareTag("Enemy") && !isSpeedReduced)
       {
           StartCoroutine(speedReduction());
       }
    }
    public IEnumerator speedReduction()
    {
        isSpeedReduced = true;

        // Simpan kecepatan asli sebelum pengurangan
        float originalSpeed = playerData.currentSpeed;

        // Kurangi kecepatan pemain
        playerData.currentSpeed = originalSpeed * 0.5f;

        // Tunggu selama 2 detik
        yield return new WaitForSeconds(2f);

        // Kembalikan kecepatan pemain ke nilai aslinya
        playerData.currentSpeed = playerData.baseSpeed;

        isSpeedReduced = false;
    }

}
