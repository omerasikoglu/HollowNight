using UnityEngine;
using System;

public static class UtilsClass
{
    private static Camera mainCamera;
    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); //kameranýn ortasýný (0,0,0) yapar
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }
    public static Vector3 GetMouseDirection(Transform bulletSource) //ok atarken gitceði yön vektörünü bulmak için
    {
        Vector3 mouseVector = GetMouseWorldPosition() - bulletSource.position;
        mouseVector.z = 0f;
        return mouseVector;
    }
    public static Vector3 GetMouseScreenPosition()
    {
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
