namespace Model
{
    public class CharacteristicValue:Entity<int>
    {
        public static class MaxLengthFor
        {
            public static int Value = 100;
        }


        #region Properties

        public string Value { get; set; }

        #endregion


        #region Navigation properties

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int CharacteristicId { get; set; }

        public virtual Characteristic Characteristic { get; set; }

        #endregion


        #region Methods

        public void CopyFrom(CharacteristicValue characteristicValue)
        {
            Value = characteristicValue.Value;
            ProductId = characteristicValue.ProductId;
            Product = characteristicValue.Product;
            CharacteristicId = characteristicValue.CharacteristicId;
            Characteristic = characteristicValue.Characteristic;
        }

        #endregion

    }
}
