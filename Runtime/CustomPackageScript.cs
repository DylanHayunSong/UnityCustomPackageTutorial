using UnityEngine;

public class CustomPackageScript : MonoBehaviour
{
    public static void DebugText(string msg)
    {
        Debug.Log(string.Format("{0} \n Package Successfully Loaded!", msg));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DebugText("This message called Runtime Script!");
        }
    }
}
