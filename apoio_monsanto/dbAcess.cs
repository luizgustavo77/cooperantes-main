using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace apoio_monsanto
{
    public class dbAcess
    {
        //Util.Funcoes func = new Funcoes();
        //string connStr = ConfigurationManager.ConnectionStrings["MyDbConn1"].ToString();
        //SqlConnection myConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog = base_monsanto; Integrated Security = False; User Id = apoio_mon; Password=&$$mon_$@nt0;");

        SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyDbConn1"].ToString());
        private DataSet dsRetQry = new DataSet();
        public DataSet dsReturn
        {
            get
            {
                return this.dsRetQry;
            }
            set
            {
                this.dsRetQry = value;
            }
        }
        // = "";
        private String retQry;
        public String retQuery
        {
            get
            {
                return this.retQry;
            }
            set
            {
                this.retQry = value;
            }
        }

        public bool OpenCon()
        {
            try
            {
                myConnection.Close();
                myConnection.Open();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        public bool CloseCon()
        {
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public void stringSql(string query)
        {
            using (var cmd = myConnection.CreateCommand())
            {
                myConnection.Open();
                cmd.CommandText = query;
                try
                {
                    retQry = cmd.ExecuteScalar().ToString();
                }
                catch (Exception)
                {

                    retQry = "";
                }

            }

            try
            {
                CloseCon();
            }
            catch (Exception c)
            {
                retQry = "Erro ao fechar conexão: " + c.Message;
            }
        }

        public bool execSql(string type, string query)
        {
            // abro a conexão
            try
            {
                OpenCon();
            }
            catch (Exception e)
            {
                retQry = "Erro ao abrir conexão: " + e.Message;
                return false;
            }

            SqlDataAdapter dadapter = new SqlDataAdapter();

            if (type == "insert")
            {
                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = myConnection;            // <== lacking
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;

                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException sql)
                        {
                            retQry = "Erro ao executar insert: " + sql.Message;
                            return false;
                        }
                    }
                }
                catch (Exception i)
                {
                    retQry = "Erro ao executar insert: " + i.Message;
                    return false;
                }
            }
            else if (type == "delete")
            {
                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = myConnection;            // <== lacking
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;

                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException sql)
                        {
                            retQry = "Erro ao executar delete: " + sql.Message;
                            return false;
                        }
                    };
                }
                catch (Exception d)
                {
                    retQry = "Erro ao executar delete: " + d.Message;
                    return false;
                }
            }
            else if (type == "update")
            {
                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = myConnection;            // <== lacking
                        command.CommandType = CommandType.Text;
                        command.CommandText = query;

                        try
                        {
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException sql)
                        {
                            retQry = "Erro ao executar update: " + sql.Message;
                            return false;
                        }
                    }

                }
                catch (Exception u)
                {
                    retQry = "Erro ao executar update: " + u.Message;
                    return false;
                }
            }
            else
            {

                try
                {
                    dsReturn.Clear();
                    dadapter.SelectCommand = new SqlCommand(query, myConnection);
                    dadapter.Fill(dsRetQry);
                }
                catch (SqlException d)
                {
                    retQry = "Erro ao executar dataset: " + d.Message;
                    return false;
                }

            }

            try
            {
                CloseCon();
            }
            catch (Exception c)
            {
                retQry = "Erro ao fechar conexão: " + c.Message;
                return false;
            }

            return true;
        }
        private static byte[] GetKey(string data)
        {
            string pwd = null;

            if (Encoding.UTF8.GetByteCount(data) < 24)
            {
                pwd = data.PadRight(24, ' ');
            }
            else
            {
                pwd = data.Substring(0, 24);
            }
            return Encoding.UTF8.GetBytes(pwd);
        }

        public static string Encrypt(string data)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Mode = CipherMode.ECB;
            DES.Key = GetKey("D@!Bn1s3l4(");

            DES.Padding = PaddingMode.PKCS7;
            ICryptoTransform DESEncrypt = DES.CreateEncryptor();
            Byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(data);

            return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

        public static string Decrypt(string data)
        {
            TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();

            DES.Mode = CipherMode.ECB;
            DES.Key = GetKey("D@!Bn1s3l4(");

            DES.Padding = PaddingMode.PKCS7;
            ICryptoTransform DESEncrypt = DES.CreateDecryptor();
            Byte[] Buffer = Convert.FromBase64String(data.Replace(" ", "+"));

            return Encoding.UTF8.GetString(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
        }

    }

}