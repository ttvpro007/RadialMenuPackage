namespace RadialMenu.Contracts
{
    public interface IRadialMenuItem
    {
        public IRadialMenuItemElement CreateItemElement();
        public IRadialMenuItemCenterElement CreateItemCenterElement();
        
        public void OnItemPerform();
    }
}