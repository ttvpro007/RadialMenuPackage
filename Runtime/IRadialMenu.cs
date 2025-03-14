using UnityEngine;
using UnityEngine.UIElements;

namespace RadialMenu
{
    public interface IRadialMenu
    {
        internal void Initialize(PanelSettings defaultPanelSettings, StyleSheet baseStyleSheet, RadialMenuSettings radialMenuSettings);

        public void Show();
        public void Hide();
        public void SetScreenPosition(Vector2 position);

        public static void Test(){}
    }
}