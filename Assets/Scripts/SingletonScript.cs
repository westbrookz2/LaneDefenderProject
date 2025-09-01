using UnityEngine;
using System.Collections;

public class Singleton<Instance> : MonoBehaviour where Instance : Singleton<Instance>
{
    public static Instance instance { get; set; }
    public bool IsSingleton = false;
    public bool IsPersistant = false;

    public virtual void Awake()
    {
        // If there can be only one...
        if (IsSingleton)
        {
            // Check if instance already exists
            if (!instance)
            {
                // Set initial instance
                instance = this as Instance;
            }
            else
            {
                // We don't want additional instances
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            // Set instance
            instance = this as Instance;
        }
        // Check if we want to persist this gameObject between loads
        if (IsPersistant)
        {
            // Make sure this object stays on reloads
            DontDestroyOnLoad(gameObject);
        }
    }
}