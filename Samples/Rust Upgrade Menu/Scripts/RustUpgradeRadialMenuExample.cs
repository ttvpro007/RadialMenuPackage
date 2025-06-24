using System;
using RadialMenu;
using RadialMenu.Contracts;
using RadialMenu.Enums;
using RadialMenu.MenuTypes;
using UnityEngine;
using UnityEngine.UIElements;

namespace Rust_Upgrade_Menu
{
    public class RustUpgradeRadialMenuExample : MonoBehaviour
    {
        [SerializeField] private StyleSheet _additionalStyle;
        [SerializeField] private Sprite _demolishSprite;
        [SerializeField] private Sprite _woodSprite;
        [SerializeField] private Sprite _stoneSprite;
        [SerializeField] private Sprite _metalSprite;
        [SerializeField] private Sprite _hqmSprite;
        [SerializeField] private PanelSettings _panelSettings;
        private IRadialMenu _radialMenu;
        private IRadialMenuItem[] _items;
        
        private void Start()
        {
            _items = new IRadialMenuItem[]
            {
                new UpgradeOption("Upgrade to wood", "Upgrade the element to wood", _woodSprite, Color.red, Color.white),
                new UpgradeOption("Upgrade to stone", "Upgrade the element to stone", _stoneSprite, Color.red, Color.white),
                new UpgradeOption("Upgrade to metal", "Upgrade the element to metal", _metalSprite, Color.red, Color.white),
                new UpgradeOption("Upgrade to hqm", "Upgrade the element to hqm", _hqmSprite, Color.red, Color.white),
                new UpgradeOption("Demolish", "Demolish the element", _demolishSprite, Color.red, Color.white),
            };
            
            Color centerBackground = Color.black;
            centerBackground.a = 0.1f;
            _radialMenu = RadialMenuBuilder
                .Create(_panelSettings, _items)
                .WithVisibilityAnimationTime(0.1f)
                .WithAdditionalStyleSheet(_additionalStyle)
                .InScreenPosition(new Vector2(Screen.width * .5f, Screen.height * .5f))
                .WithMainOuterRadius(236, 240)
                .WithMainInnerRadius(150, 160)
                .WithMainColors(Color.white, Color.red, Color.white, Color.red)
                .WithCenterElementRadius(150)
                .WithCenterElementColors(centerBackground, centerBackground, centerBackground, centerBackground)
                .WithActionOnClickOutOfBounds(RadialMenuAction.PerformItemAndClose)
                .Build<ScaledRadialMenu>();
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                _radialMenu.Show();
            }
            else if (Input.GetMouseButtonUp(1))
            {
                _radialMenu.Hide();
            }
        }

        private void OnDestroy()
        {
            _radialMenu?.Destroy();
        }
    }
}