using SettlerSystem;
using UnityEngine;
using TMPro;

namespace SettlerSystem
{
    public enum NameSystemType
    {
        SettlerFactory,
        SettlerFactoryPatronymics
    }
    public class GrabSettlerFromFactory : MonoBehaviour
    {
        public Gender gender;

        public string foreName { get; private set; }
        public string surName { get; private set; } 

        [SerializeField] private NameSystemType systemSelection;
        [SerializeField] private bool randomFather;

        private TextMeshPro textMeshPro;
        private Settler thisSettler;

        public void GetName()
        {
            textMeshPro = GetComponentInChildren<TextMeshPro>();
            if (textMeshPro == null)
            {
                Debug.LogError("TextMeshPro component not found on this object");
            }

            switch (systemSelection)
            {
                case NameSystemType.SettlerFactory:
                    thisSettler = SettlerFactory.Instance.Create().GetComponent<Settler>();
                    break;
                case NameSystemType.SettlerFactoryPatronymics:
                    thisSettler = SettlerFactoryPatronymics.Instance.CreateCustom(SocialStatus.Unassigned, 0, true, gender, randomFather).GetComponent<Settler>();
                    break;
            }

            thisSettler.transform.parent = this.transform;

            foreName = thisSettler.forename;
            surName = thisSettler.surname;
            textMeshPro.text = $"{foreName} {surName}";
        }

        public void ChangeNameSystem()
        {
            switch (systemSelection)
            {
                case NameSystemType.SettlerFactory:
                    systemSelection = NameSystemType.SettlerFactoryPatronymics;
                    break;
                case NameSystemType.SettlerFactoryPatronymics:
                    systemSelection = NameSystemType.SettlerFactory;
                    break;
            }
        }

        public void ChangeNameSystem(NameSystemType systemType)
        {
            systemSelection = systemType;
        }
    }
}
