﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ToDoList.ViewModels;
using ToDoList.Models;
using ToDoList.Utils;
using System.Data;

namespace ToDoList.Manager
{
    public class ActivityManager
    {

        private readonly static string connString = ConfigurationManager.ConnectionStrings["ToDoList"].ConnectionString;

        public static bool insertActivity(Activity activity, out int ReturnCode)
        {
            ReturnCode = 0;
            bool retval = false;

            SqlConnection conn = new SqlConnection(connString); //creo una connessione con il db
            SqlCommand cmd = new SqlCommand("[dbo].[usp_insertActivity]", conn); //gli dico di eseguire una store procedure a quella connessione
            cmd.CommandType = CommandType.StoredProcedure;
            //Passo i valori alla store procedure facendo attenzione perchè a sinistra sono i nomi della store, a destra i valori che gli passo 
            cmd.Parameters.AddWithValue("@name", activity.name);
            cmd.Parameters.AddWithValue("@description", activity.description);
            cmd.Parameters.AddWithValue("@date", activity.date);
            cmd.Parameters.AddWithValue("@priority", activity.priority);
            cmd.Parameters.AddWithValue("@category", activity.category);
           

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


        public static List<Activity> findActivity(DateTime today) //mi deve trovare tutte le attività filtrate con la data di oggi
        {
            int count = 0;
            List<Activity> retval = new List<Activity>();
            using (SqlConnection dbConn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("", dbConn)) //eseguo la store procedure
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@today", today);
                    cmd.Parameters.Add("@count", SqlDbType.Int).Direction = ParameterDirection.Output;
                    dbConn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        retval = dr.BindToList<Activity>();
                    }

                    count = Convert.ToInt32(cmd.Parameters["@count"].Value);
                }
            }

            return retval; //mi faccio tornare 
        }

    }
}