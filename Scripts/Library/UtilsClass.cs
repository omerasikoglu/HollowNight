using UnityEngine;
using System;

public static class UtilsClass
{
    private static Camera mainCamera;

    public static Vector3 GetMouseViewportPosition() //sol-alt k��e 0.0f | sa�-�st 1.0f | kamera hareket etse de hep b�yle
    {                                                 
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mousePos = mainCamera.ScreenToViewportPoint(Input.mousePosition); //kameran�n sol alt� 0 sa� �st� ��z�n�rl�k
        mousePos.z = 0f;
        return mousePos;
    }
    public static Vector3 GetMouseWorldPosition() //sol-alt D�nyada neyse o -299 bile olabilir kamera konumu �nemsiz
    {                                               //D�nyadaki 0,0,0 noktas� origin olur
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
    public static Vector3 GetMouseDirection(Transform bulletSource) //ok atarken gitce�i y�n vekt�r�n� bulmak i�in
    {
        Vector3 mouseVector = GetMouseWorldPosition() - bulletSource.position;
        mouseVector.z = 0f;
        return mouseVector;
    }
    public static Vector3 GetNormalizeMouseDirection(Transform bulletSource) //ok atarken gitce�i y�n vekt�r�n� bulmak i�in
    {
        Vector3 mouseVector = GetMouseDirection(bulletSource);
        mouseVector.Normalize();
        return mouseVector;
    }
    public static Vector3 GetMouseScreenPosition() //ayn� Vector3'ten d�nd�rmez hi�. ilk girdi�inde kameran�n sol alt� 0,0 olur
    {                                                //70k'lara kadar ��kar ilerledik�e �ok artar
        if (mainCamera == null) mainCamera = Camera.main;
        Vector3 mouseScreenPosition = Camera.main.WorldToScreenPoint(Input.mousePosition);
        mouseScreenPosition.z = 0;
        return mouseScreenPosition;
    }
    public static Vector3 GetRandomDir()    //x ekseninde random dir normalized
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    //nesneyi havaya f�rlatma
    public static Vector3 GetRandomUpDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), 1f).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector)  //okun y�n� d��mana g�re d�ns�n diye
    {
        float radians = Mathf.Atan2(vector.y, vector.x);    //diferansiyel.. en sevdi�im mmm..
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

    // Returns 0-255
    public static int Hex_to_Dec(string hex)
    {
        return Convert.ToInt32(hex, 16);
    }
    // Returns a float between 0->1
    public static float Hex_to_Dec01(string hex)
    {
        return Hex_to_Dec(hex) / 255f;
    }
    // Get Color from Hex string FF00FFAA
    public static Color GetColorFromString(string color)
    {
        float red = Hex_to_Dec01(color.Substring(0, 2));
        float green = Hex_to_Dec01(color.Substring(2, 2));
        float blue = Hex_to_Dec01(color.Substring(4, 2));
        float alpha = 1f;
        if (color.Length >= 8)
        {
            // Color string contains alpha
            alpha = Hex_to_Dec01(color.Substring(6, 2));
        }
        return new Color(red, green, blue, alpha);
    }
}
