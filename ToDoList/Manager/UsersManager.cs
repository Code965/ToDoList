using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ToDoList.ViewModels;
using ToDoList.Models;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using ToDoList.Utils;

namespace ToDoList.Manager
{
    public class UsersManager
    {
        private readonly static string connString = ConfigurationManager.ConnectionStrings["ToDoList"].ConnectionString;

        public static UsersViewModel findUser(string email, string password)
        {
            int count = 0;

            UsersViewModel user =  new UsersViewModel();

            using (SqlConnection dbConn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_findUser]", dbConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password); 

                    cmd.Parameters.Add("@count", SqlDbType.Int).Direction = ParameterDirection.Output;
                    dbConn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        user.name = dr["name"].ToString();
                        user.surname = dr["surname"].ToString();
                        user.email = dr["email"].ToString();
                        user.encryptPassword = Decrypt(dr["password"].ToString());
                        user.password = dr["password"].ToString();
                        user.dateBirth = Convert.ToDateTime(dr["dateBirth"].ToString());
                    }
                    
                }
            }

            return user;
        }

        public static bool insertUser(Users utente, out int ReturnCode)
        {
            ReturnCode = 0;
            bool retval = false;

            SqlConnection conn = new SqlConnection(connString); //creo una connessione con il db
            SqlCommand cmd = new SqlCommand("[dbo].[usp_insertUser]", conn); //gli dico di eseguire una store procedure a quella connessione
            cmd.CommandType = CommandType.StoredProcedure;
            //Passo i valori alla store procedure facendo attenzione perchè a sinistra sono i nomi della store, a destra i valori che gli passo 
            cmd.Parameters.AddWithValue("@name", utente.name);
            cmd.Parameters.AddWithValue("@surname", utente.surname);
            cmd.Parameters.AddWithValue("@email", utente.email);
            cmd.Parameters.AddWithValue("@password", Encrypt(utente.password)); //cosi cripto la password
            cmd.Parameters.AddWithValue("@dateBirth", utente.dateBirth);

            cmd.Parameters.Add("@ReturnCode", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            int retCode = 0;

            conn.Open(); //apro la connessione
            int i = cmd.ExecuteNonQuery(); //Esegue la store procedure
            retCode = (int)cmd.Parameters["@ReturnCode"].Value; //PRENDE IL VALORE DI OUTPUT
            retval = retCode == 0; //PRENDE IL VALORE DI retCode
            ReturnCode = retCode; ;
            conn.Close(); //Chiude la connessione al database

            return retval; //mi torna un valore booleano
        }



        //METODI PER CRIPTARE E DECRIPTARE LE PASSWORD
        private static string Encrypt(string clearText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }


        private static string Decrypt(string cipherText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }


    }
}