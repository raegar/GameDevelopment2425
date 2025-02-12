using UnityEngine;

namespace PatternLibrary
{   // As this is a base class we wont know the type of child classes until they are implimented so we substitute with T
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public bool persistentAcrossScenes = true;
        protected static T instance;
        public static T Instance
        {
            get
            {   // This should only occur prior to the InitializeSingleton method being called
                if (instance == null)
                {
                    // Try to find an instance of the object in the scene
                    instance = FindAnyObjectByType<T>();
                    // if another script is calling for a singleton of type T and none exists in the scene then create one
                    if (instance == null)
                    {
                        var go = new GameObject(typeof(T).Name + " Auto-Generated");
                        // This is not ideal as we may want to do some editing or setting properties prior to use
                        Debug.Log(typeof(T).Name + " was not present in the scene");
                        // adds the singleton script component to the newly created GameObject
                        instance = go.AddComponent<T>();
                    }
                }
                return instance;
            }
            // other scripts modifying the Instance could cause errors - so prevent it.
            private set { }
        }

        /// <summary>
        /// Child objects wishing to use the Awake method will need to override it 
        /// and call base.Awake()
        /// </summary>
        protected virtual void Awake()
        {
            InitializeSingleton();
        }
        /// <summary>
        /// Initialises the singleton and ensures a single instance of it
        /// </summary>
        protected virtual void InitializeSingleton()
        {
            // Some Editor scripts can adversely affect this - so make sure the game is running
            if (!Application.isPlaying) return;
            // Likely that this is the first setup of the instance so set it to 'this' object
            if (instance == null)
            {
                instance = this as T;
                // optionally allow the singleton to persist across scene changes
                if (persistentAcrossScenes)
                {
                    // persistant objects can only be set on unparented objects in the scene
                    transform.SetParent(null);
                    // be aware of other objects the singleton may use being dereferenced in scene changes
                    DontDestroyOnLoad(gameObject);
                }
            }
            // if there is already an instance of this single-ton then destroy 'this' object to prevent duplicates 
            else if (instance != this) Destroy(gameObject);
        }
    }
}