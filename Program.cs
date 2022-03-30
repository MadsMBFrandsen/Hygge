using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

using BL;
using H1OPG.Models;



// Passwords sql server = Admin1!

namespace H1OPG
{
    public class Program
    {


        static void Main(string[] args)
        {
            Metode();
        }
        private static void Metode()
        {
            bool start = true;
            while (start)
            {
                Console.Clear();
                Console.WriteLine("1. Vælg firma\n" +
                "2. Rediger firma (virker ikke helt)\n" +
                "3. Lav nyt firma\n" +
                "4. Slet firma\n" +
                "5. Luk programmet");
                int i = 0;
                try
                {
                    i = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("invalit input");
                    Console.WriteLine("tast et nummer 1 til 5");
                    Console.WriteLine("Press Enter hvis du fårstår");
                    Console.ReadLine();
                    
                }
                //i = Convert.ToInt32(Console.ReadLine());
                Repositiory rep = new Repositiory();
                // her laver jeg en liste som får data fra Repositoryfirma som igen får det fra DBFirma hvor den er lavet og der er blevet tilføjet 5 statick objects til listen
                List<Company> listFirma = rep.GetAll();
                switch (i)
                {

                    case 1:

                        //forvære Firma i listen printes firma og alle dataerne den indeholder ud i Consolen
                        foreach (Company item in listFirma)
                        {
                            Console.WriteLine("         Firma Nummer {0} Firma Navn={1},", item.CompanyID, item.CompanyName);
                            Console.WriteLine("         ***");
                        }

                        
                        //her laver jeg en bool til at kunne break while looped 
                        bool tempB = true;
                        while (tempB)//her bruges boolen for at definerer mens at tempB er sandt gøres hvad der er i looped, dette gøres for at tvinge den intastede værdig til være den rigtige format
                        {
                            Console.WriteLine("         ");
                            Console.WriteLine("         skriv det Nummer på det Firma du vil have vist");
                            Console.WriteLine("         ");
                            //her tager en variabel(input) i mod og gemmer det intastede input fra consolen 
                            string input = Console.ReadLine();
                            //her valideres om inputtet er en int dette gøres ved at sette en variabel(inputTemp) til hvad der gives tilbage fra valideringen som bliver lavet i RepositoryFirma
                            //methoden Validate som får inputet(input) der blev gemt før med til validering 
                            //hvis ja gives det tilbage uændret hvis nej gives -1 tilbage 
                            int inputTemp = rep.Validate(input);

                            //hvis variablen er -1 er der lavet en fejl intastning hvor med at der skrives en fejl meddelse ud på consolen 
                            if (inputTemp == -1)
                            {
                                Console.WriteLine("fejl input lavet");
                            }
                            //hvis inputtet er 1 til og med 5 laves der printes den valgte Firma ud og boolen sættes til false for at færtigøre while looped 
                            if (inputTemp > 0)
                            {
                                Company listOneFirma = rep.GetOne(inputTemp);
                                if (listOneFirma == null)
                                {
                                    inputTemp = -1; 
                                }
                                else
                                {
                                    Console.WriteLine("         ID={0} FirmaName={1} CompanyDescription={2} CompanyAddress={3} CompanyFoundingDate={4}", +
                                    listOneFirma.CompanyID, listOneFirma.CompanyName, listOneFirma.CompanyDescription, listOneFirma.CompanyAddress, listOneFirma.CompanyFoundingDate);
                                    Console.WriteLine("         ***");
                                    tempB = false;
                                }
                                
                            }
                            else
                            {
                                tempB = true;
                            }
                        }

                        Console.ReadLine();
                        break;


                    case 2:
                        foreach (Company item in listFirma)
                        {
                            Console.WriteLine("         Firma Nummer {0} Firma Navn={1},", item.CompanyID, item.CompanyName);
                            Console.WriteLine("         ***");
                        }
                        //her laves en variabel med de ønsked værdiger der skal overskrive en af de objecter i listen
                        Company FirmaToUpdate = new Company() { CompanyName = "test", CompanyDescription = "test", CompanyAddress = "test", CompanyFoundingDate = DateTime.Now };
                        
                        bool TempB2 = true;
                        Console.WriteLine("         ");
                        while (TempB2)//her bruges boolen for at definerer mens at TempB2 er sandt gøres hvad der er i looped, dette gøres for at tvinge den intastede værdig til være den rigtige format
                        {
                            Console.WriteLine("         ");
                            Console.WriteLine("         skriv det Nummert på den Firma du vil have Updateret");
                            //her tager en variabel(input) i mod og gemmer det intastede input fra consolen 
                            string input2 = Console.ReadLine();
                            int inputTemp2 = rep.Validate(input2);
                            //her valideres om inputtet er en int dette gøres ved at sette en variabel(inputTemp2) til hvad der gives tilbage fra valideringen som bliver lavet i RepositoryFirma Methoden Validate
                            //som får inputet(input2) der blev gemt før med til validering 
                            //hvis ja gives det tilbage uændret hvis nej gives -1 tilbage 

                            if (inputTemp2 == -1)
                            {//hvis variablen er -1 er der lavet en fejl intastning hvor med at der skrives en fejl meddelse ud på consolen 
                                Console.WriteLine("fejl input lavet");
                            }
                            if (inputTemp2 > 0)
                            {//hvis inputtet er 1 til og med 5 laves der printes den valgte Firma ud og boolen sættes til false for at færtigøre while looped 
                                Company UpdateFirma = rep.UpdateFirma(inputTemp2, FirmaToUpdate);
                                //her skrives på console en bool der er givet tilbage fra RepositoryFirma UpdateFirma Methoden hvis Firman er updateret skrives true hvis nej skrives false
                                Console.WriteLine("         {0}", UpdateFirma);
                                //her laves en variabel hvor i en liste bliver lavet af alle Firma hvorfra dataen fås fra methode GetAll i RepositoryFirma 
                                List<Company> listFirmaNew = rep.GetAll();
                                //forvære Firma i listen printes Firman og alle dataerne den indeholder ud i Consolen
                                foreach (Company item in listFirmaNew)
                                {
                                    if (item.CompanyID == inputTemp2)
                                    {
                                        Console.WriteLine("         ID={0} FirmaName={1} CompanyDescription={2} CompanyAddress={3} CompanyFoundingDate={4}", +
                                            item.CompanyID, item.CompanyName, item.CompanyDescription, item.CompanyAddress, item.CompanyFoundingDate);
                                        Console.WriteLine("         ***");
                                    }

                                }
                                //her bliver boolen sat til false for at bryde while looped
                                TempB2 = false;
                            }

                        }
                        Console.ReadLine();
                        break;
                    case 3:
                        int count = 0;
                        foreach (Company item in listFirma)
                        {
                            count++;
                        }
                        bool Textbool = true;
                        while (Textbool)
                        {
                            Console.WriteLine("         Skriv CompanyName");
                            string CN = Console.ReadLine();
                            string CN1 = rep.ValidateText(CN);
                            if (CN1 == "0")
                            {
                                Console.WriteLine("invalit input");
                                Textbool = true;
                            }

                            else
                            {
                                Console.WriteLine("         Skriv CompanyDescription");
                                string CD = Console.ReadLine();
                                string CD1 = rep.ValidateText(CD);
                                if (CD1 == "0")
                                {
                                    Console.WriteLine("invalit input");
                                    Textbool = true;
                                }
                                else
                                {
                                    Console.WriteLine("         Skriv Location");
                                    string L = Console.ReadLine();
                                    string L1 = rep.ValidateText(L);
                                    if (L1 == "0")
                                    {
                                        Textbool = true;
                                    }

                                    //inputTemp = Convert.ToInt32(input);
                                    Console.WriteLine("         Skriv CompanyFoundingDate yyyy-mm-dd");
                                    string CFDa = Console.ReadLine();
                                    try
                                    {
                                        DateTime CFD = Convert.ToDateTime(CFDa);
                                        Company firmaNew = new Company() { /*CompanyID = count + 1,*/ CompanyName = CN1, CompanyDescription = CD1, CompanyAddress = L1, CompanyFoundingDate = CFD };
                                        //her skrives der true hvis et nyt object er addet til listen 
                                        //som gøres ved at kalde på methoden CreateFirma som tager variablen FirmaNew med til RepositoryFirma
                                        //hvor det igen bliver sendt til databasen og der bliver addet til listen og der bliver retuneret en bool til RepositoryFirma som sender en bool til Program som er true eller false
                                        rep.CreateFirma(firmaNew);
                                    Console.WriteLine("         ");
                                    //forvære variable i listen skrives Firman ud dette gøres for at se om at den virklig er blevet puttet på listen
                                    foreach (Company item in listFirma)
                                    {

                                        Console.WriteLine("         ID={0} CompanyName={1} CompanyDescription={2} CompanyAddress={3} CompanyFoundingDate={4}", +
                                            item.CompanyID, item.CompanyName, item.CompanyDescription, item.CompanyAddress, item.CompanyFoundingDate);
                                        Console.WriteLine("         ***");
                                    }
                                    Console.ReadLine();
                                    Textbool = false;
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine("forkert date");
                                        Textbool = true;
                                    }
                                                                      

                                    
                                }


                            }

                        }







                        break;
                    case 4:
                        foreach (Company item in listFirma)
                        {
                            Console.WriteLine("         Firma Nummer {0} Firma Navn={1},", item.CompanyID, item.CompanyName);
                            Console.WriteLine("         ***");
                        }
                        Console.WriteLine("         skriv det Nummeret på det Firma du vil have Deletet");
                        Console.WriteLine("         ");
                        //her laver jeg en bool til at kunne break while looped 
                        bool TempB3 = true;
                        while (TempB3)
                        {//her bruges boolen for at definerer mens at TempB3 er sandt gøres hvad der er i looped, dette gøres for at tvinge den intastede værdig til være den rigtige format
                         //her tager en variabel(input) i mod og gemmer det intastede input fra consolen 
                            string input3 = Console.ReadLine();
                            int inputTemp3 = rep.Validate(input3);
                            //her valideres om inputtet er en int dette gøres ved at sette en variabel(inputTemp3) til hvad der gives tilbage fra valideringen som bliver lavet i RepositoryFirma Methoden Validate
                            //som får inputet(input3) der blev gemt før med til validering 
                            //hvis ja gives det tilbage uændret hvis nej gives -1 tilbage
                            if (inputTemp3 == -1)
                            {//hvis variablen er -1 er der lavet en fejl intastning hvor med at der skrives en fejl meddelse ud på consolen 
                                Console.WriteLine("fejl input lavet");
                            }
                            //hvis inputtet er 1 til og med 5 laves der printes den valgte Firma ud og boolen sættes til false for at færtigøre while looped 
                            if (inputTemp3 > 0)
                            {
                                //her skrives på console en bool der er givet tilbage fra RepositoryFirma(repV) DeleteFirma Methoden hvis Firman er Deletes(slettes) skrives true hvis nej skrives false
                                Console.WriteLine("         {0}", rep.DeleteFirma(inputTemp3));
                                //her bliver listen skrevet til consolen for at tjekke om at den rigtige object på listen er blevet slettet
                                foreach (Company item in listFirma)
                                {
                                    Console.WriteLine("         ID={0} FirmaName={1} CompanyDescription={2} CompanyAddress={3} CompanyFoundingDate={4}", +
                                        item.CompanyID, item.CompanyName, item.CompanyDescription, item.CompanyAddress, item.CompanyFoundingDate);

                                    Console.WriteLine("         ***");
                                }
                                //her bliver boolen sat til false for at bryde while looped
                                TempB3 = false;
                            }
                        }
                        Console.WriteLine("Press Enter to Exit Program");
                        Console.ReadLine();


                        break;
                    case 5:

                        start = false;
                        break;
                    case 6:
                        //var connectionString = ConfigurationManager.ConnectionStrings["Sql"].ConnectionString;
                        ////Make query to database
                        //string query = "SELECT [CompanyName], [CompanyDescription], [CompanyAddress] FROM [Company]  ";
                        ////DB connection inside a using clause to ensure the closing of connection when done.
                        //using (SqlConnection connection = new SqlConnection(connectionString))
                        //{
                        //    //ALWAYS OPEN CONNECTION!
                        //    connection.Open();
                        //    Console.WriteLine("I AM OPEN!");
                        //    //Create a new SQL query command
                        //    using (SqlCommand command = new SqlCommand(query, connection))
                        //    {
                        //        //Remember to put the query text INTO THE COMMMAND!!!!!
                        //        command.CommandText = query;
                        //        //Execute the command and send the query to the database with a data reader
                        //        using (SqlDataReader reader = command.ExecuteReader())
                        //        {
                        //            Console.WriteLine("Retrieving Data...\n\n");
                        //            //Read the data
                        //            while (reader.Read())
                        //            {
                        //                //do stuff with data
                        //                Console.WriteLine("{0} {1} {2}", reader["CompanyName"], reader["CompanyDescription"], reader["CompanyAddress"]);
                        //            }
                        //        }
                        //        Console.ReadLine();
                        //    }
                        //    //not necessary but good just in case!
                        //    connection.Close();
                        //}
                        break;

                    case 7:

                        break;


                }
            }
        }

    }
}
