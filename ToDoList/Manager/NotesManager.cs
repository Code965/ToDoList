using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ToDoList.Models;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using ToDoList.Utils;

namespace ToDoList.Manager
{
    public class NotesManager
    {
        private readonly static string connString = ConfigurationManager.ConnectionStrings["ToDoList"].ConnectionString;

        public static bool insertNotes(Notes notes, out int ReturnCode)
        {
            ReturnCode = 0;
            bool retval = false;

            SqlConnection conn = new SqlConnection(connString); //creo una connessione con il db
            SqlCommand cmd = new SqlCommand("[dbo].[usp_insertNotes]", conn); //gli dico di eseguire una store procedure a quella connessione
            cmd.CommandType = CommandType.StoredProcedure;
            //Passo i valori alla store procedure facendo attenzione perchè a sinistra sono i nomi della store, a destra i valori che gli passo 
            cmd.Parameters.AddWithValue("@title", notes.title);
            cmd.Parameters.AddWithValue("@DescriptionNotes", notes.DescriptionNotes);
            cmd.Parameters.AddWithValue("@DateNotes", notes.DateNotes);
            cmd.Parameters.AddWithValue("@CategoryNotes", notes.CategoryNotes);

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


        public static List<Notes> findNotes() //mi deve trovare tutte le attività filtrate con la data di oggi
        {
            int returnCode = 0;

            List<Notes> retval = new List<Notes>();
            using (SqlConnection dbConn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[usp_findNotes]", dbConn)) //eseguo la store procedure
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReturnCode", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
                    dbConn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        retval = dr.BindToList<Notes>();
                    }

                    returnCode = Convert.ToInt32(cmd.Parameters["@ReturnCode"].Value);
                }
            }

            return retval; //mi faccio tornare 
        }



    }
}