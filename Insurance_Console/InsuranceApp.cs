using System;
using System.Collections.Generic;
using System.Globalization;        // CultureInfo, DateTimeStyles
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance_Console
{
    //#############################################################################################
    // Hlavní třída zapouzdřující celou aplikaci
    //#############################################################################################
    internal class InsuranceApp
    {
        private List<Person> _insuredPersons = new List<Person>();  // Seznam pojištěných osob  
        
        private enum Option { AddNewPerson, WriteAllPersons, SearchPerson, Finish, Invalid };

        //-----------------------------------------------------------------------------------------
        public InsuranceApp() 
        {}

        //-----------------------------------------------------------------------------------------
        // Základní funkce spouštějící aplikaci
        //-----------------------------------------------------------------------------------------
        public void Run()
        {
            // Hlavní smyčka programu
            Option option = Option.Invalid;
            while (option != Option.Finish)
            {
                WriteInitialScreen();
                option = GetOptionFromUser();

                // Provede akci v závislosti na volbě uživatele
                switch (option)
                {
                    case Option.AddNewPerson:
                        AddNewPerson();
                        break;
                    case Option.WriteAllPersons:
                        WriteAllPersons();
                        break;
                    case Option.SearchPerson:
                        SearchPerson();
                        break;
                    case Option.Finish:
                        InformAboutApplicationFinish();
                        break;
                    default:
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        //-----------------------------------------------------------------------------------------
        // Vypíše úvodní obrazovku s nabídkou voleb.
        //-----------------------------------------------------------------------------------------
        private void WriteInitialScreen()
        {
            WriteHeadline("Evidence pojištěných");

            Console.WriteLine("Vyberte akci:");
            Console.WriteLine("1 - Přidat nového pojištěného");
            Console.WriteLine("2 - Vypsat všechny pojištěné");
            Console.WriteLine("3 - Vyhledat pojištěného");
            Console.WriteLine("4 - Konec");
            Console.WriteLine();
        }

        //-----------------------------------------------------------------------------------------
        // Vyzve uživatele k zadání volby, validuje ji a vrátí.
        //-----------------------------------------------------------------------------------------
        private Option GetOptionFromUser()
        {
            Console.Write("Zvolená akce: ");
            Option option = Option.Invalid;
            while (option == Option.Invalid)
            {
                if (int.TryParse(Console.ReadLine(), out int userInput))
                {
                    option = TransformIntToUserOption(userInput);
                }

                if (option == Option.Invalid)
                {
                    Console.Write("Neplatná volba, zadejte znovu: ");
                }
            }

            Console.WriteLine();
            return option;
        }

        //-----------------------------------------------------------------------------------------
        // Transfornuje int na uživatelskou volbu typu enum Option.
        //-----------------------------------------------------------------------------------------
        private Option TransformIntToUserOption(int value)
        {
            switch (value)
            {
                case 1:
                    return Option.AddNewPerson;
                case 2:
                    return Option.WriteAllPersons;
                case 3:
                    return Option.SearchPerson;
                case 4:
                    return Option.Finish;
                default:
                    return Option.Invalid;
            }
        }

        //-----------------------------------------------------------------------------------------
        // Vyzve uživatele k zadání nové pojištěné osoby.
        // Vyžádá si všechny potřebné vstupy, validuje je, vytvoří novou osobu a uloží ji do 
        // seznamu pojištěných osob.
        //-----------------------------------------------------------------------------------------
        private void AddNewPerson()
        {
            WriteHeadline("Přidání nového pojištěného:");

            Console.Write("Jméno: ");
            string firstName = GetValidStringFromUser();
            
            Console.Write("Příjmení: ");
            string lastName = GetValidStringFromUser();

            Console.Write("Datum narození (ve formátu dd.mm.rrrr): ");
            DateTime birthDate = GetValidDateFromUser();

            Console.Write("Telefonní číslo (bez mezer): ");
            ulong phoneNumber = GetValidPhoneNumberFromUser();

            _insuredPersons.Add(new Person(firstName, lastName, birthDate, phoneNumber));

            Console.WriteLine();
            Console.WriteLine("Data byla uložena. Pokračujte libovolnou klávesou...");
        }

        //-----------------------------------------------------------------------------------------
        // Vypíše všechny uložené pojištěné osoby uložené v seznamu pojištěných osob.
        //-----------------------------------------------------------------------------------------
        private void WriteAllPersons()
        {
            WriteHeadline("Výpis všech pojištěných:");

            string emptyMessage = "Seznam pojištěných je prázdný.";
            WritePersonsFromList(_insuredPersons, emptyMessage);

            Console.WriteLine();
            Console.WriteLine("Pokračujte libovolnou klávesou...");
        }

        //-----------------------------------------------------------------------------------------
        // Vyžádá si od uživatele jméno a příjmení osoby.
        // Následně v seznamu pojištěných osob najde a vypíše osoby zadaného jména a příjmení.
        //-----------------------------------------------------------------------------------------
        private void SearchPerson()
        {
            WriteHeadline("Vyhledání pojistného:");

            Console.Write("Jméno: ");
            string firstName = GetValidStringFromUser();

            Console.Write("Příjmení: ");
            string lastName = GetValidStringFromUser();

            Console.WriteLine();

            List<Person> foundPersons = _insuredPersons.FindAll((person) => (person.FirstName == firstName && 
                                                                             person.LastName == lastName));

            string emptyMessage = "V seznamu nebyla nalezena žádná osoba zadaného jména a příjmení.";
            WritePersonsFromList(foundPersons, emptyMessage);

            Console.WriteLine();
            Console.WriteLine("Pokračujte libovolnou klávesou...");
        }

        //-----------------------------------------------------------------------------------------
        // Vypíše hlášku o ukončení aplikace.
        //-----------------------------------------------------------------------------------------
        private void InformAboutApplicationFinish()
        {
            WriteHeadline("Ukončení aplikace:");

            Console.WriteLine("Aplikaci ukončíte stiskem libovolné klávesy...");
        }

        //-----------------------------------------------------------------------------------------
        // Vyzve uživatele k zadání stringu, validuje ho (aby nebyl null, nebyl prázdný,
        // neobsahoval pouze bílé znaky) a vrátí ho. 
        //-----------------------------------------------------------------------------------------
        private string GetValidStringFromUser()
        {
            string? value = null;

            while (string.IsNullOrWhiteSpace(value = Console.ReadLine()))
            {
                Console.Write("Neplatná hodnota, zadejte znovu: ");
            }

            return value;
        }

        //-----------------------------------------------------------------------------------------
        // Vyzve uživatele k zadání data, které validuje a vrátí.
        //-----------------------------------------------------------------------------------------
        private DateTime GetValidDateFromUser()
        {
            DateTime date;
            string datePattern = "d.M.yyyy";

            while (!DateTime.TryParseExact(Console.ReadLine(), datePattern, null,
                                           DateTimeStyles.None, out date))
            {
                Console.Write("Neplatné datum, zadejte znovu: ");
            }

            return date;
        }

        //-----------------------------------------------------------------------------------------
        // Vyzve uživatele k zadání telefonního čísla, které validuje a vrátí
        // Validace je provedena zjednodušeně pouze aby splňovala formát ulong.
        // Stačí toto zjednodušené řešení???
        //-----------------------------------------------------------------------------------------
        private ulong GetValidPhoneNumberFromUser()
        {
            ulong phoneNumber;
            while (!ulong.TryParse(Console.ReadLine(), out phoneNumber))
            {
                Console.Write("Neplatné telefonní číslo, zadejte znovu: ");
            }

            return phoneNumber;
        }

        //-----------------------------------------------------------------------------------------
        // Vypíše na obrazovku formátovaný nadpis.
        //-----------------------------------------------------------------------------------------
        private void WriteHeadline(string text)
        {
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine(text);
            Console.WriteLine("-----------------------------------------------------------------");
            Console.WriteLine();
        }

        //-----------------------------------------------------------------------------------------
        // Vypíše osoby v zadaném seznamu osob (persons).
        // Pokud je seznam prázdný, vypíše zadanou hlášku (emptyMessage).
        //-----------------------------------------------------------------------------------------
        private void WritePersonsFromList(List<Person> persons, string emptyMessage)
        {
            if (persons.Count == 0)
            {
                Console.WriteLine(emptyMessage);
            }
            else
            {
                foreach (Person person in persons)
                {
                    Console.WriteLine(person);
                }
            }
        }

        //-----------------------------------------------------------------------------------------
    }
}
