using UnityEditor.Animations;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{

    public float speed = 3;

    Rigidbody2D rb2d;
    Vector2 movementInput;

    private Animator animator;

    private bool gameIsPaused = false;

    //si esta atacando o no
    private bool isAttacking = false;

    [HideInInspector]
    //para saber si nos podemos mover, no podriamos movernos si estamos atacando
    public bool canMove = true;

    //ultima direccion dd nos movemos
    Vector2 lastMovementDir = Vector2.right;


    Vector2 attackDir;
    public float attackRange = 1.2f;
    public int attackDamage = 1;
    //para detectar layer de los objetos, label enemy para saber a que le pegamos
    public LayerMask targetLayer;


    private int xp = 1;
    [HideInInspector]
    public int currentLevel = 0;


    [Header("Skin")]
    public NPCSkin selectedSkin;
    public AnimatorController[] animatorControllers;
    public enum NPCSkin{Blue,Purple,Red,Yellow}

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        UIManager.Instance.UpdatePlayerStats(xp, currentLevel, speed, attackDamage);
        ApplySkin();
    }

    void Update()
    {

        if (movementInput != Vector2.zero )
        {
            lastMovementDir = movementInput;
        }

        //posicion x del personaje
        movementInput.x = Input.GetAxisRaw("Horizontal");
        //posicion y del personaje
        movementInput.y = Input.GetAxisRaw("Vertical");

        //movimiento en todas las direcciones igual
        movementInput = movementInput.normalized;

        animator.SetFloat("Horizontal", Mathf.Abs(movementInput.x));
        animator.SetFloat("Vertical", Mathf.Abs(movementInput.y));

        CheckFlip();
        OpenCloseInventory();
        OpenClosePauseMenu();
        OpenCloseStatsPlayer();

        Attack();

    }


    private void FixedUpdate()
    {

        if (canMove)
        {
            //velocidad a la que se mueve
            rb2d.linearVelocity = movementInput * speed;
        }

    }



    void CheckFlip()
    {
        if (movementInput.x > 0 && transform.localScale.x < 0 || movementInput.x < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }

    }


    void OpenCloseInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UIManager.Instance.OpenOrCloseInventory();
        }



    }

    void OpenCloseStatsPlayer()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UIManager.Instance.OpenOrCloseStatsPlayer();
        }


    }

    void OpenClosePauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPaused)
            {
                UIManager.Instance.ResumeGame();
                gameIsPaused = false;
            }
            else
            {
                UIManager.Instance.PauseGame();
                gameIsPaused = true;
            }
        }
    }


    void Attack()
    {

        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {

            //coge la direccion del ultimo movimiento
            int dir = GetDirectionIndex(lastMovementDir);
            attackDir = GetAttackInputDirection();
            int attackDirection = GetDirectionIndex(attackDir);

            animator.SetInteger("AttackDirection", attackDirection);





            //para saber que ataque se usa
            int randomIndex = Random.Range(0, 2);
            animator.SetInteger("AttackIndex", randomIndex);
            animator.SetTrigger("DoAttack");

        }


    }


    public void StartAttack()
    {
        isAttacking = true;
        rb2d.linearVelocity = Vector2.zero;
        canMove = false;
    }

    public void EndAttack()
    {
        isAttacking = false;
        canMove = true;
    }


    Vector2 GetAttackInputDirection()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (inputDir != Vector2.zero)
        {
            return inputDir;
        }
        else
        {
            if (transform.localScale.x > 0)
            {
                return Vector2.right;
            }
            else
            {
                return Vector2.left;
            }
        }
    }

    int GetDirectionIndex(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            //si la condicion en x es mayor que 0, se devuelve 0, sino se devuelve 1 (saber si miramos a derecha o izq)
            return dir.x > 0 ? 0 : 1;
        }
        else
        {
            // si la direccion es hacia arriba o hacia abajo
            return dir.y > 0 ? 2 : 3;
        }
    }

    public void DetectAndDamageTargets()
    {
        Vector2 attackPoint = (Vector2)transform.position + attackDir.normalized * attackRange * 0.1f;
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackPoint, attackRange, targetLayer);

        foreach (Collider2D target in hitTargets)
        {
            Vector2 hitDirection = target.transform.position - transform.position;
            GameObject obj = target.gameObject;
            int layer = obj.layer;

            if (layer == LayerMask.NameToLayer("Enemy"))
            {
                obj.GetComponent<DamageReceiver>().ApplyDamage(attackDamage, true, false, hitDirection);
            }
            else if (layer == LayerMask.NameToLayer("Sheep"))
            {
                obj.GetComponent<DamageReceiver>().ApplyDamage(attackDamage, true, false, hitDirection);

            }
            else if (layer == LayerMask.NameToLayer("Tree"))
            {
                obj.GetComponent<DamageReceiver>().ApplyDamage(attackDamage, false, true, hitDirection);

            }
            else
            {

            }


        }
    }


    private void OnEnable()
    {
        DamageReceiver.OnTargetKilled += AddExp;
    }

    private void OnDisable()
    {
        DamageReceiver.OnTargetKilled -= AddExp;

    }


    public void AddExp(int xpAmount)
    {
        xp += xpAmount;

        if (xp>100)
        {
            xp -= 100;

            LevelUp();
        }


        UIManager.Instance.UpdatePlayerStats(xp, currentLevel, speed, attackDamage);

    }


    public void LevelUp()
    {
        currentLevel += 1;

        switch (currentLevel)
        {
            case 2:
                speed += 1;
                attackDamage += 1;
                GetComponent<DamageReceiverPlayer>().GainHealth(1);
                break;

            case 3:
                speed += 1;
                break;

            case 4:
                attackDamage += 1;
                break;

            case 5:
                GetComponent<DamageReceiverPlayer>().GainHealth(2);
                break;

            case 6:
                speed += 1;
                break;

            case 7:

                GetComponent<DamageReceiverPlayer>().GainHealth(1);
                break;

            case 8:
                speed += 1;
                break;


            case 9:
                
                attackDamage += 1;
                
                break;

            case 10:
                GetComponent<DamageReceiverPlayer>().GainHealth(5);
                break;

            case 11:
                GetComponent<DamageReceiverPlayer>().GainHealth(5);
                break;

            case 12:
                speed += 1;
                
                break;

            case 13:
                speed += 1;
                attackDamage += 1;
                GetComponent<DamageReceiverPlayer>().GainHealth(5);
                break;


            default:
                break;
        }

    }


    void ApplySkin()
    {
        string savedSkinName = PlayerPrefs.GetString("MainPlayerSkin", "Blue");

        if (System.Enum.TryParse(savedSkinName, out NPCSkin skin))
        {
            selectedSkin = skin;
        }
        else
        {
            selectedSkin = NPCSkin.Red;
        }

        if (animatorControllers != null && animatorControllers.Length > 0)
        {
            int skinIndex = (int)selectedSkin;
            if (animator != null && skinIndex < animatorControllers.Length)
            {
                animator.runtimeAnimatorController = animatorControllers[skinIndex];

            }

        }
    }



}
