using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace apoio_monsanto
{
    public class newmom
    {
        dbAcess dbAcess = new dbAcess();
        String query = "";
        public DataSet login(String login, String pass)
        {
            DataSet dsRetorno = new DataSet();

            query = " SELECT NAME, EMAIL, LOGIN, TYPE, CHANGEPASS, COOPERANTES, VISUALIZA, DISTRITO FROM [base_monsanto].[dbo].[NEW_REGISTER] WHERE TYPE in(1,2,3,4,5,6,99) AND LOGIN='" + login + "' AND PASS='" + dbAcess.Encrypt(pass) + "' AND ACTIVE=1";

            dbAcess.execSql("query", query);

            if (dbAcess.dsReturn != null && dbAcess.dsReturn.Tables[0].Rows.Count > 0)
            {
                dsRetorno = dbAcess.dsReturn;
            }

            return dsRetorno;
        }

        public String customer_add(int type, String name, String doc, String sap, String sap_filial, String regional, String unidade)
        {
            String ret = "";
            DataSet dsRet = new DataSet();
            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[NEW_CUSTOMER] WHERE name = '" + name.ToUpper() + "' AND document = '" + doc + "' ";
            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (Convert.ToInt16(dsRet.Tables[0].Rows[0][0]) > 0)
                {
                    ret = "Cliente já cadastrado!";
                    return ret;
                }
            }

            query = " INSERT INTO [base_monsanto].[dbo].[NEW_CUSTOMER] (name, document, sap, sap_filial, unidade, regional) ";
            query += " VALUES('" + name.ToUpper() + "', '" + doc + "', '" + sap + "', '" + sap_filial + "','" + unidade + "', '" + regional + "')";

            dbAcess.execSql("insert", query);

            ret = "Novo cliente cadastrado com sucesso!";
            return ret;
        }

        public DataSet retCustomers()
        {
            DataSet dsRet = new DataSet();

            // type 2 = Clientes

            query = " SELECT name, document, regional, unidade, agric  FROM [base_monsanto].[dbo].[NEW_CUSTOMER] WHERE DEL = '' ORDER BY ID DESC";
            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet searcCustomers(String name, String document, String id, String gla)
        {
            DataSet dsRet = new DataSet();

            // type 2 = Clientes

            query = " SELECT id, name, document, regional, unidade, sap, sap_filial FROM [base_monsanto].[dbo].[NEW_CUSTOMER] WHERE DEL is null ";

            if (!String.IsNullOrEmpty(name))
            {
                query += " AND name like '%" + name.ToUpper() + "%' ";
            }

            if (!String.IsNullOrEmpty(document))
            {
                query += " AND document like '%" + document + "%' ";
            }

            if (!String.IsNullOrEmpty(id))
            {
                query += " AND id = " + id + "";
            }

            query += " ORDER BY sap, sap_filial ";

            dbAcess.execSql("select", query);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_searcCustomer.txt", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public String changeCustomer(Int64 id, String name, String document, String delete, String sap, String sap_filial, String regional, String unidade)
        {
            String ret = "";
            DataSet dsRet = new DataSet();
            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[NEW_CUSTOMER] WHERE id = '" + id + "' ";
            dbAcess.execSql("select", query);

            if (String.IsNullOrEmpty(delete))
            {
                delete = "null";
            }
            else
            {
                delete = "'" + delete + "' ";
            }

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {

                query = " UPDATE [base_monsanto].[dbo].[NEW_CUSTOMER] SET name = '" + name.ToUpper() + "', document = '" + document + "', del = " + delete + " , sap = '" + sap + "', sap_filial = '" + sap_filial + "', regional = '" + regional + "', unidade = '" + unidade + "' ";
                query += " WHERE ID = " + id + " ";

                string path = HttpContext.Current.Server.MapPath("data");
                memowrit(path + "/arquivo_log_changeCustomer.txt", query);

                dbAcess.execSql("update", query);

                ret = "Dados Atualizados!";
            }

            return ret;
        }

        public string insertContract(string type, string id_client, ref string id_contract, string useradd)
        {
            string ret = "";
            string query = "";

            query = " SELECT ISNULL(MAX(ID),0)+1 INFO from [base_monsanto].[dbo].[NEW_CONTRACT] ";

            dbAcess.execSql("select", query);

            id_contract = dbAcess.dsReturn.Tables[0].Rows[0]["INFO"].ToString();

            query = " INSERT INTO [base_monsanto].[dbo].[NEW_CONTRACT] (type_contract, id_client, id, useradd, lastupduser) ";
            query += " VALUES ('{0}','{1}', '{2}', '{3}', '{3}')";

            query = String.Format(query, type, id_client, id_contract, useradd);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_insert.txt", query);

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string updateContract(string id, string dt_receb, string cultura, string obs, string status, string dt_status, string user_conf,
            string criteria, string id_user_rtv, string id_user_gr, string dt_digital, string dt_archive, string dt_approv, string keeper, string userupd, string tpDoc, string tpTermo)
        {
            string ret = "";
            string query = "";

            query = " UPDATE [base_monsanto].[dbo].[NEW_CONTRACT] SET dt_receb =  CONVERT(DATETIME, NULLIF('{0}',''),105), cultura = '{1}', obs = '{2}', status = '{3}', dt_status = CONVERT(DATETIME,NULLIF('{4}',''),105), user_conf = '{5}', criteria = '{6}', id_user_rtv = '{7}', id_user_gr = '{8}', dt_digital = CONVERT(DATETIME, NULLIF('{9}',''),105), dt_archive = CONVERT(DATETIME, NULLIF('{10}',''),105), dt_approv = CONVERT(DATETIME, NULLIF('{11}',''), 105), keeper = '{12}', lastupduser = '{13}', type_doc_prdsem = '{14}', term = '{15}' ";
            query += " WHERE id = " + id + "";

            query = String.Format(query, dt_receb, cultura, obs, status, dt_status, user_conf, criteria, id_user_rtv, id_user_gr,
                dt_digital, dt_archive, dt_approv, keeper, userupd, tpDoc, tpTermo);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_insert.txt", query);

            dbAcess.execSql("update", query);

            ret = dbAcess.retQuery;

            return ret;
        }
        public DataSet selectContract(String id)
        {
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();
            String query = "";

            query = " SELECT * ";
            query += " FROM [base_monsanto].[dbo].[NEW_CONTRACT] ";
            query += " WHERE id = " + id + "";

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_select_contract.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;

            return dsRet;
        }

        public DataSet selectFullContract(String id)
        {
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();
            String query = "";

            query = " SELECT * ";
            query += " FROM [base_monsanto].[dbo].[NEW_CONTRACT] ";
            query += " WHERE id = " + id + "";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectContractByClient(String id_client, String tpContrato, String CY, String cultura)
        {
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();
            String query = "";

            query = " SELECT * ";
            query += " FROM [base_monsanto].[dbo].[NEW_CONTRACT] ";
            query += " WHERE id_client = '" + id_client + "'";

            if (!string.IsNullOrEmpty(tpContrato))
                query += " AND type_contract = '" + tpContrato + "' ";

            if (!string.IsNullOrEmpty(CY))
                query += " AND cy = '" + CY + "' ";

            if (!string.IsNullOrEmpty(cultura))
                query += " AND cultura = '" + cultura + "' ";

            dbAcess.execSql("select", query);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/contract_clients.txt", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public String insertDocument(String id_contract, String id_client, String docname, String path)
        {
            String query = "";
            String ret = "";
            query = " INSERT INTO [base_monsanto].[dbo].[NEW_DOCUMENT] (id_contract, id_client, docname, path) ";
            query += " VALUES ('{0}','{1}', '{2}', '{3}')";

            query = String.Format(query, id_contract, id_client, docname, path);

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public DataSet selectDocument(String id_contract)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();

            query = " SELECT * FROM [base_monsanto].[dbo].[NEW_DOCUMENT] WHERE id_contract = '" + id_contract + "' ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0 && dsRet.Tables[0].Rows.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public String deleteDocByContract(String id_contract)
        {
            String query = "";
            String ret = "";
            query = " DELETE FROM [base_monsanto].[dbo].[NEW_DOCUMENT] ";
            query += " WHERE id_contract ='{0}' ";

            query = String.Format(query, id_contract);

            dbAcess.execSql("delete", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public String deleteDoc(String id)
        {
            String query = "";
            String ret = "";
            query = " DELETE FROM [base_monsanto].[dbo].[NEW_DOCUMENT] ";
            query += " WHERE id ='{0}' ";

            query = String.Format(query, id);

            dbAcess.execSql("delete", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public DataSet selectRTV()
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT '' name union all ";
            query += " SELECT name FROM [base_monsanto].[dbo].[NEW_REGISTER] WHERE del is null AND ACTIVE = 1 AND TYPE = 3 ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectRTVReport()
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT 'Todos' name union all ";
            query += " SELECT name FROM [base_monsanto].[dbo].[NEW_REGISTER] WHERE del is null AND ACTIVE = 1 AND TYPE = 3 ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectGR()
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT '' name union all ";
            query += " SELECT name FROM [base_monsanto].[dbo].[NEW_REGISTER] WHERE del is null AND ACTIVE = 1 AND TYPE = 4 ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectGRReport()
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT 'Todos' name union all ";
            query += " SELECT name FROM [base_monsanto].[dbo].[NEW_REGISTER] WHERE del is null AND ACTIVE = 1 AND TYPE = 4 ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public void memowrit(string path, string data)
        {
            try
            {


                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path))
                {
                    sw.Write(data);
                }

            }
            catch (Exception)
            {


            }
        }

        public void valDocCont(string id_contract, string docname)
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT id_doc FROM [base_monsanto].[dbo].[NEW_CONTRACT] WHERE id = " + id_contract + " ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (String.IsNullOrEmpty(dsRet.Tables[0].Rows[0][0].ToString()))
                {
                    query = " UPDATE [base_monsanto].[dbo].[NEW_CONTRACT] SET id_doc = '" + docname + "' WHERE id = " + id_contract + "";

                    dbAcess.execSql("update", query);
                }
            }
        }

        public bool changePass(string username, string newPass, string conPass)
        {
            string query = "";

            try
            {
                query = " UPDATE [base_monsanto].[dbo].[NEW_REGISTER] set pass = '" + dbAcess.Encrypt(conPass) + "', changepass = 'S' ";
                query += " WHERE login = '" + username + "' ";

                dbAcess.execSql("update", query);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public String insertUser(string name, string email, string login, string type, string active,
            string distrito, string regional, string cont1, string cont2, string endereco, string usergla)
        {
            String ret = "";
            DataSet dsRet = new DataSet();
            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[NEW_REGISTER] WHERE login = '" + login + "' AND (del is null or del = '') ";
            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (Convert.ToInt16(dsRet.Tables[0].Rows[0][0]) > 0)
                {
                    ret = "Usuário já cadastrado!";
                    return ret;
                }
            }

            query = " INSERT INTO [base_monsanto].[dbo].[NEW_REGISTER] (name, email, login, type, active, distrito, regional, contato1, contato2, endereco, usergla) ";
            query += " VALUES('" + name.ToUpper() + "', '" + email + "', '" + login + "', '" + type + "', '" + active + "', '" + distrito + "','" + regional + "','" + cont1 + "','" + cont2 + "','" + endereco + "','" + usergla + "')";

            dbAcess.execSql("insert", query);

            ret = "Novo usuário cadastrado com sucesso!";
            return ret;
        }

        public DataSet selectUsers(string name, string email, string login, string type,
            string distrito, string regional, string cont1, string cont2, string endereco)
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT login, name, email, case when active = 1 then 'ATIVO' else 'INATIVO' end status, regional FROM [base_monsanto].[dbo].[NEW_REGISTER] ";
            query += " WHERE del is null AND TYPE = " + type + " ";
            if (!string.IsNullOrEmpty(login))
                query += " AND login like '%" + login + "%'";
            if (!string.IsNullOrEmpty(name))
                query += " AND name like '%" + name + "%'";
            if (!string.IsNullOrEmpty(email))
                query += " AND email like '%" + email + "%'";
            if (!string.IsNullOrEmpty(distrito))
                query += " AND distrito like '%" + distrito + "%'";
            if (!string.IsNullOrEmpty(regional))
                query += " AND regional like '%" + regional + "%'";
            if (!string.IsNullOrEmpty(cont1))
                query += " AND contato1 like '%" + cont1 + "%'";
            if (!string.IsNullOrEmpty(cont2))
                query += " AND contato2 like '%" + cont2 + "%'";
            if (!string.IsNullOrEmpty(endereco))
                query += " AND endereco like '%" + endereco + "%'";

            query += " ORDER BY name ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectDatUsers(string login, string type)
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT * FROM [base_monsanto].[dbo].[NEW_REGISTER] ";
            query += " WHERE del is null AND TYPE = " + type + " ";
            query += " AND login = '" + login + "'";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public void deleteUser(string login)
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT login FROM [base_monsanto].[dbo].[NEW_REGISTER] WHERE login = '" + login + "' ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (!String.IsNullOrEmpty(dsRet.Tables[0].Rows[0][0].ToString()))
                {
                    query = " UPDATE [base_monsanto].[dbo].[NEW_REGISTER] SET del = '*' WHERE login = '" + login + "'";

                    dbAcess.execSql("update", query);
                }
            }
        }

        public void deleteContract(string id_contract)
        {
            String query = "";

            query = " DELETE FROM [base_monsanto].[dbo].[NEW_CONTRACT] WHERE id = '" + id_contract + "'";

            dbAcess.execSql("delete", query);
        }

        public String updateUser(string name, string email, string login, string type, string active,
            string distrito, string regional, string cont1, string cont2, string endereco)
        {
            String ret = "";
            DataSet dsRet = new DataSet();
            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[NEW_REGISTER] WHERE login = '" + login + "' ";
            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (Convert.ToInt16(dsRet.Tables[0].Rows[0][0]) == 0)
                {
                    ret = "Usuário não encontrado!";
                    return ret;
                }
            }

            query = " UPDATE [base_monsanto].[dbo].[NEW_REGISTER] set name = '" + name.ToUpper() + "', ";
            query += " email = '" + email + "', ";
            query += " type =  '" + type + "', ";
            query += "active = '" + active + "', ";
            query += "distrito = '" + distrito + "',";
            query += "regional = '" + regional + "',";
            query += "contato1 = '" + cont1 + "',";
            query += "contato2 = '" + cont2 + "',";
            query += "endereco = '" + endereco + "'";
            query += "WHERE login = '" + login + "' ";

            dbAcess.execSql("update", query);

            ret = "Novo usuário alterado com sucesso!";
            return ret;
        }

        public string sentEmail(string mailTo, string mailMsg, string mailSubject)
        {
            try
            {
                //Define os dados do e-mail
                string nomeRemetente = "Apoio Monsanto";
                string emailRemetente = "sistema@apoioflc.com.br";
                string senha = "sistemaapoio001";

                //Host da porta SMTP
                string SMTP = "200.234.210.13";

                string emailDestinatario = mailTo;
                //string emailComCopia        = "email@comcopia.com.br";
                //string emailComCopiaOculta  = "email@comcopiaoculta.com.br";

                string assuntoMensagem = mailSubject;
                string conteudoMensagem = mailMsg;

                //Cria objeto com dados do e-mail.
                MailMessage objEmail = new MailMessage();

                //Define o Campo From e ReplyTo do e-mail.
                objEmail.From = new System.Net.Mail.MailAddress(nomeRemetente + "<" + emailRemetente + ">");

                //Define os destinatários do e-mail.
                objEmail.To.Add(emailDestinatario);

                //Enviar cópia para.
                //objEmail.CC.Add(emailComCopia);

                //Enviar cópia oculta para.
                //objEmail.Bcc.Add(emailComCopiaOculta);

                //Define a prioridade do e-mail.
                objEmail.Priority = System.Net.Mail.MailPriority.Normal;

                //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
                objEmail.IsBodyHtml = true;

                //Define título do e-mail.
                objEmail.Subject = assuntoMensagem;

                //Define o corpo do e-mail.
                objEmail.Body = conteudoMensagem;

                //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
                objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

                // Caso queira enviar um arquivo anexo
                //Caminho do arquivo a ser enviado como anexo
                //string arquivo = Server.MapPath("arquivo.jpg");

                // Ou especifique o caminho manualmente
                //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";

                // Cria o anexo para o e-mail
                //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);

                // Anexa o arquivo a mensagem
                //objEmail.Attachments.Add(anexo);

                //Cria objeto com os dados do SMTP
                System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient();

                //Alocamos o endereço do host para enviar os e-mails  
                objSmtp.Credentials = new System.Net.NetworkCredential(emailRemetente, senha);
                objSmtp.Host = SMTP;
                objSmtp.Port = 587;
                //Caso utilize conta de email do exchange da locaweb deve habilitar o SSL
                //objEmail.EnableSsl = true;

                //Enviamos o e-mail através do método .send()
                try
                {
                    objSmtp.Send(objEmail);
                    return "sent";
                }
                catch (Exception ex)
                {
                    return "Falha: " + ex.Message;
                }
                finally
                {
                    //excluímos o objeto de e-mail da memória
                    objEmail.Dispose();
                    //anexo.Dispose();
                }
            }
            catch (Exception ex)
            {

                return "Falha: " + ex.Message;
            }
        }

        public string retUserMail(String name)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT distinct email FROM [base_monsanto].[dbo].[NEW_REGISTER] ";
            query += " WHERE del is null AND name = '" + name + "' ";

            dbAcess.stringSql(query);

            return dbAcess.retQuery;
        }

        public string retUserMailbyLogin(String login)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT distinct email FROM [base_monsanto].[dbo].[NEW_REGISTER] ";
            query += " WHERE del is null AND login = '" + login + "' ";

            dbAcess.stringSql(query);

            return dbAcess.retQuery;
        }

        public string retNameCont(String id_contract)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT id_doc FROM [base_monsanto].[dbo].[NEW_CONTRACT] ";
            query += " WHERE id = " + id_contract + " ";

            dbAcess.stringSql(query);

            return dbAcess.retQuery;
        }

        public string insertLog(string oldValue, string newValue, string id_contract, string field, string userName, string tableName)
        {
            string query = "";
            string ret = "";

            query = " INSERT INTO [base_monsanto].[dbo].[NEW_AUDIT]( type, TableName, FieldName, OldValue, NewValue, UpdateDate, UserName, PK )";
            query += " VALUES( '{0}', '{1}', '{2}', '{3}', '{4}', GETDATE(), '{5}', '{6}' )";

            query = string.Format(query, "U", tableName, field, oldValue, newValue, userName, id_contract);

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public DataSet selectLog(String reference, string doccli, string user)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();

            query = @" SELECT FIELDNAME, OLDVALUE, NEWVALUE, UPDATEDATE, USERNAME, ID_DOC PK
                        FROM [base_monsanto].[dbo].[NEW_AUDIT] A
                        INNER JOIN [base_monsanto].[dbo].[NEW_CONTRACT] D
                        ON D.ID = REPLACE(REPLACE(A.PK, '<ID=', ''), '>', '')
                        INNER JOIN [base_monsanto].[dbo].[NEW_CUSTOMER] C
                        ON C.ID = D.ID_CLIENT";

            if (!String.IsNullOrEmpty(reference))
            {
                query += @" AND D.ID_DOC LIKE UPPER('%{0}%') ";

                query = String.Format(query, reference);
            }

            if (!String.IsNullOrEmpty(doccli))
            {
                query += @" AND C.DOCUMENT LIKE '%{0}%' ";

                query = String.Format(query, doccli);
            }

            if (!String.IsNullOrEmpty(user))
            {
                query += " AND USERNAME LIKE '%" + user + "%'";
            }

            query += " ORDER BY UPDATEDATE DESC";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectPermission()
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT * FROM [base_monsanto].[dbo].[NEW_PERMISSION] ORDER BY dt_register DESC";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public string insertPermission(string id_user, string dtIni, string dtFim, string profile)
        {
            String query = "";
            String ret = "";
            dbAcess.retQuery = "";

            query = " INSERT INTO [base_monsanto].[dbo].[NEW_PERMISSION] (id_user, dt_perm_ini, dt_perm_end, profile, dt_register)";
            query += " VALUES ('{0}', CAST(NULLIF('{1}','') as datetime), CAST(NULLIF('{2}','') as datetime), '{3}', GETDATE() )";

            query = String.Format(query, id_user, dtIni, dtFim, profile);

            dbAcess.execSql("insert", query);

            updatePermission(id_user, "INSERT");

            ret = dbAcess.retQuery;

            return ret;
        }

        public string updatePermission(string id_user, string operation)
        {
            String query = "";
            String ret = "";
            int type = 0;
            dbAcess.retQuery = "";

            if (operation == "VALIDATE")
            {
                query = " SELECT ISNULL(MAX(CASE WHEN CONVERT(DATETIME,DT_PERM_INI,105) < CONVERT(DATETIME,GETDATE(),105) THEN 'ALLOWED' ELSE 'FORBIDDEN' END),'') ";
                query += " FROM [base_monsanto].[dbo].[NEW_PERMISSION] WHERE id_user = '" + id_user + "' ";

                dbAcess.stringSql(query);

                if (dbAcess.retQuery == "ALLOWED" || dbAcess.retQuery == "")
                    return "OK";
            }

            if (operation == "DELETE" || operation == "VALIDATE")
                type = 2;

            if (operation == "INSERT")
                type = 1;

            query = " UPDATE [base_monsanto].[dbo].[NEW_REGISTER] set TYPE = " + type + " where login = '" + id_user + "' ";

            dbAcess.execSql("update", query);

            ret = dbAcess.retQuery;

            return ret;
        }
        /// <summary>
        /// NOT GR OR RTV
        /// </summary>
        /// <returns></returns>
        public DataSet selectAllActiveUsers()
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT name, login FROM [base_monsanto].[dbo].[NEW_REGISTER] WHERE del is null AND type = 2 ORDER BY 1 DESC";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public String deletePermission(String id_user)
        {
            String query = "";
            String ret = "";
            query = " DELETE FROM [base_monsanto].[dbo].[NEW_PERMISSION] ";
            query += " WHERE id_user ='{0}' ";

            query = String.Format(query, id_user);

            dbAcess.execSql("delete", query);

            updatePermission(id_user, "DELETE");

            ret = dbAcess.retQuery;

            return ret;
        }

        public String retPassDecrypt(string login)
        {
            query = " SELECT PASS FROM [base_monsanto].[dbo].[NEW_REGISTER] WHERE del is null AND login = '" + login + "' ";

            dbAcess.stringSql(query);

            return dbAcess.Decrypt(dbAcess.retQuery);
        }

        public String passForget(string login)
        {
            string mail = retUserMailbyLogin(login);

            if (String.IsNullOrEmpty(mail))
            {
                return "Não há registro para o usuário informado!";
            }
            else
            {
                string htmlMail = @"<table>
	                                <tr>
		                                <td colspan='2'><strong>Senha de Acesso - Apoio Monsanto</strong>
		                                </td>
	                                </tr>
                                    <tr>
		                                <td colspan='2'>
			                                &nbsp;
		                                </td>
	                                </tr>
	                                <tr>
		                                <td colspan='2'>
			                                Conforme solicitado, sua senha de acesso é: {0}. <br/>
                                            Caso você não tenha solicitado esse e-mail, gentileza entrar em contato com Apoio Monsanto ou alterar sua senha imediatamente.
		                                </td>
	                                </tr>
                                    <tr>
		                                <td colspan='2'>
			                                &nbsp;
		                                </td>
	                                </tr>
	                                <tr>
		                                <td colspan='2'>
			                                <span style='font-size:10px'>E-mail automático. Por favor, não responda. Powered by Apoio Monsanto</span>
		                                </td>
	                                </tr>
                                </table>";

                htmlMail = string.Format(htmlMail, retPassDecrypt(login));

                sentEmail(mail, htmlMail, "Senha de Acesso - Monsanto");

                return "Senha de acesso enviada para e-mail de cadastro";

            }
        }

        public DataSet selectAllCriteria(string type)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT * FROM [base_monsanto].[dbo].[NEW_CRITERIA] ";
            if (!String.IsNullOrEmpty(type))
            {
                query += " WHERE type = '" + type + "' ";
            }
            query += "    ORDER BY 1 ASC";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public string insertCriteria(string criteria, string type)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = "INSERT INTO [base_monsanto].[dbo].[NEW_CRITERIA] (criteria, type) VALUES ('" + criteria + "','" + type + "') ";

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string deleteCriteria(string id_criteria)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = "DELETE FROM [base_monsanto].[dbo].[NEW_CRITERIA] WHERE id = " + id_criteria + " ";

            dbAcess.execSql("delete", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public DataSet selectAllRegion()
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT * FROM [base_monsanto].[dbo].[NEW_REGIONS] ";
            query += "    ORDER BY 1 ASC";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }



        public string insertRegion(string region)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = "INSERT INTO [base_monsanto].[dbo].[NEW_REGIONS] (region) VALUES ('" + region + "') ";

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string deleteRegion(string id_region)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[NEW_REGISTER] where regional = (select distinct region from [base_monsanto].[dbo].[NEW_REGIONS] where id = '" + id_region + "') ";

            dbAcess.stringSql(query);

            if (dbAcess.retQuery != "0")
                ret = "Exclusão não permitida!";
            else
            {
                query = "DELETE FROM [base_monsanto].[dbo].[NEW_REGIONS] WHERE id = " + id_region + " ";

                dbAcess.execSql("delete", query);

                ret = dbAcess.retQuery;
            }



            return ret;
        }

        public DataSet selectAllSafra()
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT * FROM [base_monsanto].[dbo].[NEW_SAFRA] ";
            query += "    ORDER BY 1 ASC";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public string insertSafra(string safra)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = "INSERT INTO [base_monsanto].[dbo].[NEW_SAFRA] (safra) VALUES ('" + safra + "') ";

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string deleteSafra(string id_safra)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[NEW_CONTRACT] where safra = (select distinct safra from [base_monsanto].[dbo].[NEW_SAFRA] where id = '" + id_safra + "') ";

            dbAcess.stringSql(query);

            if (dbAcess.retQuery != "0")
                ret = "Exclusão não permitida!";
            else
            {
                query = "DELETE FROM [base_monsanto].[dbo].[NEW_SAFRA] WHERE ID = " + id_safra + " ";

                dbAcess.execSql("delete", query);

                ret = dbAcess.retQuery;
            }



            return ret;
        }


        public DataSet selectContAproved(string gr, string rtv, string safra, string regional, string document, string tpCont, string tpDoc)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = @" SELECT(CAST(mes as INT)) mes, SUM(total) total FROM (SELECT SUBSTRING(CAST(dt_receb AS VARCHAR),6,2) mes, COUNT(distinct c.id) total ";
            query += " FROM [base_monsanto].[dbo].[NEW_CONTRACT] c ";
            query += " LEFT JOIN base_monsanto.dbo.register AS gr on gr.name = id_user_gr AND gr.type = 4 ";
            query += " LEFT JOIN base_monsanto.dbo.register AS rtv on rtv.name = id_user_rtv AND rtv.type = 3";
            query += " LEFT JOIN base_monsanto.dbo.customer AS client on client.ID = id_client";
            query += "  WHERE (dt_receb IS NOT NULL AND dt_receb <> '') ";
            query += "  AND status = 1 ";
            query += "  AND safra = '" + safra + "' ";

            if (!String.IsNullOrEmpty(gr) && gr != "Todos")
                query += " AND id_user_gr = '" + gr + "'";

            if (!String.IsNullOrEmpty(rtv) && rtv != "Todos")
                query += " AND id_user_rtv = '" + rtv + "'";

            if (!String.IsNullOrEmpty(regional) && regional != "Todos")
                query += " AND (gr.regional = '" + regional + "' or rtv.regional = '" + regional + "')";

            if (!String.IsNullOrEmpty(document))
                query += " AND client.document = '" + document + "' ";

            if (!String.IsNullOrEmpty(tpCont) && tpCont != "Todos")
                query += " AND type_contract = '" + tpCont + "' ";

            if (!String.IsNullOrEmpty(tpDoc) && tpCont == "3")
                query += " AND type_doc_prdsem = '" + tpDoc + "' ";

            query += "  GROUP BY dt_receb) tot ";
            query += "  GROUP BY mes ORDER BY 1";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectContReproved(string gr, string rtv, string safra, string regional, string document, string tpCont, string tpDoc)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = @" SELECT(CAST(mes as INT)) mes, SUM(total) total FROM (SELECT SUBSTRING(CAST(dt_receb AS VARCHAR),6,2) mes, COUNT(distinct c.id) total ";
            query += " FROM [base_monsanto].[dbo].[NEW_CONTRACT] c";
            query += " LEFT JOIN base_monsanto.dbo.register AS gr on gr.name = id_user_gr AND gr.type = 4 ";
            query += " LEFT JOIN base_monsanto.dbo.register AS rtv on rtv.name = id_user_rtv AND rtv.type = 3";
            query += " LEFT JOIN base_monsanto.dbo.customer AS client on client.ID = id_client";
            query += "  WHERE (dt_receb IS NOT NULL AND dt_receb <> '') ";
            query += "  AND status = 2 ";
            query += "  AND safra = '" + safra + "' ";

            if (!String.IsNullOrEmpty(gr) && gr != "Todos")
                query += " AND id_user_gr = '" + gr + "'";

            if (!String.IsNullOrEmpty(rtv) && rtv != "Todos")
                query += " AND id_user_rtv = '" + rtv + "'";

            if (!String.IsNullOrEmpty(regional) && regional != "Todos")
                query += " AND (gr.regional = '" + regional + "' or rtv.regional = '" + regional + "')";

            if (!String.IsNullOrEmpty(document))
                query += " AND client.document = '" + document + "' ";

            if (!String.IsNullOrEmpty(tpCont) && tpCont != "Todos")
                query += " AND type_contract = '" + tpCont + "' ";

            if (!String.IsNullOrEmpty(tpDoc) && tpCont == "3")
                query += " AND type_doc_prdsem = '" + tpDoc + "' ";

            query += "  GROUP BY dt_receb) tot ";
            query += "  GROUP BY mes ORDER BY 1";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectContAll(string gr, string rtv, string safra, string regional, string document, string tpCont, string tpDoc)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = @" SELECT(CAST(mes as INT)) mes, SUM(total) total FROM (SELECT SUBSTRING(CAST(dt_receb AS VARCHAR),6,2) mes, COUNT(distinct c.id) total ";
            query += " FROM [base_monsanto].[dbo].[NEW_CONTRACT] c";
            query += " LEFT JOIN base_monsanto.dbo.register AS gr on gr.name = id_user_gr AND gr.type = 4 ";
            query += " LEFT JOIN base_monsanto.dbo.register AS rtv on rtv.name = id_user_rtv AND rtv.type = 3";
            query += " LEFT JOIN base_monsanto.dbo.customer AS client on client.ID = id_client";
            query += "  WHERE (dt_receb IS NOT NULL AND dt_receb <> '') ";
            query += "  AND safra = '" + safra + "' ";

            if (!String.IsNullOrEmpty(gr) && gr != "Todos")
                query += " AND id_user_gr = '" + gr + "'";

            if (!String.IsNullOrEmpty(rtv) && rtv != "Todos")
                query += " AND id_user_rtv = '" + rtv + "'";

            if (!String.IsNullOrEmpty(regional) && regional != "Todos")
                query += " AND (gr.regional = '" + regional + "' or rtv.regional = '" + regional + "')";

            if (!String.IsNullOrEmpty(document))
                query += " AND client.document = '" + document + "' ";

            if (!String.IsNullOrEmpty(tpCont) && tpCont != "Todos")
                query += " AND type_contract = '" + tpCont + "' ";

            if (!String.IsNullOrEmpty(tpDoc) && tpCont == "3")
                query += " AND type_doc_prdsem = '" + tpDoc + "' ";

            query += "  GROUP BY dt_receb) tot ";
            query += "  GROUP BY mes ORDER BY 1";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectDetailAll(string gr, string rtv, string safra, string regional, string document, string tpCont, string tpDoc, string tpTermo)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = @" SELECT distinct client.name as 'Nome do Cliente', ";
            query += "  client.document as 'Documento do Cliente', ";
            query += "   safra as 'Safra', c.id as 'ID', ";
            query += "   case when type_contract = 1 then 'Distribuidor' ";
            query += " when type_contract = 2 then 'POD' ";
            query += " when type_contract = 3 then 'Produtor de Sementes' ";
            query += " when type_contract = 4 then 'Monsoy' ";
            query += " else '' end as 'Tipo de Contrato', ";
            query += "    substring(CAST(dt_receb as varchar),9,2) + '/' +  substring(CAST(dt_receb as varchar),6,2) + '/' + substring(CAST(dt_receb as varchar),1,4) as 'Data de Recebimento', ";
            query += "    case when status = 1 then 'Aprovado' else 'Reprovado' end as 'Status', ";
            query += "   id_user_rtv as 'RTV', ";
            query += "    id_user_gr as 'GR', ";
            query += "   substring(CAST(dt_digital as varchar),9,2) + '/' +  substring(CAST(dt_digital as varchar),6,2) + '/' + substring(CAST(dt_digital as varchar),1,4) as 'Data de Digitalização', ";
            query += "    substring(CAST(dt_approv as varchar),9,2) + '/' +  substring(CAST(dt_approv as varchar),6,2) + '/' + substring(CAST(dt_approv as varchar),1,4) as 'Data de Aprovação', ";
            query += "   substring(CAST(dt_archive as varchar),9,2) + '/' +  substring(CAST(dt_archive as varchar),6,2) + '/' + substring(CAST(dt_archive as varchar),1,4) as 'Data Keepers', ";
            query += "   keeper as 'N. Caixa Keepers', case when term = 'at' then 'Aditivo ao Termo' when term = 'ta' then 'Termo de Aditivo' else '' end as 'Tipo de Termo' ";
            query += " FROM [base_monsanto].[dbo].[NEW_CONTRACT]c ";
            query += " LEFT JOIN base_monsanto.dbo.register AS gr on gr.name = id_user_gr AND gr.type = 4 ";
            query += " LEFT JOIN base_monsanto.dbo.register AS rtv on rtv.name = id_user_rtv AND rtv.type = 3";
            query += " LEFT JOIN base_monsanto.dbo.customer AS client on client.ID = id_client";
            query += "  WHERE (dt_receb IS NOT NULL AND dt_receb <> '') ";
            query += "  AND safra = '" + safra + "' ";
            query += "  AND status in (1,2) ";

            if (!String.IsNullOrEmpty(gr) && gr != "Todos")
                query += " AND id_user_gr = '" + gr + "'";

            if (!String.IsNullOrEmpty(rtv) && rtv != "Todos")
                query += " AND id_user_rtv = '" + rtv + "'";

            if (!String.IsNullOrEmpty(regional) && regional != "Todos")
                query += " AND (gr.regional = '" + regional + "' or rtv.regional = '" + regional + "')";

            if (!String.IsNullOrEmpty(document))
                query += " AND client.document = '" + document + "' ";

            if (!String.IsNullOrEmpty(tpCont) && tpCont != "Todos")
                query += " AND type_contract = '" + tpCont + "' ";

            if (!String.IsNullOrEmpty(tpDoc) && tpCont == "3")
                query += " AND type_doc_prdsem = '" + tpDoc + "' ";

            if (!String.IsNullOrEmpty(tpTermo))
                query += " AND term = '" + tpTermo + "' ";

            query += "  ORDER BY 5";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet retDistByRTV(String rtv)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            rtv = String.IsNullOrEmpty(rtv) ? rtv : rtv.ToUpper();

            query = " SELECT distinct distrito FROM [base_monsanto].[dbo].[NEW_REGISTER] WHERE name = '" + rtv + "' and type = 3 and (del = '' or del is null) ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet retRegiByGR(String gr)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            gr = String.IsNullOrEmpty(gr) ? gr : gr.ToUpper();

            query = " SELECT distinct regional FROM [base_monsanto].[dbo].[NEW_REGISTER] WHERE name = '" + gr + "' and type = 4 ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public Boolean valueValidation(String value, int type)
        {
            bool ret = false;
            string query = "";

            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[NEW_REGISTER] WHERE name = '" + value + "' and type = " + type + " ";

            dbAcess.stringSql(query);

            if (dbAcess.retQuery != "0")
                ret = true;

            return ret;
        }

        public DataSet selectAllGeneral(string gla_cad_type, string gla_cad_value)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT CASE WHEN gla_cad_type = 'cy' THEN 'CY'" +
                    " WHEN gla_cad_type = 'ma' THEN 'Marca'" +
                    " WHEN gla_cad_type = 're' THEN 'Regional'" +
                    " WHEN gla_cad_type = 'un' THEN 'Unidade' ELSE '' END gla_cad_type," +
                    " gla_cad_value, id_gla_cad_generic FROM [base_monsanto].[dbo].[new_cad_GENERIC] WHERE id_gla_cad_generic > 0";

            if (!string.IsNullOrEmpty(gla_cad_type))
                query += " AND gla_cad_type = '" + gla_cad_type + "' ";

            if (!string.IsNullOrEmpty(gla_cad_value))
                query += " AND gla_cad_value = '" + gla_cad_value + "' ";

            //query += " WHERE gla_cad_type = '" + gla_cad_type + "' ";
            query += "    ORDER BY 1 ASC";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectAllGlaCad(string gla_cad_type)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT '' GLA_VAL UNION ALL";
            query += " SELECT gla_cad_value GLA_VAL FROM [base_monsanto].[dbo].[new_cad_GENERIC] ";
            query += " WHERE gla_cad_type = '" + gla_cad_type + "' ";
            //query += "    ORDER BY 1 ASC";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public string insertGlaCad(string gla_cad_type, string gla_cad_value)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = "INSERT INTO [base_monsanto].[dbo].[new_cad_GENERIC] (gla_cad_type,gla_cad_value) VALUES ('" + gla_cad_type + "','" + gla_cad_value + "') ";

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string deleteGlaCad(string id_gla_cad_generic)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = "DELETE FROM [base_monsanto].[dbo].[new_cad_GENERIC] WHERE id_gla_cad_generic = " + id_gla_cad_generic + " ";

            dbAcess.execSql("delete", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        /// <summary>
        /// Main Function to Create, Update and Delete database data
        /// </summary>
        /// <param name="Operation"></param>
        /// <param name="TableName"></param>
        /// <param name="TableFields"></param>
        /// <param name="TableValues"></param>
        /// <param name="IdField"></param>
        /// <param name="IdValue"></param>
        /// <returns></returns>
        public String Main_CUD(String Operation, String TableName, List<String> TableFields, List<String> TableValues, String IdField, String IdValue, bool showID)
        {
            // variable that will be the query
            String qryOper = "";
            // return variable
            String Ret = "";
            // auxiliar query
            String auxQuery = "";
            DataSet dsAuxQuery = new DataSet();
            String dBase = "BASE_MONSANTO";

            // validate initial operation
            switch (Operation)
            {
                case "insert":
                    qryOper = " INSERT INTO [" + dBase + "].[dbo].{0} ({1}) VALUES ({2}) ";
                    break;
                case "update":
                    qryOper = " UPDATE [" + dBase + "].[dbo].{0} SET {1} WHERE {2} = '{3}' ";
                    break;
                case "delete":
                    qryOper = " DELETE FROM [" + dBase + "].[dbo].{0} WHERE {1} = '{2}'";
                    break;

            }

            // add shit
            if (Operation == "insert")
            {
                // variable that receive current information
                int numFields = TableFields == null ? 0 : TableFields.Count;
                // all fields separated
                string detailedFields = "";

                if (numFields == 0)
                {
                    auxQuery = " SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = UPPER('" + TableName + "') AND COLUMN_NAME <> UPPER('ID') ";
                    dbAcess.execSql("select", auxQuery);
                    dsAuxQuery = dbAcess.dsReturn;

                    // all array
                    for (int d = 0; d < dsAuxQuery.Tables[0].Rows.Count; d++)
                    {
                        // i did this just to not set the comma at the end
                        if ((d + 1) == dsAuxQuery.Tables[0].Rows.Count)
                            detailedFields += dsAuxQuery.Tables[0].Rows[d]["COLUMN_NAME"].ToString();
                        else
                            detailedFields += dsAuxQuery.Tables[0].Rows[d]["COLUMN_NAME"].ToString() + ", ";
                    }
                }
                else
                {

                    // all array
                    for (int i = 0; i < numFields; i++)
                    {
                        // i did this just to not set the comma at the end
                        if ((i + 1) == numFields)
                            detailedFields += TableFields[i].ToString();
                        else
                            detailedFields += TableFields[i].ToString() + ",";
                    }
                }

                // now, values
                int numValues = TableValues.Count();
                // all values splited
                string detailedValues = "";

                // all array
                for (int v = 0; v < numValues; v++)
                {
                    // i did this again to not set the comma at the end
                    if ((v + 1) == numValues)
                        detailedValues += "'" + TableValues[v].ToString() + "'";
                    else
                        detailedValues += "'" + TableValues[v].ToString() + "', ";
                }

                // at this point we already have all the values and fields to insert
                qryOper = String.Format(qryOper, TableName, detailedFields, detailedValues);


            }

            // now update bad shit
            if (Operation == "update")
            {
                // variable that receive current information
                int numFields = TableFields == null ? 0 : TableFields.Count();
                // all fields separated
                string updateFields = "";

                if (numFields == 0)
                {
                    // if the fields are not filled into variable
                    TableFields = retTableFields(TableName, showID);
                    numFields = TableFields.Count();
                }

                // all array
                for (int i = 0; i < numFields; i++)
                {
                    // i did this again to not set the comma at the end
                    if ((i + 1) == numFields)
                        updateFields += TableFields[i].ToString() + " = " + "'" + TableValues[i].ToString() + "' ";
                    else
                        updateFields += TableFields[i].ToString() + " = " + "'" + TableValues[i].ToString() + "', ";
                }

                // at this point we already have all information to update
                qryOper = String.Format(qryOper, TableName, updateFields, IdField, IdValue);
            }

            if (Operation == "delete")
            {
                // at this point we already have all information to delete
                qryOper = String.Format(qryOper, TableName, IdField, IdValue);
            }

            try
            {
                string path = HttpContext.Current.Server.MapPath("data");
                memowrit(path + "/arquivo_log_cud.txt", qryOper);

                // now, we run the query executioner
                dbAcess.retQuery = "";
                dbAcess.execSql(Operation, qryOper);
                Ret = dbAcess.retQuery;
                Ret = String.IsNullOrEmpty(Ret) ? "Dados Salvos!" : Ret;


            }
            catch (Exception ex)
            {
                Ret = "Error Function Main_CUD: " + Operation + " - " + ex.Message;
            }

            return Ret;
        }

        public List<string> retTableFields(String TableName, bool showID)
        {
            String auxQuery = "";
            DataSet dsAuxQuery = new DataSet();
            List<string> retList = new List<string>();
            dbAcess.dsReturn.Clear();

            auxQuery = " SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = UPPER('" + TableName + "') ";
            if (!showID)
                auxQuery += " AND COLUMN_NAME <> UPPER('ID') ";

            dbAcess.execSql("select", auxQuery);
            dsAuxQuery = dbAcess.dsReturn;

            // all array
            for (int d = 0; d < dsAuxQuery.Tables[0].Rows.Count - 2; d++)
            {
                retList.Add(dsAuxQuery.Tables[0].Rows[d]["COLUMN_NAME"].ToString());
            }

            return retList;
        }

        public DataSet selectCustomerAndContract(string name, string document, string sap, string distrito, string unidade, string regional, string sap_filial)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = @" SELECT DISTINCT ID, NAME, DOCUMENT, SAP, SAP_FILIAL FROM [BASE_MONSANTO].[DBO].[NEW_CUSTOMER] C 
                        WHERE DEL IS NULL ";

            if (!string.IsNullOrEmpty(distrito) || !string.IsNullOrEmpty(unidade) || !string.IsNullOrEmpty(regional))
            {
                query += @"  AND EXISTS( 
                        SELECT* FROM NEW_CONTRACT CON WHERE CON.ID_CLIENT = C.ID";

                if (!string.IsNullOrEmpty(distrito))
                    query += " AND DISTRITO like '%" + distrito + "%' ";

                if (!string.IsNullOrEmpty(unidade))
                    query += " AND UNIDADE like '%" + unidade + "%' ";

                if (!string.IsNullOrEmpty(regional))
                    query += " AND REGIONAL like '%" + regional + "%' ";

                query += " ) ";
            }

            if (!string.IsNullOrEmpty(name))
                query += " AND name like '%" + name + "%'";

            if (!string.IsNullOrEmpty(document))
                query += " AND document like '%" + document + "%'";

            if (!string.IsNullOrEmpty(sap))
                query += " AND SAP like '%" + sap + "%'";

            if (!string.IsNullOrEmpty(sap_filial))
                query += " AND SAP_FILIAL like '%" + sap_filial + "%'";

            query += " ORDER BY SAP, SAP_FILIAL, NAME";

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_main_select.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet reportGLA(string name, string sap, string cy, string cultura, string tipo_contrato)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = @" select con.id Contrato, case when type_contract = 1 then 'Contrato de Distribuição Comercial' 
                            when type_contract = 2 then 'Termo de Participação no Programa de Vendas'
                            when type_contract = 3 then 'Carta de Quitação' 
                            when type_contract = 4 then 'Contrato ATS'
                            when type_contract = 5 then 'Contrato Qion Implantação'
                            when type_contract = 6 then 'Contrato Qion Licenciamento'
                            when type_contract = 7 then 'Outros (Documentos Diversos)'
                            when type_contract = 8 then 'FCPA'
                            else 'Undefined' end Tipo, cus.name Cliente, 
                            dbo.formatarCNPJ(Replace(Replace(Replace(cus.document, '.',''),'-',''),'/','')) Documento, 
cus.sap Sap, cus.sap_filial SapFilial, dt_contract DataContrato, cy CY, cultura Cultura, distrito Marca, con.unidade Unidade, distrito Regional 
                            from NEW_CONTRACT con
                            inner join NEW_CUSTOMER cus ON cus.ID = con.id_client 
                        where con.id is not null";

            if (!string.IsNullOrEmpty(name))
            {
                query += " and cus.name like '%" + name + "%'";
            }

            if (!string.IsNullOrEmpty(sap))
            {
                query += " and cus.sap like '%" + sap + "%'";
            }

            if (!string.IsNullOrEmpty(cy))
            {
                query += " and cy like '%" + cy + "%'";
            }

            if (!string.IsNullOrEmpty(cultura))
            {
                query += " and cultura like '%" + cultura + "%'";
            }

            if (!string.IsNullOrEmpty(tipo_contrato))
            {
                query += " and type_contract = '" + tipo_contrato + "'";
            }

            query += " ORDER BY cus.SAP, cus.SAP_FILIAL, NAME";

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_main_select.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet reportNEWGLA(string name, string document, string unidade, string regional, string data_recebimento,
            string data_contrato, string status, string caixa)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = @" select cl.document CpfCnpj, cl.name Parceiro, cl.regional Regional, cl.unidade Unidade, 
                              c.dt_digital DataRecebimento, CASE WHEN c.dt_contract is null THEN 'Não Consta' WHEN c.dt_contract = '' THEN 'Não Consta' ELSE c.dt_contract END DataContrato, c.keeper NumeroCaixa, c.numero_acordo as NumeroAcordo, c.tipo_acordo as TipoAcordo, 
                        case when c.status = 'A' then 'Aprovado' when c.status = 'R' then 'Reprovado' else '' end Status, c.obs Motivo
                        from NEW_CONTRACT c 
                        inner join NEW_CUSTOMER cl ON cl.id = c.id_client and cl.del is null
                        where c.id is not null";

            if (!string.IsNullOrEmpty(name))
            {
                query += " and cl.name like '%" + name + "%'";
            }

            if (!string.IsNullOrEmpty(document))
            {
                query += " and cl.document like '%" + document + "%'";
            }

            if (!string.IsNullOrEmpty(unidade))
            {
                query += " and cl.unidade like '%" + unidade + "%'";
            }

            if (!string.IsNullOrEmpty(regional))
            {
                query += " and cl.regional like '%" + regional + "%'";
            }

            if (!string.IsNullOrEmpty(data_recebimento))
            {
                query += " and c.dt_digital like '%" + data_recebimento + "%'";
            }

            if (!string.IsNullOrEmpty(data_contrato))
            {
                query += " and c.dt_contract like '%" + data_contrato + "%'";
            }

            if (!string.IsNullOrEmpty(status))
            {
                query += " and c.status = '" + status + "'";
            }

            if (!string.IsNullOrEmpty(caixa))
            {
                query += " and c.keeper = '" + caixa + "'";
            }

            query += " ORDER BY cl.name, cl.document";

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/reportNEWGLA.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }
    }
}