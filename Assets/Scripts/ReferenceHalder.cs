using UnityEngine;

public class ReferenceHalder : MonoBehaviour
{
   public SimpleComponent simpleComponentReference;
   [HideInInspector]
   public SimpleComponent simpleComponentReferenceCachedOnAwake;

   private void Awake()
   {
      simpleComponentReferenceCachedOnAwake = gameObject.GetComponent<SimpleComponent>();
   }
}
