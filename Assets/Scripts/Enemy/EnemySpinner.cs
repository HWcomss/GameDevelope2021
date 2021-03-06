using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpinner : MonoBehaviour
{
    [SerializeField] private bool isMove = true;
    [SerializeField] private bool isDeal = true;
    [SerializeField] private bool isHp = true;

    CharacterHealth playerHP;
    EnemyHealth enemyHP;

    [SerializeField] private float attackDamgage = 25.0f;
    [SerializeField] private GameObject hitParticle;
    private TextMeshPro hitParticleText;

    [Space(10)]
    [Header("----------------------------- Dealable timer -----------------------------")]
    [SerializeField] private bool isDealready = false;
    [SerializeField] private float dealableTime = 0.3f;
    [SerializeField] private float dealTimer;

    EnemyHealth enemyhealthScript;
    float temp_Hp;
    public GameObject particlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerHP = GameObject.FindWithTag("Player").GetComponent<CharacterHealth>();
        enemyHP = this.GetComponent<EnemyHealth>();
        //hitParticle = (GameObject) Resources.Load("Scenes/Model/hit.prefab");
        //hitParticle.GetComponentInChildren<TextMesh>().text = ((int)attackDamgage).ToString();

        hitParticleText = hitParticle.GetComponentInChildren<TextMeshPro>();
        hitParticleText.text = "0";

        enemyhealthScript = GetComponent<EnemyHealth>();
        temp_Hp = enemyhealthScript.getMaxHp();
    }

    // Update is called once per frame
    void Update()
    {
        //getHit
        if (enemyhealthScript.getHp() < temp_Hp)
        {
            temp_Hp = enemyhealthScript.getHp();

            GameObject soulPrefab = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            soulPrefab.GetComponent<SoulParticle>().monster = this.gameObject;

            //anim.SetBool("Is_Block", true);

            //StartCoroutine(Block());
        }

        if (!isMove) return;

        if (!isHp) return;

        if (playerHP.getDead() || enemyHP.getDead()) return;
        this.transform.Rotate(0,0.3f,0,Space.Self);
    }

    
    private void FixedUpdate()
    {
        if(!isDealready)
        {
            dealTimer -= Time.deltaTime;
        }

        if(dealTimer <= 0)
        {
            dealTimer = dealableTime;
            isDealready = true;
        }
    }
    
    
    private void OnTriggerStay(Collider other)
    {
        if (!isDeal) return;

        if (playerHP.getDead()) return;

        if (isHp)
            if (enemyHP.getDead()) return;

        //print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            if(isDealready)
            {
                if(playerHP.changeHp(-attackDamgage, 1) && hitParticle != null)
                {
                    //hitParticle.GetComponentInChildren<TextMeshPro>().text = ((int)attackDamgage).ToString();
                    hitParticleText.text = ((int)attackDamgage).ToString();

                    isDealready = false;
                    GameObject.Instantiate(hitParticle, this.GetComponentInChildren<Collider>().ClosestPointOnBounds(other.transform.position) , transform.rotation);
                }
                    
            }
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (!isDeal) return;

        if (playerHP.getDead()) return;

        if (isHp)
            if (enemyHP.getDead()) return;

        //print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            if (isDealready)
            {
                if (playerHP.changeHp(-attackDamgage, 1) && hitParticle != null)
                {
                    
                    //hitParticle.GetComponentInChildren<TextMeshPro>().text = ((int)attackDamgage).ToString();
                    hitParticleText.text = ((int)attackDamgage).ToString();

                    isDealready = false;
                    GameObject.Instantiate(hitParticle, this.GetComponentInChildren<Collider>().ClosestPointOnBounds(other.transform.position), transform.rotation);
                }

            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (!isDeal) return;

        if (playerHP.getDead()) return;

        if (isHp)
            if (enemyHP.getDead()) return;

        //print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            if (isDealready)
            {
                if (playerHP.changeHp(-attackDamgage, 1) && hitParticle != null)
                {

                    //hitParticle.GetComponentInChildren<TextMeshPro>().text = ((int)attackDamgage).ToString();
                    hitParticleText.text = ((int)attackDamgage).ToString();

                    isDealready = false;
                    GameObject.Instantiate(hitParticle, this.GetComponentInChildren<Collider>().ClosestPointOnBounds(other.transform.position), transform.rotation);
                }

            }
        }
    }

    public void setMove(bool flag) {
        isMove = flag;
    }
    public void setDeal (bool flag)
    {
        isDeal = flag;
    }

    public void setMove()
    {
        isMove = !isMove;
    }
    public void setDeal()
    {
        isDeal = !isDeal;
    }
}
