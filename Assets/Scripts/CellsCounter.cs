using UnityEngine;

public class CellsCounter : MonoBehaviour
{
    void Update()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Cell");
        GlobalVariables.cellsCount = allObjects.Length;
    }
}