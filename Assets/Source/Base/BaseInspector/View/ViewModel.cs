using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR


    public class AttributeViewModel
    {
        public virtual TargetAttributeTypes GetTargetType()
        {
            return TargetAttributeTypes.Method;
        }

        public virtual void CheckMethod(System.Reflection.MethodInfo method)
        {

        }

        public virtual void CheckField(System.Reflection.FieldInfo field, object target)
        {

        }

        public virtual void Draw(Object target)
        {
          
        }
    }

    public class MethodAttributeViewModel : AttributeViewModel
    {

        public override TargetAttributeTypes GetTargetType()
        {
            return TargetAttributeTypes.Method;
        }

    }

    public class FieldAttributeViewModel : AttributeViewModel
    {
        public override TargetAttributeTypes GetTargetType()
        {
            return TargetAttributeTypes.Field;
        }
    }

    public enum TargetAttributeTypes
    {
        Method,
        Field
    }

#endif
