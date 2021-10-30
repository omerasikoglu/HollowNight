using UnityEngine;
using System;

public static class UtilsClass
{
    private static Camera mainCamera;

    public static Vector3 GetMouseViewportPosition() //sol-alt köþe 0.0f | sað-üst 1.0f | kamera hareket etse de hep böyle
    {                                                 
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mousePos = mainCamera.ScreenToViewportPoint(Input.mousePosition); //kameranýn sol altý 0 sað üstü çözünürlük
        mousePos.z = 0f;
        return mousePos;
    }
    public static Vector3 GetMouseWorldPosition() //sol-alt Dünyada neyse o -299 bile olabilir kamera konumu önemsiz
    {                                               //Dünyadaki 0,0,0 noktasý origin olur
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
    public static Vector3 GetMouseDirection(Transform bulletSource) //ok atarken gitceði yön vektörünü bulmak için
    {
        Vector3 mouseVector = GetMouseWorldPosition() - bulletSource.position;
        mouseVector.z = 0f;
        return mouseVector;
    }
    public static Vector3 GetNormalizeMouseDirection(Transform bulletSource) //ok atarken gitceði yön vektörünü bulmak için
    {
        Vector3 mouseVector = GetMouseDirection(bulletSource);
        mouseVector.Normalize();
        return mouseVector;
    }
    public static Vector3 GetMouseScreenPosition() //ayný Vector3'ten döndürmez hiç. ilk girdiðinde kameranýn sol altý 0,0 olur
    {                                                //70k'lara kadar çýkar ilerledikçe çok artar
        if (mainCamera == null) mainCamera = Camera.main;
        Vector3 mouseScreenPosition = Camera.main.WorldToScreenPoint(Input.mousePosition);
        mouseScreenPosition.z = 0;
        return mouseScreenPosition;
    }
    public static Vector3 GetRandomDir()    //x ekseninde random dir normalized
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    //nesneyi havaya fýrlatma
    public static Vector3 GetRandomUpDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), 1f).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector)  //okun yönü düþmana göre dönsün diye
    {
        float radians = Mathf.Atan2(vector.y, vector.x);    //diferansiyel.. en sevdiðim mmm..
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
