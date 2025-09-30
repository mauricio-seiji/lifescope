using UnityEngine;

public class PinkCellNucleus : PinkCoccus
{
    void Update()
    {
        BaseLifeUpdate();
        MoveAfterCoolDown();
    }
}
