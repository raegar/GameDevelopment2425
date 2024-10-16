/* Author(s)    : Don MacSween
 * email(s)     : dm1200@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 11/10/2024
 * Purpose      : This script is a temporary test script to test the settler factory and settler creation.
 */
using UnityEngine;
using SettlerSystem;
public class settlerTestScript : MonoBehaviour
{
    private GameObject settlerGO;
    [SerializeField] private int numberOfsettlers = 5;
    void Start()
    {
        for (int i = 0; i < numberOfsettlers; i++)
        {
            settlerGO = SettlerFactory.Instance.Create();
        }
    }
}
