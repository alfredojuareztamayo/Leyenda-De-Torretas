using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormulesHAR
{
    //DISTANCIA
    public static float Dist(Vector3 pos1, Vector3 pos2)
    {
        float x = pos2.x - pos1.x;
        float y = pos2.y - pos1.y;
        float z = pos2.z - pos1.z;

        float xCuadrada = x * x;
        float yCuadrada = y * y;
        float zCuadrada = z * z;

        float dist = Mathf.Sqrt(xCuadrada + yCuadrada + zCuadrada);



        return dist;
    }

    public static float DistGO(GameObject pos1, GameObject pos2)
    {
        float x = pos2.transform.position.x - pos1.transform.position.x;
        float y = pos2.transform.position.y - pos1.transform.position.y;
        float z = pos2.transform.position.z - pos1.transform.position.z;

        float xCuadrada = x * x;
        float yCuadrada = y * y;
        float zCuadrada = z * z;

        float distGO = Mathf.Sqrt(xCuadrada + yCuadrada + zCuadrada);

        return distGO;
    }


    //NORMALIZACION DE VECTOR 3
    public static float Magnitud(Vector3 pos)
    {
        float xCuadrada = pos.x * pos.x;
        float yCuadrada = pos.y * pos.y;
        float zCuadrada = pos.z * pos.z;

        float mag = Mathf.Sqrt(xCuadrada + yCuadrada + zCuadrada);

        return mag;
    }

    public static Vector3 Normalize(Vector3 pos)
    {
        float x = pos.x / Magnitud(pos);
        float y = pos.y / Magnitud(pos);
        float z = pos.z / Magnitud(pos);

        return new Vector3(x, y, z);
    }


    //MATRIZ TRASLACION
    public Vector3 Traslacion(float trasladarX, float trasladarY, float trasladarZ, GameObject pos)
    {

        float mtX = trasladarX + pos.transform.position.x;
        float mtY = trasladarY + pos.transform.position.y;
        float mtZ = trasladarZ + pos.transform.position.z;

        
        return new Vector3(mtX, mtY, mtZ);
    }

}
