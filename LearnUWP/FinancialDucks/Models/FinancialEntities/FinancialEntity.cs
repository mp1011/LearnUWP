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

        public override bool Equals(object obj)
        {
            if(obj is FinancialEntity other)
                return ID == other.ID && GetType() == other.GetType();
            else 
                return false;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}
