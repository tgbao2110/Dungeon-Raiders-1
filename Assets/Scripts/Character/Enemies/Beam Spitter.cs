using UnityEngine;

public class BeamSpitter : Enemy
{
    // Start is called before the first frame update

    [SerializeField] GunData attackData;
    
    public Transform shootingPoint;
    public float speed = 10f;
    float interval = 0.25f;
    public int numberOfBullets = 8;

    public Transform Player; 

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if(interval>0)
        {
            interval-= Time.deltaTime;
        }
        else
        {
            Shoot();
            interval=0.25f;
        }
        
         //InvokeRepeating("Shoot", 1f,1f);
    }

    void Shoot()
    {
        // Get the direction vector from the enemy to the player
        Vector3 directionToPlayer = (Player.position - shootingPoint.position).normalized;

        // Calculate the angle in degrees
        float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        attackType.ExecuteAttack(attackData, shootingPoint,directionToPlayer, angleToPlayer);
    }

    public override void SetAttackType()
    {
        attackType = new SingleBulletAttack();
    }

}
