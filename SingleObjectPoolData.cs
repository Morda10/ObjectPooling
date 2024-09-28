using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Create It with clicking right mouse button and choosing Object Pools/NewSingleObjectPoolData
*/
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewSingleObjectPoolData", menuName = "Object Pools/NewSingleObjectPoolData")]
    public class SingleObjectPoolData : ScriptableObject
    {
        public PrefabPool pool;
    }
}
