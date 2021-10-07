using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Problem_6 : MonoBehaviour
{
    public GameObject boxPf;
    public int boxAmount;
    public float areaConstraintValue;

    private void Start()
    {
        for (int i = 0; i < boxAmount; i++)
        {
            GameObject box = Instantiate(boxPf, GetRandomPosition(), Quaternion.identity);
            box.transform.localScale = GetRandomScale();
            box.SetActive(true);
        }
    }

    public Vector2 GetRandomScale()
    {
        float xScale = Random.Range(0.45f, 1f);
        float yScale = Random.Range(0.5f, 1f);
        return new Vector2(xScale, yScale);
    }

    public Vector2 GetRandomPosition()
    {
        float xPosition = Random.Range(-areaConstraintValue, areaConstraintValue);
        float yPosition = Random.Range(-areaConstraintValue, areaConstraintValue);

        return new Vector2(xPosition, yPosition);
    }
}
