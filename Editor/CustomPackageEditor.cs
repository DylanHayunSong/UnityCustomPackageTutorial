using UnityEditor;

public class CustomPackageEditor : Editor
{
    [MenuItem("CustomPackageSample/Test")]
    public static void Test() 
    {
        CustomPackageScript.DebugText("This message called Editor Script!");
    }
}