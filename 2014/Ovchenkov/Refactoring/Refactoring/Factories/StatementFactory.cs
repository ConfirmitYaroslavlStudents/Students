using Refactoring.StatementTools;

namespace Refactoring.Factories
{
    public class StatementFactory
    {
        public static string CustomerName { get; set; }
        public static Rental[] Rentals { get; set; }
        private static Statement _statement;

        public static void GetCustomerRental(string customerName, Rental[] rentals)
        {
            CustomerName = customerName;
            Rentals = rentals;
        }
        public static Statement BuildStatement()
        {
            var customer = Rentals != null
                ? new Customer(CustomerName, Rentals)
                : new Customer(CustomerName);
            _statement = new Statement(customer);
            return _statement;
        }

        public static string BuildStringStatement()
        {
            var statementData = new StringStatementData();
            _statement.GetStatement(statementData, new StringStatement());

            return statementData.GetData().ToString();
        }

        public static string BuildJsonStatement()
        {
            var statementData = new StringStatementData();
            _statement.GetStatement(statementData, new JsonStatement());

            return statementData.GetData().ToString();
        }
        public static string[] BuildUniversalStatement()
        {
            var standartStatementData = new StringStatementData();
            var jsonStatementData = new StringStatementData();
            var formater = new UniversalStatement();

            formater.AddFormatter(new StringStatement(), standartStatementData.SetData);
            formater.AddFormatter(new JsonStatement(), jsonStatementData.SetData);

            _statement.GetStatement(null, formater);

            return new[] { standartStatementData.GetData().ToString(), jsonStatementData.GetData().ToString() };
        }
    }
}
