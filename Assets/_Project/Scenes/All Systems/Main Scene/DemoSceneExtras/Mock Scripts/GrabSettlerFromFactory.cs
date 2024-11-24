using SettlerSystem;
using UnityEngine;
using TMPro;

namespace SettlerSystem
{
    public class GrabSettlerFromFactory : MonoBehaviour
    {
        [SerializeField] private Gender gender;
        [SerializeField] private string foreName, surName;
        [SerializeField] private SystemType systemSelection;
        
        public enum SystemType
        {
            SettlerFactory,
            SettlerFactoryPatronymics
        }

        private TextMeshPro textMeshPro;
        private Settler thisSettler;

        private void Start()
        {
            textMeshPro = GetComponentInChildren<TextMeshPro>();
            if (textMeshPro == null)
            {
                Debug.LogError("TextMeshPro component not found on this object");
            }
            
            switch (systemSelection)
            {
                case SystemType.SettlerFactory:
                    thisSettler = SettlerFactory.Instance.Create().GetComponent<Settler>();
                    break;
                case SystemType.SettlerFactoryPatronymics:
                    thisSettler = SettlerFactoryPatronymics.Instance.CreateCustom(SocialStatus.Unassigned, 0, true, gender).GetComponent<Settler>();
                    break;
            }

            thisSettler.transform.parent = this.transform;

            foreName = thisSettler.forename;
            surName = thisSettler.surname;
            textMeshPro.text = $"{foreName} {surName}";
        }
    }
}
