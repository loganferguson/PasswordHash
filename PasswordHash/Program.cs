using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PasswordHash
{
    class Program
    {
        static void Main(string[] args)
        {
            bool stillHashing = true;

            Dictionary<string, string> UserLogins = new Dictionary<string, string>();

            while (stillHashing == true)
            {
             try
                {
                Console.WriteLine("\n");
                Console.WriteLine("  -------------MAIN MENU-------------");
                Console.WriteLine("  -----(1) Create User Account.------");
                Console.WriteLine("  ---------(2) User Login.-----------");
                Console.WriteLine("  ------(3) Verify Encryption.-------");
                Console.WriteLine("  --------(4) View all users.--------");
                Console.WriteLine("  -----(5) Exit the application.-----\n");
                Console.Write("     Enter selection: ");
                int numSelect = int.Parse(Console.ReadLine());
                
                    if (numSelect == 1)
                            {
                                Console.WriteLine("\n  -------CREATE USER ACCOUNT--------");
                                Console.Write("Enter Username: ");
                                string username = Console.ReadLine();
                                Console.Write("Enter new password: ");
                                string password = Console.ReadLine();
                                UserLogins.Add(username, password);
                                Console.WriteLine("\nUser account created!");
                            }
                
                


                else if (numSelect == 2)
                {
                    bool loginFail = true;

                    while (loginFail == true)
                    {
                        Console.WriteLine("\n  -------USER AUTHENTICATION--------");
                        Console.Write("Enter Username: ");
                        string username = Console.ReadLine();
                        Console.Write("Enter password: ");
                        string password = Console.ReadLine();

                        bool hasValue = UserLogins.TryGetValue(username, out string pass);

                            if (hasValue)
                            {   
                               if (password == pass)
                               {
                                  Console.WriteLine("\nLogin successful!");
                                    loginFail = false;
                               }
                                  else
                                  {
                                   Console.WriteLine("\nThe password you entered is invalid.");
                                  }                                                           
                        
                            }
                            else               
                            {
                               Console.WriteLine("\nThe username you entered does not exist.");
                            }
                    }
                    
                }              
                else if (numSelect == 3)
                {
                    Console.WriteLine("\n  -------VERIFY ENCRYPTION SERVICE--------");
                    Console.Write("Enter Username: ");
                    string username = Console.ReadLine();
                    Console.Write("Enter password: ");
                    string password = Console.ReadLine();

                    using (MD5 md5Hash = MD5.Create())
                    {
                        string hashPassword = Encryption(md5Hash, password);
                        
                        Console.WriteLine($"\nPassword Encryption: {hashPassword}");
                    }
                    
                }
                else if (numSelect == 4)
                {
                        int numUsers = 1;
                        Console.WriteLine("\n         --------ALL USERS-------");
                    foreach (KeyValuePair<string, string> users in UserLogins)
                    {
                        Console.WriteLine($"\n     {numUsers})   Username: {users.Key}    Password: {users.Value}");
                            numUsers++;
                    }
                }
                else
                {
                    stillHashing = false;
                }
                }
                catch(ArgumentException)
                {
                    Console.WriteLine("\nThe account information you have entered already exists.\n Please enter a different username.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("\n       INVALID ENTRY - Please enter numerical value to select menu option.");
                };
            }
        }

        static string Encryption (MD5 md5Hash, string userLogin)
        {
            byte[] byteArray = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(userLogin));

            StringBuilder hashStringer = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < byteArray.Length; i++)
            {
                hashStringer.Append(byteArray[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return hashStringer.ToString();
        }
    }
}
