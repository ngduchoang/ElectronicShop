using System;
using System.Collections.Generic;
using System.Text;
//MD5
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;

namespace CommonCode
{
    #region MD5
    class MD5Hash
    {
        #region Ví dụ sử dụng
        // This code example produces the following output:
        //
        // The MD5 hash of Hello World! is: ed076287532e86365e841e92bfc50d8c.
        // Verifying the hash...
        // The hashes are the same.
        /*
        string source = "Hello World!";
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, source);

                Console.WriteLine("The MD5 hash of " + source + " is: " + hash + ".");

                Console.WriteLine("Verifying the hash...");

                if (VerifyMd5Hash(md5Hash, source, hash))
                {
                    Console.WriteLine("The hashes are the same.");
                }
                else
                {
                    Console.WriteLine("The hashes are not same.");
                }
            }
        */
        #endregion
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

           
            byte[] data = md5Hash.ComputeHash(Encoding.Default.GetBytes(input));

         
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
           

            if (input== hash)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    #endregion

    namespace DataClasses
    {
    
        /// Class nay cung cap cac phuong thuc de tuong tac voi CSDL
        
        public class DataTool
        {
            public DataTool()
            {
            }
            ~DataTool()
            {
            }
           
            /// Phuong thuc tra lai mot doi tuong ket noi CSDL SqlConnection da duoc mo
            /// Dau vao la chuoi ket noi
            /// </summary>
            /// <param name="conString"></param>
            /// <returns></returns>
            public SqlConnection getConnection(string conString)
            {
                SqlConnection sqlCon = new SqlConnection();
                try
                {
                    sqlCon.ConnectionString = conString;
                    sqlCon.Open();
                    if (sqlCon.State == ConnectionState.Open)
                        return sqlCon;
                }
                catch (Exception exc)
                {
                    
                    throw exc;
                }
                finally
                {
                }
                return null;
            }
           
            public int execInsUpdDel(string conString, string sqlInsUpdDel, List<SqlParameter> sqlParams)
            {
                SqlConnection sqlConnection;
                SqlCommand sqlCommand = new SqlCommand(); ;
                try
                {
                    sqlConnection = getConnection(conString);
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sqlInsUpdDel;
                    foreach (SqlParameter pr in sqlParams)
                    {
                        sqlCommand.Parameters.Add(pr);
                    }
                    return sqlCommand.ExecuteNonQuery();
                }
                catch (Exception exc)
                {
                    throw exc;
                }
                finally
                {
                    //sqlConnection.Close();
                    sqlCommand.Dispose();
                }
                //return 0;
            }
           
            public DataTable execSelect(string conString, string sqlSelect, List<SqlParameter> sqlParams)
            {
                SqlConnection sqlConnection;
                SqlCommand sqlCommand = new SqlCommand(); ;
                DataTable retData = new DataTable(); ;
                SqlDataAdapter sqlAdap;
                try
                {
                    sqlConnection = getConnection(conString);
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sqlSelect;
                    if (sqlParams != null)
                        foreach (SqlParameter pr in sqlParams)
                        {
                            sqlCommand.Parameters.Add(pr);
                        }
                    sqlAdap = new SqlDataAdapter(sqlCommand);
                    sqlAdap.Fill(retData);
                    return retData;
                }
                catch (Exception exc)
                {
                    throw exc;
                }
                finally
                {
                    //sqlConnection.Close();
                    sqlCommand.Dispose();
                }
                //return null;
            }
            public SqlDataReader execReader(string conString, string sqlSelect, List<SqlParameter> sqlParams)
            {
                SqlConnection sqlConnection;
                SqlCommand sqlCommand = new SqlCommand(); ;
                try
                {
                    sqlConnection = getConnection(conString);
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sqlSelect;
                    if (sqlParams != null)
                        foreach (SqlParameter pr in sqlParams)
                        {
                            sqlCommand.Parameters.Add(pr);
                        }
                    return sqlCommand.ExecuteReader();
                }
                catch (Exception exc)
                {
                    throw exc;
                }
                finally
                {
                    //sqlConnection.Close();
                    sqlCommand.Dispose();
                }
                //return null;
            }
        }

    }
}