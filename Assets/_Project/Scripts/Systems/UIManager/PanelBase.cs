/* Author(s)    : Don MacSween & Jess Woodward
 * email(s)     : dm1200@student.aru.ac.uk & jw1519@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 26/10/2024
 * Purpose      :This script is designed as a abstract base class for all UI panels to inherit from.
 *               Simply by inheriting from this class, the panel will be registered with the UIManager.cs singleton
 *               provided that if child class uses the Awake method you call the base.Awake method
 */
using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
    /// <summary>
    /// The Unity Awake method used for initialisation
    /// </summary>
    protected virtual void Awake()
    {
        // registers the panel with the UIManager so it can be opened and closed from any script.
        UIManager.Instance.Register(this);
    }
}