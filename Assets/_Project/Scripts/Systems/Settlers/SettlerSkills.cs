/* Author  : Don MacSween
 * License : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Purpose : This script defines a singular skill that a settler can have. Skills are a prerequsite for certain tasks.
 *           e.g. only a settler with the 'Blacksmith' skill can make a shovel.
 */
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Settler
{

    public class SettlerSkills : MonoBehaviour
    {
        public SerializedDictionary<string, int> settlerSkills;


    }
}
