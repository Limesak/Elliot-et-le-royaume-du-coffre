using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Magifolien : MonoBehaviour
{
    public enum AttackType { Vertical, Horizontal };

    [Header("Inspector Info")]
    public DamageCentralizer DC;
    public Animator Anim;
    private GameObject PLAYER;
    public NavMeshAgent NavAgent;
    public LockableObject LO;
    public RelevantEntity RE;
    public SoundManager_Poussierin SM;

    public GameObject Prefab_Piece;
    public GameObject Prefab_Bonbon;
    public GameObject Prefab_CleJaune;

    [Header("Global Info")]
    public int TableIndex;
    public bool RespawnAfterReload;
    public int HPmax;
    private int HP;//HPmax
    private bool isDead;//false

    [Header("Achivements")]
    public bool FirstMagifolien;
    public bool FirstInvocateur;

    [Header("Animation")]
    private int CurrentIndexPTLA;
    public GameObject PREFAB_Hit;
    public GameObject PREFAB_HitSword;

    [Header("Deplacement & Targets")]
    private Vector3 LastPositionSeen;
    public GameObject[] WanderingPoints;
    private int CurrentPointSelected;
    public float WanderingPointRadius;
    public float SPEED;
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
    public float TauntDuration;
    private float lastDateTaunt;


    [Header("Attacks & Combat")]
    public float MinDistToHit;
    public float MaxDistBeforeFlight;
    public float FlightCooldown;
    private float LastFlightDate;
    public GameObject OrbeMagique;
    public float AttackBaseCooldown;
    public float AttackMaxAdditionnalCooldown;
    private float CurrentAdditionalAttackCooldown;
    private float lastAttackDate;
    public float StunDuration;
    private float lastTimeDamageTaken;
    public float speedOfRotation;
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

    //public GameObject DebugPosAccessing;

    void Start()
    {
        HP = HPmax;
        currentForce = 0;
        isAgressive = false;
        isPushed = false;
        currentPushDirection = Vector3.zero;
        PLAYER = GameObject.FindGameObjectWithTag("Player");
        transform.localScale = transform.localScale * Random.Range(0.85f, 1.2f);
        LastFlightDate = -FlightCooldown-5;
    }

    void Update()
    {
        CheckHP();
        if (isPushed)
        {
            Push(currentPushDirection, currentForce);
        }
        if (!isDead)
        {
            if (isAgressive && LastFlightDate + 5 <= Time.time)
            {
                if (lastTimeDamageTaken + StunDuration <= Time.time)//Not stun    isFree
                {
                    if (lastDateTaunt + TauntDuration <= Time.time) //isFree
                    {
                        if (isAttacking)
                        {
                            LookAt(PosWhenAttacking);
                            StopWalking();
                            //Debug.Log("isAttacking");
                        }
                        else//not Attacking
                        {
                            if (CanSeePlayer())
                            {
                                if (Vector3.Distance(transform.position, PLAYER.transform.position) <= MinDistToHit && Vector3.Distance(transform.position, PLAYER.transform.position) >= MaxDistBeforeFlight && LastFlightDate + 5 <= Time.time)// Close enought to hit
                                {
                                    if (lastAttackDate + CurrentAdditionalAttackCooldown + AttackBaseCooldown <= Time.time)// can hit
                                    {
                                        LookAt(PLAYER.transform.position);
                                        //FRAPPER LE JOUEUR
                                        StopWalking();
                                        Attack();
                                    }
                                    else// can't hit yet
                                    {
                                        LookAt(PLAYER.transform.position);
                                        StopWalking();
                                    }
                                }
                                else if (Vector3.Distance(transform.position, PLAYER.transform.position) <= MaxDistBeforeFlight)
                                {
                                    if (LastFlightDate + FlightCooldown <= Time.time)
                                    {
                                        Debug.Log("Start flight");
                                        LastFlightDate = Time.time;
                                        StopWalking();
                                        if (lastTimePlayerSeen + maxSearchDuration <= Time.time)
                                        {
                                            isOnPoint = false;
                                            isAgressive = false;
                                            FindNewWanderingPoint();
                                        }
                                    }
                                    
                                }
                                else if(LastFlightDate + 5 <= Time.time)// Not close enough to hit
                                {
                                    NavMeshPath navMeshPath = new NavMeshPath();
                                    if (NavAgent.CalculatePath(PLAYER.transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
                                    {
                                        GoTo(PLAYER.transform.position);
                                        LookAt(NavAgent.steeringTarget);
                                    }
                                    else
                                    {
                                        isOnPoint = false;
                                        isAgressive = false;
                                        FindNewWanderingPoint();
                                    }

                                    //AVANCER VERS LE JOUEUR
                                }
                            }
                            else // can't see player
                            {


                                //AVANCE VERS DERNIERE POSITION CONNU
                                //CHECK SI A PORTE DE LA LASTPOS ET SI SA FAIT LONGTEMP QU'IL CHERCHE
                                if (Vector3.Distance(transform.position, LastPositionSeen) <= MinDistToHit)
                                {
                                    StopWalking();
                                    if (lastTimePlayerSeen + maxSearchDuration <= Time.time)
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
                    NavMeshPath navMeshPath = new NavMeshPath();
                    if (CanSeePlayer() && LastFlightDate + 5 <= Time.time/*&& NavAgent.CalculatePath(PLAYER.transform.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete*/)
                    {
                        StopWalking();
                        Taunt();
                    }
                    else
                    {
                        if (isOnPoint)
                        {
                            if (lastTimeAngleChecked + CurrentTimeToSpent <= Time.time)// Check last angle is done
                            {
                                if (haveCheckXAngles >= CurrentAngleVerifiedOnSpot)//this point is done
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
                                }
                            }
                        }
                        else //Going on new point
                        {
                            if (Vector3.Distance(transform.position, WanderingPoints[CurrentPointSelected].transform.position) <= WanderingPointRadius) // arrived on point
                            {
                                isOnPoint = true;
                                //CHECKER LES ANGLES
                                FindNewAngleToCheck();
                            }
                            else// getting on point
                            {
                                //ALLER VERS LE POINT
                                GoTo(WanderingPoints[CurrentPointSelected].transform.position);
                                LookAt(NavAgent.steeringTarget);
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
            Push(Direction, HitPushForce);
            lastTimeDamageTaken = Time.time;

            if (HP > 0 && !isDead && (!isAttacking || isAttacking && LastAttackType != AttackType.Vertical))
            {
                Anim.SetTrigger("Hit");
                isAttacking = false;
                //SM.PlaySound(SM.OneOf(SM.VOCAL_Hit), SM.Asource_Effects, 1, true);
            }
            else if (HP <= 0 && !isDead)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            /*
            if (AreneTuto)
            {
                SaveData.current.Achievements_AreneTuto = true;
                PLAYER.GetComponent<DiaryManager>().ChangeTheMission("Rejoindre Lèche Cuillère.");
                PLAYER.GetComponent<DiaryManager>().AddBufferEntry("J'ai combattu et vaincu mon premier poussierin !");
                SaveData.current.Codex_Bestiaire_unlockList[1] = true;
                PLAYER.GetComponentInChildren<NotifManager>().NewNotif("Nouvelle fiche codex!");
            }
            */
            isDead = true;
            Anim.SetTrigger("Die");
            StopWalking();
            PLAYER.GetComponent<DiaryManager>().AddAKill(1);
            if (RE != null && !RespawnAfterReload)
            {
                RE.NotRelevantAnymore();
            }
            LO.Die();
            transform.DOScale(transform.localScale * 0.95f, 2f).OnComplete(() => { DiePartTwo(); });
            //SM.PlaySound(SM.OneOf(SM.VOCAL_Die), SM.Asource_Effects, 1, true);
        }
    }

    public void DiePartTwo()
    {
        transform.DOScale(Vector3.zero, 0.8f).OnComplete(() => { DiePartThree(); });
    }

    public void DiePartThree()
    {
        float rdm = Random.Range(0, 100);
        if (rdm > 95)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(Prefab_Piece, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 1 + Random.Range(-0.1f, 0.2f), Random.Range(-0.5f, 0.5f)), Quaternion.identity);
            }
        }
        else if (rdm > 50)
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(Prefab_Piece, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 1 + Random.Range(-0.1f, 0.2f), Random.Range(-0.5f, 0.5f)), Quaternion.identity);
            }
        }
        else
        {
            Instantiate(Prefab_Piece, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 1 + Random.Range(-0.1f, 0.2f), Random.Range(-0.5f, 0.5f)), Quaternion.identity);
        }

        float rdm2 = Random.Range(0, 100);
        bool hadSpawnBonbon = false;
        switch (SaveData.current.CurrentDifficulty)
        {
            case 0:
                if (PLAYER.GetComponent<LifeManager>().GetCurrentIndex() >= 1 && rdm2 > 30)
                {
                    Instantiate(Prefab_Bonbon, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    hadSpawnBonbon = true;
                }
                break;
            case 1:
                if (PLAYER.GetComponent<LifeManager>().GetCurrentIndex() >= 2 && rdm2 > 50)
                {
                    Instantiate(Prefab_Bonbon, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    hadSpawnBonbon = true;
                }
                break;
            case 2:
                if (PLAYER.GetComponent<LifeManager>().GetCurrentIndex() >= 2 && rdm2 > 50)
                {
                    Instantiate(Prefab_Bonbon, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    hadSpawnBonbon = true;
                }
                break;
            case 3:
                if (PLAYER.GetComponent<LifeManager>().GetCurrentIndex() >= 2 && rdm2 > 70)
                {
                    Instantiate(Prefab_Bonbon, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    hadSpawnBonbon = true;
                }
                break;
            case 4:
                if (PLAYER.GetComponent<LifeManager>().GetCurrentIndex() == 3 && rdm2 > 70)
                {
                    Instantiate(Prefab_Bonbon, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    hadSpawnBonbon = true;
                }
                break;
            default:
                //Debug.Log("NOTHING");
                break;
        }

        float rdm3 = Random.Range(0, 100);
        if (!hadSpawnBonbon)
        {
            if (SaveData.current.CPT_YellowKey == 0)
            {
                if (rdm3 > 80)
                {
                    Instantiate(Prefab_CleJaune, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                }
            }
            else
            {
                if (rdm3 > 90)
                {
                    Instantiate(Prefab_CleJaune, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                }
            }
        }
        Destroy(gameObject);
    }

    public bool CanSeePlayer()
    {
        bool res = false;
        Vector3 direction = (PLAYER.transform.position + new Vector3(0, YLookingOffset, 0)) - transform.position;
        RaycastHit hit;
        Debug.DrawRay(Eye.transform.position, direction, Color.green);
        if (Physics.Raycast(Eye.transform.position, direction, out hit))
        {
            if (hit.collider.gameObject.tag == "Player" && Vector3.Distance(PLAYER.transform.position, transform.position) <= minDistToDetect)
            {
                LastPositionSeen = PLAYER.transform.position;
                lastTimePlayerSeen = Time.time;
                res = true;
            }
            //Debug.Log("Seeing " + hit.collider.gameObject.name + " with tag "+hit.collider.gameObject.tag);
        }

        if (Vector3.Distance(PLAYER.transform.position, transform.position) <= minDistToDetect / 3)
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

    public void Taunt()
    {
        isAgressive = true;
        Anim.SetTrigger("Hit");
        lastDateTaunt = Time.time;
    }

    public void GoTo(Vector3 Position)
    {
        if (!isAttacking && !isDead)
        {
            NavAgent.isStopped = false;
            Anim.SetBool("isRunning", true);
            NavAgent.speed = SPEED;
            NavAgent.destination = Position;
            //DebugPosAccessing.transform.position = Position;
        }
    }

    public void StopWalking()
    {
        NavAgent.destination = transform.position;
        NavAgent.isStopped = true;
        Anim.SetBool("isRunning", false);
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


    public void Attack()
    {
        Anim.SetTrigger("AttackSide");

        lastAttackDate = Time.time;
        CurrentAdditionalAttackCooldown = Random.Range(0, AttackMaxAdditionnalCooldown);
        isAttacking = true;
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

        NavAgent.Warp(transform.position + (direction * force * Time.deltaTime));
        currentPushDirection = direction;
        currentForce = force - (ForceFriction * Time.deltaTime);

        if (currentForce <= 0)
        {
            isPushed = false;
        }
    }
}
