using System;

namespace TaxRelatedMethodPrblms {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("-----------TAX CALCULATOR-----------\nWhat is your annual income (Please do not include a comma): ");
            double userIncome = Convert.ToDouble(Console.ReadLine());
            float taxAmnt = HowMuchDoIOwe(TaxBracket(userIncome), userIncome);
            Console.WriteLine("You owe " + taxAmnt + " in taxes this year LOL.");


            static int TaxBracket(double userIncome1) {
                int whatBracket;
                if (userIncome1 >= 0 && userIncome1 <= 9525) {
                    whatBracket = 0;
                    Console.WriteLine("You earned " + userIncome1 + " this year, so you are in the $0 to $9,525 tax bracket.");
                    return whatBracket;
                }
                else if (userIncome1 >= 9526 && userIncome1 <= 38700) {
                    whatBracket = 1;
                    Console.WriteLine("You earned " + userIncome1 + " this year, so you are in the $9,526 to $38,700 tax bracket.");
                    return whatBracket;
                }
                else if (userIncome1 >= 38701 && userIncome1 <= 82500) {
                    whatBracket = 2;
                    Console.WriteLine("You earned " + userIncome1 + " this year, so you are in the $38,701 to $82,500 tax bracket.");
                    return whatBracket;
                }
                else if (userIncome1 >= 82501 && userIncome1 <= 157500) {
                    whatBracket = 3;
                    Console.WriteLine("You earned " + userIncome1 + " this year, so you are in the $82,501 to $15,7500 tax bracket.");
                    return whatBracket;
                }
                else if (userIncome1 >= 159501 && userIncome1 <= 200000) {
                    whatBracket = 4;
                    Console.WriteLine("You earned " + userIncome1 + " this year, so you are in the $15,7501 to $200,000 tax bracket.");
                    return whatBracket;
                }
                else if (userIncome1 >= 200001 && userIncome1 <= 500000) {
                    whatBracket = 5;
                    Console.WriteLine("You earned " + userIncome1 + " this year, so you are in the $200,001 to $500,000 tax bracket.");
                    return whatBracket;
                }
                else if (userIncome1 >= 500001) {
                    whatBracket = 6;
                    Console.WriteLine("You earned " + userIncome1 + " this year, so you are in the $500,001 or higher tax bracket.");
                    return whatBracket;
                }
                return whatBracket = -1;
            }


            static float HowMuchDoIOwe(int bracketNumID, double userIncome2) {
                float userTaxMoney;
                float userIncome3 = Convert.ToSingle(userIncome2);
                switch (bracketNumID) {
                    case (0):
                        userTaxMoney = userIncome3 * 0.1f;
                        return userTaxMoney;
                    case (1):
                        userTaxMoney = 952.50f + ((userIncome3 - 9525f) * 0.12f);
                        return userTaxMoney;
                    case (2):
                        userTaxMoney = 4453.50f + ((userIncome3 - 38700f) * 0.22f);
                        return userTaxMoney;
                    case (3):
                        userTaxMoney = 14089.50f + ((userIncome3 - 82500f) * 0.24f);
                        return userTaxMoney;
                    case (4):
                        userTaxMoney = 32089.50f + ((userIncome3 - 157500f) * 0.32f);
                        return userTaxMoney;
                    case (5):
                        userTaxMoney = 45689.50f + ((userIncome3 - 200000f) * 0.35f);
                        return userTaxMoney;
                    case (6):
                        userTaxMoney = 150689.50f + ((userIncome3 - 500000f) * 0.37f);
                        return userTaxMoney;
                }
                return userTaxMoney = 0;
            }

        }
    }
}
