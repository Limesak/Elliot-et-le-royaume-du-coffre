using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootEntity : MonoBehaviour
{
    public LootManager.LootType Type;
    public float DetectionRadius;
    public bool isFollowing;
    private LootManager LM;
    public float SpeedIfFollowing;
    public RelevantEntity RE;

    void Start()
    {
        LM = GameObject.FindGameObjectWithTag("Player").GetComponent<LootManager>();
    }

    void Update()
    {
        if (isFollowing)
        {
            MoveTowardPlayer();
        }
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        if(Vector3.Distance(transform.position, LM.gameObject.transform.position) <= DetectionRadius)
        {
            LM.Loot(Type);
            if (RE != null)
            {
                RE.NotRelevantAnymore();
            }
            Destroy(gameObject);
        }
    }

    private void MoveTowardPlayer()
    {
        Vector3 direction = (LM.gameObject.transform.position  - transform.position).normalized;
        transform.position = transform.position + (direction* Time.deltaTime* SpeedIfFollowing);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);
    }
}
