namespace Insurance_Console
{
    /*---------------------------------------------------------------------------------------------
    Dotazy:
    -----------------------------------------------------------------------------------------------
    - class Person
       - Je možné místo věku použít datum narození? - Ano (konzultace 24.8.2022)
       - PhoneNumber 
          - Je lepší jako string int nebo ulong? - ulong je v pořádku (konzultace 24.8.2022)
          - Validaci na vstupu jsem provedl zjednodušeně - v pořádku (konzultace 24.8.2022)
    */

    //#############################################################################################
    internal class Program
    {
        //-----------------------------------------------------------------------------------------
        static void Main(string[] args)
        {
            // Vytvoří aplikaci pojištění a spustí ji:
            InsuranceApp insuranceApp = new InsuranceApp();
            insuranceApp.Run();
        }
    }
}