namespace InventorySystem
{
    public interface IStackable
    {
        public void AddToStack(int amountToAdd, int currentAmount, int maxAmount);
    }
}
