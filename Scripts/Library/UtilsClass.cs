using UnityEngine;

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
    public static Vector3 GetRandomDir()    //x ekseninde random dir
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    //nesneyi havaya fýrlatma
    public static Vector3 GetRandomUpDir()
    {
        return new Vector3(Random.Range(-1f, 1f), 1f).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector)  //okun yönü düþmana göre dönsün diye
    {
        float radians = Mathf.Atan2(vector.y, vector.x);    //diferansiyel.. en sevdiðim mmm..
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }
}
