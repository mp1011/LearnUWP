namespace FinancialDucks.Models
{
    public abstract class FinancialEntity
    {
        public int ID { get; }
        public string Name { get; } 

        public decimal InitialAmount { get; }

        public override string ToString()
        {
            return Name;
        }

        protected FinancialEntity(int iD, string name, decimal initialAmount)
        {
            ID = iD;
            Name = name;
            InitialAmount = initialAmount;
        }
    }
}
