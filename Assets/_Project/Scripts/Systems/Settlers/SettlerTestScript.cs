using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Settler;

public class settlerTestScript : MonoBehaviour
{
    public Settler.Settler settler;
    public int numberOfsettlers = 5;
   
    void Start()
    {
        for (int i = 0; i < numberOfsettlers; i++)
        {
            settler = SettlerFactory.Instance.Create();
            Debug.Log("Settler: "+ numberOfsettlers);
            Debug.Log(settler.foreName);
            Debug.Log(settler.surName);

        }
        Debug.Log(settler.foreName);
        Debug.Log(settler.surName);
    }

}
