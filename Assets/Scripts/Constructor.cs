using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction
{

    GameObject meshObject;
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

    public static void createConstruction(ConstructionType type, int level, RaycastHit hit)
    {
        if (isValidPoint(hit))
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Transform constructions = hit.transform.Find("Constructions");
            if (!constructions)
            {
                GameObject constructionsObj = new GameObject("Constructions");
                constructions = constructionsObj.transform;
                constructions.parent = hit.transform;
            }

            cube.transform.parent = constructions;
            cube.transform.localScale = cube.transform.localScale * 0.25f;

            Vector3 elongate = cube.transform.localScale;
            elongate.y *= 2;

            cube.transform.localScale = elongate;

            cube.transform.position = hit.point;

            cube.transform.rotation = toLocalUp(cube.transform, hit);
        }
    }
}

public enum ConstructionType
{
    COMMUNICATIONS
}
