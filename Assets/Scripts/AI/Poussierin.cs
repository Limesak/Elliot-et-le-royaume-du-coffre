using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Poussierin : MonoBehaviour
{
    public enum AttackType { Vertical, Horizontal};

    [Header("Inspector Info")]
    public DamageCentralizer DC;
    public Animator Anim;
    private GameObject PLAYER;
    public NavMeshAgent NavAgent;
    public LockableObject LO;
    public RelevantEntity RE;

    public GameObject Prefab_Piece;
    public GameObject Prefab_Bonbon;

    [Header("Global Info")]
    public int TableIndex;
    public bool RespawnAfterReload;
    public int HPmax;
    private int HP;//HPmax
    private bool isDead;//false

    [Header("Animation")]
    public int CurrentAnimVersion;
    public float MoveEyeCooldown;
    private float lastTimeEyeMoved;
    public GameObject[] PointsToLookAt;
    private int CurrentIndexPTLA;
    public MeshRenderer HeadMR;
    public SkinnedMeshRenderer BodyMR;
    public Material[] HeadSkins;
    public Material[] BodySkins;
    public GameObject PREFAB_Head;
    public GameObject PREFAB_Hit;
    public GameObject PREFAB_HitSword;

    [Header("Deplacement & Targets")]
    private Vector3 LastPositionSeen;
    public GameObject[] WanderingPoints;
    private int CurrentPointSelected;
    public float WanderingPointRadius;
    public float SPEEDwandering;
    public float SPEEDchasing;
    public float MinTimeSpentOnPoint;
    public float MaxTimeSpentOnPoint;
    private float CurrentTimeToSpent;
    public int MinAngleVerifiedOnSpot;
    public int MaxAngleVerifiedOnSpot;
    private int CurrentAngleVerifiedOnSpot;//rdm
    private int haveCheckXAngles;//0
    public bool isOnPoint;//false
    private Vector3 CurrentCheckingAnglePosition;
    public float minDistToCheckAngle;
    private float lastTimeAngleChecked;

    [Header("Aggro")]
    public float minDistToDetect;
    private bool isAgressive;//false
    private float lastTimePlayerSeen;
    public float maxSearchDuration;
    public float maxDistFromPoints;
    public GameObject Eye;
    public GameObject ModelEye;
    public float TauntDuration;
    private float lastDateTaunt;


    [Header("Attacks & Combat")]
    public float MinDistToHit;
    public float DamageHorizontalAttack;
    public float DamageVerticalAttack;
    public float AttackBaseCooldown;
    public float AttackMaxAdditionnalCooldown;
    private float CurrentAdditionalAttrackCooldown;
    private float lastAttackDate;
    public float StunDuration;
    private float lastTimeDamageTaken;
    public float speedOfRotation;
    public float radiusOfCombatEvolution;
    private Vector3 CurrentCombatMovePos;
    private bool didCombatMove;
    public float YLookingOffset;
    private bool isAttacking;
    private Vector3 PosWhenAttacking;
    private int AttackKey;
    private AttackType LastAttackType;
    private bool isPushed;
    private float currentForce;
    private Vector3 currentPushDirection;
    public float HitPushForce;
    public float ForceFriction;

    void Start()
    {
        HP = HPmax;
        currentForce = 0;
        isAgressive = false;
        isPushed = false;
        currentPushDirection = Vector3.zero;
        PLAYER = GameObject.FindGameObjectWithTag("Player");
        HeadMR.material = HeadSkins[Random.Range(0,HeadSkins.Length)];
        BodyMR.material = BodySkins[Random.Range(0, BodySkins.Length)];
        transform.localScale = transform.localScale * Random.Range(0.6f, 1.2f);
    }

    void Update()
    {
        CheckHP();
        if (isPushed)
        {
            Push(currentPushDirection, currentForce);
        }
        if (!isDead )
        {
            if (isAgressive)
            {
                if(lastTimeDamageTaken + StunDuration <= Time.time)//Not stun    isFree
                {
                    if (lastDateTaunt + TauntDuration <= Time.time) //isFree
                    {
                        if (isAttacking)
                        {
                            LookAt(PosWhenAttacking);
                            ModelEye.transform.LookAt(PosWhenAttacking);
                            StopWalking();
                            //Debug.Log("isAttacking");
                        }
                        else//not Attacking
                        {
                            if (CanSeePlayer())
                            {
                                ModelEye.transform.LookAt(PLAYER.transform.position);
                                if (Vector3.Distance(transform.position, PLAYER.transform.position) <= MinDistToHit)// Close enought to hit
                                {
                                    if (lastAttackDate + CurrentAdditionalAttrackCooldown + AttackBaseCooldown <= Time.time)// can hit
                                    {
                                        LookAt(PLAYER.transform.position);
                                        ModelEye.transform.LookAt(PLAYER.transform.position);
                                        //FRAPPER LE JOUEUR
                                        StopWalking();
                                        Attack();
                                    }
                                    else// can't hit yet
                                    {
                                        if (didCombatMove)
                                        {
                                            if (Vector3.Distance(transform.position, CurrentCheckingAnglePosition) <= minDistToCheckAngle) // arrived on combat point
                                            {
                                                LookAt(PLAYER.transform.position);
                                                ModelEye.transform.LookAt(PLAYER.transform.position);
                                                StopWalking();
                                            }
                                            else// getting on combat point
                                            {
                                                GoTo(CurrentCombatMovePos);
                                                LookAt(NavAgent.steeringTarget);
                                                ModelEye.transform.LookAt(NavAgent.steeringTarget);
                                                //ALLER VERS LA POSITION DE COMBAT

                                            }
                                        }
                                        else
                                        {
                                            //INIT CONTOURNER LE JOUEUR
                                            //DoCombatMove();
                                            StopWalking();
                                        }
                                    }
                                }
                                else // Not close enough to hit
                                {
                                    GoTo(PLAYER.transform.position);
                                    LookAt(NavAgent.steeringTarget);
                                    ModelEye.transform.LookAt(NavAgent.steeringTarget);

                                    //AVANCER VERS LE JOUEUR
                                }
                            }
                            else // can't see player
                            {
                                LookCrazy();

                                
                                //AVANCE VERS DERNIERE POSITION CONNU
                                //CHECK SI A PORTE DE LA LASTPOS ET SI SA FAIT LONGTEMP QU'IL CHERCHE
                                if(Vector3.Distance(transform.position, LastPositionSeen) <= radiusOfCombatEvolution)
                                {
                                    StopWalking();
                                    if(lastTimePlayerSeen + maxSearchDuration <= Time.time)
                                    {
                                        isOnPoint = false;
                                        isAgressive = false;
                                        FindNewWanderingPoint();
                                    }
                                }
                                else//Not on lastPos yet
                                {
                                    GoTo(LastPositionSeen);
                                    LookAt(NavAgent.steeringTarget);
                                }
                                
                            }
                        }
                        
                    }
                    else //isTaunting
                    {
                        LookAt(PLAYER.transform.position);
                        ModelEye.transform.LookAt(PLAYER.transform.position);
                    }
                }
                else // is Stunned
                {
                    StopWalking();
                }
                
            }
            else //is not Agressive----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            {
                if (lastTimeDamageTaken + StunDuration <= Time.time)//Not stun    isFree
                {
                    if (CanSeePlayer())
                    {
                        StopWalking();
                        Taunt();
                    }
                    else
                    {
                        LookCrazy();
                        if (isOnPoint)
                        {
                            if(lastTimeAngleChecked + CurrentTimeToSpent<= Time.time)// Check last angle is done
                            {
                                if(haveCheckXAngles >= CurrentAngleVerifiedOnSpot)//this point is done
                                {
                                    //RESET & TROUVER UN NOUVEAU POINT
                                    FindNewWanderingPoint();
                                }
                                else// Angle to look at in stock
                                {
                                    //CHECK UN NOUVEL ANGLE
                                    FindNewAngleToCheck();
                                }
                            }
                            else//still checking angle
                            {
                                if (Vector3.Distance(transform.position, CurrentCheckingAnglePosition) <= minDistToCheckAngle) // arrived on point
                                {
                                    //STOPER LE POUSSIERIN
                                    StopWalking();
                                }
                                else// getting on point
                                {
                                    //ALLER VERS LA POSITION DE L'ANGLE
                                    GoTo(CurrentCheckingAnglePosition);
                                    LookAt(NavAgent.steeringTarget);
                                    ModelEye.transform.LookAt(NavAgent.steeringTarget);
                                }
                            }
                        }
                        else //Going on new point
                        {
                            if(Vector3.Distance(transform.position, WanderingPoints[CurrentPointSelected].transform.position) <= WanderingPointRadius) // arrived on point
                            {
                                isOnPoint = true;
                                //CHECKER LES ANGLES
                                FindNewAngleToCheck();
                            }
                            else// getting on point
                            {
                                //ALLER VERS LE POINT
                                GoTo(WanderingPoints[CurrentPointSelected].transform.position);
                                LookCrazy();
                                LookAt(NavAgent.steeringTarget);
                                ModelEye.transform.LookAt(NavAgent.steeringTarget);
                            }
                        }
                    }
                        
                }
                else // is Stunned
                {
                    StopWalking();
                }
            }
        }
    }

    public void CheckHP()
    {
        List<Damage> TickDamage = DC.AnalyseCache();
        string AntiBugMemory = "";
        bool triggerHit = false;
        Vector3 Direction = Vector3.zero;
        foreach (Damage d in TickDamage)
        {
            if (!AntiBugMemory.Contains(d._key + ""))
            {
                AntiBugMemory = AntiBugMemory + d._key;
                HP = HP - d._power;
                triggerHit = true;
                Instantiate(PREFAB_Hit, d._impactPoint, Quaternion.identity);
                Instantiate(PREFAB_HitSword, d._impactPoint, Quaternion.identity);
                Direction = (transform.position - d._source.transform.position).normalized;
}
        }
        if (triggerHit)
        {
            Push(Direction,HitPushForce);
            CurrentAnimVersion = (int) Random.Range(1, 3);
            Anim.SetInteger("animVersion", CurrentAnimVersion);
            lastTimeDamageTaken = Time.time;
            
            if (HP > 0 && !isDead && (!isAttacking || isAttacking && LastAttackType != AttackType.Vertical))
            {
                Anim.SetTrigger("hit");
                isAttacking = false;
            }
            else if(HP <= 0 && !isDead)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;
            Anim.SetTrigger("die");
            StopWalking();
            PLAYER.GetComponent<DiaryManager>().AddAKill(0);
            if (RE != null && !RespawnAfterReload)
            {
                RE.NotRelevantAnymore();
            }
            LO.Die();
            transform.DOScale(transform.localScale*0.95f,2f).OnComplete(() => { DiePartTwo(); });
        }
    }

    public void DiePartTwo()
    {
        transform.DOScale(Vector3.zero, 0.8f).OnComplete(() => { DiePartThree(); });
    }

    public void DiePartThree()
    {
        GameObject SpawnedHead = Instantiate(PREFAB_Head, ModelEye.transform.position, Quaternion.identity);
        float rdm = Random.Range(0, 100);
        if (rdm > 95)
        {
            for (int i = 0; i < 10; i++)
            {
                Instantiate(Prefab_Piece, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 1 + Random.Range(-0.1f, 0.2f), Random.Range(-0.5f, 0.5f)), Quaternion.identity);
            }
        }
        else if(rdm > 80)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(Prefab_Piece, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 1 + Random.Range(-0.1f, 0.2f), Random.Range(-0.5f, 0.5f)), Quaternion.identity);
            }
        }
        else if (rdm > 50)
        {
            for(int i = 0; i< 3; i++)
            {
                Instantiate(Prefab_Piece, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 1 + Random.Range(-0.1f, 0.2f), Random.Range(-0.5f, 0.5f)), Quaternion.identity);
            }
        }
        else
        {
            Instantiate(Prefab_Piece,transform.position + new Vector3(Random.Range(-0.5f,0.5f),1+ Random.Range(-0.1f, 0.2f), Random.Range(-0.5f, 0.5f)), Quaternion.identity);
        }

        float rdm2 = Random.Range(0, 100);
        if (PLAYER.GetComponent<LifeManager>().GetCurrentIndex() >= 2 && rdm2 > 60)
        {
            Instantiate(Prefab_Bonbon, transform.position + new Vector3( 0,1 , 0), Quaternion.identity);
        }
        SpawnedHead.GetComponent<MeshRenderer>().material = HeadMR.material;
        Destroy(gameObject);
    }

    public bool CanSeePlayer()
    {
        bool res = false;
        Vector3 direction = (PLAYER.transform.position+new Vector3(0, YLookingOffset, 0))-transform.position;
        RaycastHit hit;
        Debug.DrawRay(Eye.transform.position, direction, Color.green);
        if (Physics.Raycast(Eye.transform.position, direction, out hit))
        {
            if(hit.collider.gameObject.tag == "Player" && Vector3.Distance(PLAYER.transform.position, transform.position) <=minDistToDetect)
            {
                LastPositionSeen = PLAYER.transform.position;
                lastTimePlayerSeen = Time.time;
                res = true;
            }
            //Debug.Log("Seeing " + hit.collider.gameObject.name + " with tag "+hit.collider.gameObject.tag);
        }

        if(Vector3.Distance(PLAYER.transform.position, transform.position) <= minDistToDetect / 3)
        {
            LastPositionSeen = PLAYER.transform.position;
            lastTimePlayerSeen = Time.time;
            res = true;
        }

        return res;
    }

    public void LookAt(Vector3 Target)
    {
        Vector3 direction = (Target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speedOfRotation);
    }

    public void LookCrazy()
    {
        if(lastTimeEyeMoved + MoveEyeCooldown <= Time.time)
        {
            lastTimeEyeMoved = Time.time;
            CurrentIndexPTLA = Random.Range(0, PointsToLookAt.Length);
            ModelEye.transform.LookAt(PointsToLookAt[CurrentIndexPTLA].transform.position);
        }
        else
        {
            ModelEye.transform.LookAt(PointsToLookAt[CurrentIndexPTLA].transform.position);
            //Debug.Log("Looking point" + CurrentIndexPTLA);
        }
    }

    public void Taunt()
    {
        isAgressive = true;
        Anim.SetTrigger("taunt");
        lastDateTaunt = Time.time;
    }

    public void GoTo(Vector3 Position)
    {
        if(!isAttacking && !isDead){
            NavAgent.isStopped = false;
            if (isAgressive)
            {
                Anim.SetBool("chasing", true);
                NavAgent.speed = SPEEDchasing;
            }
            else
            {
                Anim.SetBool("walking", true);
                NavAgent.speed = SPEEDwandering;
            }
            NavAgent.destination = Position;
        }
    }

    public void StopWalking()
    {
        NavAgent.destination = transform.position;
        NavAgent.isStopped = true;
        Anim.SetBool("walking", false);
        Anim.SetBool("chasing", false);
    }

    public void FindNewWanderingPoint()
    {
        CurrentPointSelected = Random.Range(0, WanderingPoints.Length);
        CurrentAngleVerifiedOnSpot = Random.Range(MinAngleVerifiedOnSpot, MaxAngleVerifiedOnSpot);
        haveCheckXAngles = 0;
        isOnPoint = false;
        GoTo(WanderingPoints[CurrentPointSelected].transform.position);
    }

    public void FindNewAngleToCheck()
    {
        CurrentTimeToSpent = Random.Range(MinTimeSpentOnPoint, MaxTimeSpentOnPoint);
        CurrentCheckingAnglePosition = WanderingPoints[CurrentPointSelected].transform.position + new Vector3(Random.Range(-WanderingPointRadius, WanderingPointRadius), 0, Random.Range(-WanderingPointRadius, WanderingPointRadius));
        haveCheckXAngles++;
        lastTimeAngleChecked = Time.time;
    }

    public void DoCombatMove()
    {
        didCombatMove = true;
        CurrentCombatMovePos = PLAYER.transform.position+ new Vector3(Random.Range(-radiusOfCombatEvolution, radiusOfCombatEvolution)*3, 0, Random.Range(-radiusOfCombatEvolution, radiusOfCombatEvolution)*3);
    }

    public void Attack()
    {
        float rdm = Random.Range(0, 100);
        if (rdm <= 50)
        {
            Anim.SetTrigger("verticalAttack");
            LastAttackType = AttackType.Vertical;
        }
        else
        {
            Anim.SetTrigger("horizontalAttack");
            LastAttackType = AttackType.Horizontal;
        }
        lastAttackDate = Time.time;
        CurrentAdditionalAttrackCooldown = Random.Range(0, AttackMaxAdditionnalCooldown);
        isAttacking = true;
        didCombatMove = false;
        PosWhenAttacking = PLAYER.transform.position;
        AttackKey = Random.Range(1, 10000);
    }

    public void CanWalk()
    {
        isAttacking = false;
        //Debug.Log("Attack done");
    }

    public int GetAttackKey()
    {
        return AttackKey;
    }

    public void Push(Vector3 direction, float force)
    {
        isPushed = true;
        
        NavAgent.Warp(transform.position+(direction*force*Time.deltaTime));
        currentPushDirection = direction;
        currentForce = force-(ForceFriction * Time.deltaTime);

        if (currentForce <= 0)
        {
            isPushed = false;
        }
    }
}
