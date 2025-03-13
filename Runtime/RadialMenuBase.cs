using RadialMenu.Elements;
using UnityEngine;
using UnityEngine.UIElements;

namespace RadialMenu
{
    public abstract class RadialMenuBase : IRadialMenu
    {
        private readonly UIDocument _document;
        private readonly RadialMenuRoot _root;

        internal RadialMenuBase(PanelSettings panelSettings, StyleSheet baseStyleSheet, RadialMenuSettings settings)
        {
            GameObject documentObj = new GameObject("Radial menu");
            _document = documentObj.AddComponent<UIDocument>();
            _document.panelSettings = panelSettings;
            _document.rootVisualElement.styleSheets.Add(baseStyleSheet);

            _document.rootVisualElement.Add(_root = new RadialMenuRoot(settings));
        }

        ~RadialMenuBase()
        {
            Destroy();
        }

        public void Destroy()
        {
            if (_document && _document.gameObject)
                GameObject.Destroy(_document.gameObject);
        }
    }
}