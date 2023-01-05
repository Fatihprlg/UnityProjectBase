using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
#if UNITY_EDITOR


    public class EditorButtonView : MethodAttributeViewModel
    {
        public List<ButtonView> RendererButtons;

        public override void CheckMethod(MethodInfo method)
        {
            object[] attributeDatas = method.GetCustomAttributes(typeof(EditorButton), true);

            if (attributeDatas.Length > 0)
            {
                if (RendererButtons == null)
                    RendererButtons = new List<ButtonView>();
            }

            for (int j = 0; j < attributeDatas.Length; j++)
            {
                RendererButtons.Add(new ButtonView(method));
            }

            base.CheckMethod(method);
        }

        public override void Draw(Object target)
        {
            if (RendererButtons == null)
                return;

            for (int i = 0; i < RendererButtons.Count; i++)
            {
                RendererButtons[i].Draw(target);
            }

            base.Draw(target);
        }
    }

#endif
