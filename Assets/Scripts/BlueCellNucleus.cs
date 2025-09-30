using System;
using UnityEngine;

public class BlueCellNucleus : BaseLife
{
    private bool onCoolDown = true;
    private int CoolDownCounter = 0;
    [SerializeField] private int CoolDownDuration = 2000;


    void Update()
    {
        BaseLifeUpdate();

        MoveAfterCoolDown();
    }
    public void MoveAfterCoolDown()
    {
        if (onCoolDown)
        {
            CoolDownCounter++;
            if (CoolDownCounter >= CoolDownDuration)
            {
                onCoolDown = false;
                CoolDownCounter = 0;
            }
        }
        else
        {
            IMoveTowards moveTowards = new MoveTowards();
            Vector3 newPosition = moveTowards.MoveTowardsClosestTarget("Green Cell", transform.position, speed);
            if (newPosition != Vector3.zero)
                transform.position = newPosition;

        }
    }
}
