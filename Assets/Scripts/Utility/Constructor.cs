using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Construction
{

    private static Object satelliteDish = (Object)Resources.Load("Prefabs/Buildings/Dish");

    private static Quaternion toLocalUp(Transform target, RaycastHit hitPoint)
    {
        // Could probably just use hit.normal
        Vector3 gravityUp = hitPoint.normal.normalized;
        Vector3 localUp = hitPoint.transform.up;
        return Quaternion.FromToRotation(localUp, gravityUp) * target.rotation;
    }

    private static bool isValidPoint(RaycastHit hit)
    {
        if (hit.transform.tag == "Planet")
        {
            Texture2D validMap = Resources.Load($"validMaps/{hit.transform.gameObject.name}", typeof(Texture2D)) as Texture2D;
            Vector2 pixelUV = hit.textureCoord;

            pixelUV.x *= validMap.width;
            pixelUV.y *= validMap.height;

            Color pixelColour = validMap.GetPixel(Mathf.FloorToInt(pixelUV.x), Mathf.FloorToInt(pixelUV.y));

            return pixelColour == Color.black;
        }

        return false;
    }

    private static GameObject GetModel(ConstructionType type, int level)
    {
        switch (type)
        {
            case ConstructionType.COMMUNICATIONS:
                GameObject gO = (GameObject)GameObject.Instantiate(satelliteDish, Vector3.zero, Quaternion.identity);
                return gO;
        }

        return null;
    }

    public static void createConstruction(ConstructionType type, int level, RaycastHit hit)
    {
        if (isValidPoint(hit))
        {
            GameObject model = GetModel(type, level);

            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Transform constructions = hit.transform.Find("Constructions");
            if (!constructions)
            {
                GameObject constructionsObj = new GameObject("Constructions");
                constructions = constructionsObj.transform;
                constructions.parent = hit.transform;
            }

            model.transform.parent = constructions;
            model.transform.position = hit.point;
            model.transform.rotation = toLocalUp(model.transform, hit);
        }
    }
}

public enum ConstructionType
{
    COMMUNICATIONS
}
