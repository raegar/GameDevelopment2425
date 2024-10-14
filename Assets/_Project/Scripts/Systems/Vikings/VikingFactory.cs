/* Author       : Don MacSween
 * email        : dm1200@student.aru.ac.uk
 * License      : CC BY 4.0 https://creativecommons.org/licenses/by/4.0/
 * Last Modified: 11/10/2024
 * Purpose : This script is designed as sole method that components can use to create a settler.
 *           The create method can be overloaded to allow for different configurations of settler to be created.
 *           This class follows the factory design pattern which constructs a complex object behind a common interface
 *           and returns the object as a product of the factory.
 */

using UnityEngine;

namespace Vikings
{
    public class VikingFactory : Singleton
    {
        // source of Viking randomization
        [SerializeField] private VikingRandomizer vikingRandomizer;

        /// <summary>
        /// As a factory this should be the only method to create a new viking in the game 
        /// </summary>
        /// <param name="familyID">a value > 0 was born in base</param>
        /// <returns>a new viking object</returns>
        public Viking Create(int familyID = 0)
        {
            private Viking viking = new Viking();
            viking.gender       = GenerateRandomisedGender();
            viking.foreName     = GenerateRandomisedForeName(viking.gender);
            viking.surName      = GenerateRandomisedSurName();
        }
    
    /// <summary>
    /// Polymorphic override of the create method to allow for a viking to be created with a specific social status
    /// </summary>
    /// <param name="socialStatus"></param>
    /// <returns></returns>
    public Viking Create(SocialStatus socialStatus)
        {

        }

    private Gender GenerateRandomisedGender()
        {
            if (Random.Range(0, 100) < vikingRandomizer.genderCreationBias)
            {

            }
        }
    private SocialStatus GenerateRandomisedSocialStatus()
        {

            return SocialStatus.Karl;

        }
    private string GenerateRandomisedForeName(Gender gender)
        {
            if gender.Male
             {
                return "Eric";
            }
            else
            {
                return "Helga";
            }
        }
    private string GenerateRandomisedSurName()
        {
            return "Viking";
        }
    }
}
      
  

