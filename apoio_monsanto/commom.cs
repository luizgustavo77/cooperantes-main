using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net.Mail;

namespace apoio_monsanto
{
	public class commom
	{
        dbAcess dbAcess = new dbAcess();
        String query = "";
        public DataSet login (String login, String pass)
        {
            DataSet dsRetorno = new DataSet();

            query = " SELECT NAME, EMAIL, LOGIN, TYPE, CHANGEPASS, COOPERANTES, VISUALIZA, DISTRITO, EFETIVIDADE FROM [base_monsanto].[dbo].[REGISTER] WHERE TYPE in(1,2,3,4,5,6,99) AND LOGIN='" + login + "' AND PASS='" + dbAcess.Encrypt(pass) + "' AND ACTIVE=1";

            dbAcess.execSql("query", query);

            if (dbAcess.dsReturn != null && dbAcess.dsReturn.Tables[0].Rows.Count > 0)
            {
                dsRetorno = dbAcess.dsReturn;
            }

            return dsRetorno;
        }

        public String customer_add (int type, String name, String doc, String gr, String rtv, String agric)
        {
            String ret = "";
            DataSet dsRet = new DataSet();
            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[CUSTOMER] WHERE name = '" + name.ToUpper() + "' AND document = '" + doc + "' ";
            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (Convert.ToInt32(dsRet.Tables[0].Rows[0][0]) > 0)
                {
                    ret = "Cliente já cadastrado!";
                    return ret;
                }
            }

            query = " INSERT INTO [base_monsanto].[dbo].[CUSTOMER] (name, document, type, gr, rtv) ";
            query += " VALUES('"+name.ToUpper()+"', '"+doc+"', "+type+",'"+gr+"','"+rtv+"')";

            dbAcess.execSql("insert", query);

            ret = "Novo cliente cadastrado com sucesso!";
            return ret;
        }

        public DataSet retCustomers()
        {
            DataSet dsRet = new DataSet();

            // type 2 = Clientes

            query = " SELECT name, document, gr, rtv, agric  FROM [base_monsanto].[dbo].[CUSTOMER] WHERE DEL = '' ORDER BY ID DESC";
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

            query = " SELECT id, name, document, gr, rtv, agric FROM [base_monsanto].[dbo].[CUSTOMER] WHERE DEL is null ";

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
                query += " AND id = "+id+ "";
            }

            query += " ORDER BY ID DESC ";

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

        public String changeCustomer(Int64 id, String name, String document, String delete, string rtv, string gr)
        {
            String ret = "";
            DataSet dsRet = new DataSet();
            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[CUSTOMER] WHERE id = '" + id + "' ";
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

                query = " UPDATE [base_monsanto].[dbo].[CUSTOMER] SET name = '" + name.ToUpper() +"', document = '"+document+"', del = "+delete+", gr = '" + gr + "', rtv = '" + rtv + "' ";
                query += " WHERE ID = "+id+" ";

                dbAcess.execSql("update", query);

                ret = "Dados Atualizados!";
            }

            return ret;
        }

        public string insertContract (string type, string id_client, ref string id_contract, string useradd)
        {
            string ret = "";
            string query = "";

            query = " SELECT ISNULL(MAX(ID),0)+1 from [base_monsanto].[dbo].[CONTRACT] ";

            dbAcess.execSql("select", query);

            id_contract = dbAcess.dsReturn.Tables[0].Rows[0][0].ToString();

            query = " INSERT INTO [base_monsanto].[dbo].[CONTRACT] (type_contract, id_client, id, useradd, lastupduser) " ;
            query += " VALUES ('{0}','{1}', '{2}', '{3}', '{3}')";

            query = String.Format(query, type, id_client, id_contract, useradd);

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string updateContract(string id, string dt_receb,string safra, string obs, string status, string dt_status, string user_conf,
            string criteria, string id_user_rtv, string id_user_gr, string dt_digital, string dt_archive, string dt_approv, string keeper, 
            string userupd, string tpDoc, string tpTermo, string dtContrato, string vigencia, string rsVolTotal, string rsVolTestadaMais, 
            string rsVolTestadaMenos, string baixaCredito, string fixacao, string rsValorFixado, string rsVolOutrosPartic, string valorTaxas,
            string bonusSemestral, string reajuste, string rsValorAdiantamento, string area_produtor_hectare, string valor_hectare)
        {
            string ret = "";
            string query = "";

            query = @" UPDATE [base_monsanto].[dbo].[CONTRACT] 
                          SET dt_receb =  CONVERT(DATETIME, NULLIF('{0}',''),105), 
                              safra = '{1}', obs = '{2}', status = '{3}', 
                              dt_status = CONVERT(DATETIME,NULLIF('{4}',''),105), user_conf = '{5}', 
                              criteria = '{6}', id_user_rtv = '{7}', id_user_gr = '{8}', 
                                dt_digital = CONVERT(DATETIME, NULLIF('{9}',''),105), dt_archive = CONVERT(DATETIME, NULLIF('{10}',''),105), 
                                dt_approv = CONVERT(DATETIME, NULLIF('{11}',''), 105), keeper = '{12}', lastupduser = '{13}', type_doc_prdsem = '{14}', 
                                term = '{15}', dt_contrato = '{16}', vigencia = '{17}', rs_vol_total = '{18}', rs_vol_testada_mais = '{19}',
                                rs_vol_testada_menos = '{20}', baixa_credito = '{21}', fixacao = '{22}', rs_valor_fixado = '{23}', rs_vol_outros_partic = '{24}',
                                valor_taxas = '{25}', bonus_semestral = '{26}', reajuste = '{27}', rs_valor_adiantamento = '{28}',
                                area_produtor_hectare = '{29}', valor_hectare = '{30}'";
            query += " WHERE id = "+id+"";

            query = String.Format(query, dt_receb, safra, obs, status, dt_status, user_conf, criteria, id_user_rtv, id_user_gr,
                dt_digital, dt_archive, dt_approv, keeper, userupd, tpDoc, tpTermo, dtContrato, vigencia, rsVolTotal, rsVolTestadaMais, rsVolTestadaMenos,
                baixaCredito, fixacao, rsValorFixado, rsVolOutrosPartic, valorTaxas, bonusSemestral, reajuste, rsValorAdiantamento, area_produtor_hectare, valor_hectare);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_insert.txt", query);

            dbAcess.execSql("update", query);

            ret = dbAcess.retQuery;

            return ret;
        }
        public DataSet selectContract (String id)
        {
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();
            String query = "";

            query = " SELECT id, type_contract, id_client, ISNULL(CONVERT(VARCHAR(10),dt_receb,120),'') dt_receb, safra, obs, status, ISNULL(CONVERT(VARCHAR(10),dt_status,120),'') dt_status, user_conf, criteria, id_user_rtv, id_user_gr, ISNULL(CONVERT(VARCHAR(10),dt_digital,120),'') dt_digital, ISNULL(CONVERT(VARCHAR(10),dt_archive,120),'') dt_archive, ISNULL(CONVERT(VARCHAR(10),dt_approv,120),'') dt_approv, keeper, type_doc_prdsem, term, * ";
            query += " FROM [base_monsanto].[dbo].[CONTRACT] ";
            query += " WHERE id = " + id + "";

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
            query += " FROM [base_monsanto].[dbo].[CONTRACT] ";
            query += " WHERE id = " + id + "";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectContractByClient(String id_client, String safra, String gla)
        {
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();
            String query = "";

            query = " SELECT id NrContrato, CASE WHEN type_contract = 1 then 'Distribuidor' ";
            query += "  WHEN type_contract = 2 then 'POD' ";
            query += "  WHEN type_contract = 3 then 'Produtor de Sementes'";
            query += "  WHEN type_contract = 4 then 'Monsoy' ";
            query += "  WHEN type_contract = 5 then 'Comissões' ";
            query += "  WHEN type_contract = 6 then 'Groud Breakers' ";
            query += "  WHEN type_contract = 7 then 'Distribuidor Algodão' ";
            query += "  WHEN type_contract = 8 then 'Multiplicador Algodão' ";
            query += "  ELSE '' ";
            query += "  END Tipo, ";
            query += "  CASE WHEN status = '0' THEN 'EM ANÁLISE' ";
            query += "  WHEN status = 1 THEN 'CONCLUÍDO' ";
            query += "  WHEN status is null THEN 'EM ANÁLISE' ELSE 'REPROVADO' END Status, Safra, type_contract, id_client, CASE WHEN id_doc is null THEN 'Aguardando Documento' ELSE id_doc END id_doc ";
            query += " FROM [base_monsanto].[dbo].[CONTRACT] ";
            query += " WHERE id_client = '" + id_client + "'";
            if (safra != null)
                query += " AND LTRIM(RTRIM(safra)) = '" + safra + "'";

            dbAcess.execSql("select", query);

            //string path = HttpContext.Current.Server.MapPath("data");
            //memowrit(path + "/arquivo_log.txt", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public String insertDocument (String id_contract, String id_client, String docname, String path)
        {
            String query = "";
            String ret = "";
            query = " INSERT INTO [base_monsanto].[dbo].[DOCUMENT] (id_contract, id_client, docname, path) ";
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

            query = " SELECT * FROM [base_monsanto].[dbo].[DOCUMENT] WHERE id_contract = '"+ id_contract + "' ";

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
            query = " DELETE FROM [base_monsanto].[dbo].[DOCUMENT] ";
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
            query = " DELETE FROM [base_monsanto].[dbo].[DOCUMENT] ";
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
            query += " SELECT name FROM [base_monsanto].[dbo].[REGISTER] WHERE del is null AND ACTIVE = 1 AND TYPE = 3 ";

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
            query += " SELECT name FROM [base_monsanto].[dbo].[REGISTER] WHERE del is null AND ACTIVE = 1 AND TYPE = 3 ";

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
            query += " SELECT name FROM [base_monsanto].[dbo].[REGISTER] WHERE del is null AND ACTIVE = 1 AND TYPE = 4 ";

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
            query += " SELECT name FROM [base_monsanto].[dbo].[REGISTER] WHERE del is null AND ACTIVE = 1 AND TYPE = 4 ";

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
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path))
            {
                sw.Write(data);
            }
        }

        public void valDocCont(string id_contract, string docname)
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT id_doc FROM [base_monsanto].[dbo].[CONTRACT] WHERE id = "+id_contract+" ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (String.IsNullOrEmpty(dsRet.Tables[0].Rows[0][0].ToString()))
                {
                    query = " UPDATE [base_monsanto].[dbo].[CONTRACT] SET id_doc = '"+docname+"' WHERE id = "+id_contract+"";

                    dbAcess.execSql("update", query);
                }
            }
        }

        public bool changePass(string username, string newPass, string conPass)
        {
            string query = "";

            try
            {
                query = " UPDATE [base_monsanto].[dbo].[REGISTER] set pass = '" + dbAcess.Encrypt(conPass) + "', changepass = 'S' ";
                query += " WHERE login = '"+username+"' ";

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
            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[REGISTER] WHERE login = '" + login + "' AND (del is null or del = '') ";
            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (Convert.ToInt64(dsRet.Tables[0].Rows[0][0]) > 0)
                {
                    ret = "Usuário já cadastrado!";
                    return ret;
                }
            }

            query = " INSERT INTO [base_monsanto].[dbo].[REGISTER] (name, email, login, type, active, distrito, regional, contato1, contato2, endereco, usergla) ";
            query += " VALUES('" + name.ToUpper() + "', '" + email + "', '" + login + "', '"+type+"', '"+active+"', '"+distrito+"','"+regional+"','"+cont1+"','"+cont2+"','"+endereco+"','"+ usergla+"')";

            dbAcess.execSql("insert", query);

            ret = "Novo usuário cadastrado com sucesso!";
            return ret;
        }

        public DataSet selectUsers(string name, string email, string login, string type,
            string distrito, string regional, string cont1, string cont2, string endereco)
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT login, name, email, case when active = 1 then 'ATIVO' else 'INATIVO' end status, regional FROM [base_monsanto].[dbo].[REGISTER] ";
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

            query = " SELECT * FROM [base_monsanto].[dbo].[REGISTER] ";
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

            query = " SELECT login FROM [base_monsanto].[dbo].[REGISTER] WHERE login = '" + login + "' ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (!String.IsNullOrEmpty(dsRet.Tables[0].Rows[0][0].ToString()))
                {
                    query = " UPDATE [base_monsanto].[dbo].[REGISTER] SET del = '*' WHERE login = '" + login + "'";

                    dbAcess.execSql("update", query);
                }
            }
        }

        public void deleteContract(string id_contract)
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT id FROM [base_monsanto].[dbo].[CONTRACT] WHERE id = '" + id_contract + "' ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (!String.IsNullOrEmpty(dsRet.Tables[0].Rows[0][0].ToString()))
                {
                    query = " DELETE FROM [base_monsanto].[dbo].[CONTRACT] WHERE id = '" + id_contract + "'";

                    dbAcess.execSql("delete", query);
                }
            }
        }

        public String updateUser(string name, string email, string login, string type, string active,
            string distrito, string regional, string cont1, string cont2, string endereco)
        {
            String ret = "";
            DataSet dsRet = new DataSet();
            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[REGISTER] WHERE login = '" + login + "' ";
            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (Convert.ToInt64(dsRet.Tables[0].Rows[0][0]) == 0)
                {
                    ret = "Usuário não encontrado!";
                    return ret;
                }
            }

            query = " UPDATE [base_monsanto].[dbo].[REGISTER] set name = '" + name.ToUpper() + "', ";
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

        /*public string sentEmail (string mailTo, string mailMsg, string mailSubject)
        {
            try
            {
                //Define os dados do e-mail
                string nomeRemetente = "Apoio Monsanto";
                string emailRemetente = "devops@rscomputer.com.br";
                string remetente = "sistema@rscomputer.com.br";
                string senha = "Y3x.bmw91411";

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
                objEmail.From = new System.Net.Mail.MailAddress(nomeRemetente + "<" + remetente + ">");

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
        }*/

        public string sentEmail(string mailTo, string mailMsg, string mailSubject)
        {
            // that information belong to the properties file
            // if you change that shit, some shit will happen
            string mailAuth = "flcservicos2017@gmail.com";
            string mailAuthPass = "foqazlkdiaprdpyv";

            // any message will be reported as return data
            string returnMessage = "";

            try
            {
                // all intel is in the trycatch statement
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(mailAuth, "Apoio FLC");
                mail.To.Add(mailTo);

                mail.Subject = mailSubject;
                mail.Body = mailMsg;

                // using attachement
                /*if (!String.IsNullOrEmpty(attachmentFile))
                {
                    System.Net.Mail.Attachment attachment;
                    attachment = new System.Net.Mail.Attachment(attachmentFile);
                    mail.Attachments.Add(attachment);
                }*/

                // smtp server port (if retrieve any error, try change de port)
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailAuth, mailAuthPass);
                SmtpServer.EnableSsl = true;

                // email sending...
                SmtpServer.Send(mail);

                // return message (obvious, dah)
                returnMessage = "sent";
            }
            catch (Exception ex)
            {
                // return message (obvious, dah)
                returnMessage = "E-mail error: " + ex.Message;
            }

            // message to catch errors 
            return returnMessage;
        }

        public string retUserMail (String name)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT distinct email FROM [base_monsanto].[dbo].[REGISTER] ";
            query += " WHERE del is null AND name = '" + name + "' ";

            dbAcess.stringSql(query);

            return dbAcess.retQuery;
        }

        public string retUserMailbyLogin(String login)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT distinct email FROM [base_monsanto].[dbo].[REGISTER] ";
            query += " WHERE del is null AND login = '" + login + "' ";

            dbAcess.stringSql(query);

            return dbAcess.retQuery;
        }

        public string retNameCont(String id_contract)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT id_doc FROM [base_monsanto].[dbo].[CONTRACT] ";
            query += " WHERE id = " + id_contract + " ";

            dbAcess.stringSql(query);

            return dbAcess.retQuery;
        }

        public string insertLog(string oldValue, string newValue, string id_contract, string field, string userName, string tableName)
        {
            string query = "";
            string ret = "";

            query = " INSERT INTO [base_monsanto].[dbo].[AUDIT]( type, TableName, FieldName, OldValue, NewValue, UpdateDate, UserName, PK )";
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
                        FROM [base_monsanto].[dbo].[AUDIT] A
                        INNER JOIN [base_monsanto].[dbo].[CONTRACT] D
                        ON D.ID = REPLACE(REPLACE(A.PK, '<ID=', ''), '>', '')
                        INNER JOIN [base_monsanto].[dbo].[CUSTOMER] C
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
                query += " AND USERNAME LIKE '%"+user+"%'";
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

            query = " SELECT * FROM [base_monsanto].[dbo].[PERMISSION] ORDER BY dt_register DESC";

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

            query = " INSERT INTO [base_monsanto].[dbo].[PERMISSION] (id_user, dt_perm_ini, dt_perm_end, profile, dt_register)";
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
                query += " FROM [base_monsanto].[dbo].[PERMISSION] WHERE id_user = '"+id_user+"' ";

                dbAcess.stringSql(query);

                if (dbAcess.retQuery == "ALLOWED" || dbAcess.retQuery == "")
                    return "OK";
            }

            if (operation == "DELETE" || operation == "VALIDATE")
                type = 2;

            if (operation == "INSERT")
                type = 1;

            query = " UPDATE [base_monsanto].[dbo].[REGISTER] set TYPE = "+type+" where login = '" + id_user + "' ";

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

            query = " SELECT name, login FROM [base_monsanto].[dbo].[REGISTER] WHERE del is null AND type = 2 ORDER BY 1 DESC";

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
            query = " DELETE FROM [base_monsanto].[dbo].[PERMISSION] ";
            query += " WHERE id_user ='{0}' ";

            query = String.Format(query, id_user);

            dbAcess.execSql("delete", query);

            updatePermission(id_user, "DELETE");

            ret = dbAcess.retQuery;

            return ret;
        }

        public String retPassDecrypt(string login)
        {
            query = " SELECT PASS FROM [base_monsanto].[dbo].[REGISTER] WHERE del is null AND login = '"+login+"' ";

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

            query = " SELECT * FROM [base_monsanto].[dbo].[CRITERIA] ";
            if (!String.IsNullOrEmpty(type))
            {
                query += " WHERE type = '"+type+"' ";
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

            query = "INSERT INTO [base_monsanto].[dbo].[CRITERIA] (criteria, type) VALUES ('"+criteria+"','"+type+"') ";

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string deleteCriteria(string id_criteria)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = "DELETE FROM [base_monsanto].[dbo].[CRITERIA] WHERE id = " + id_criteria + " ";

            dbAcess.execSql("delete", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public DataSet selectAllRegion()
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT * FROM [base_monsanto].[dbo].[REGIONS] ";
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

            query = "INSERT INTO [base_monsanto].[dbo].[REGIONS] (region) VALUES ('" + region + "') ";

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string deleteRegion(string id_region)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[REGISTER] where regional = (select distinct region from [base_monsanto].[dbo].[REGIONS] where id = '" + id_region + "') ";

            dbAcess.stringSql(query);

            if (dbAcess.retQuery != "0")
                ret = "Exclusão não permitida!";
            else
            {
                query = "DELETE FROM [base_monsanto].[dbo].[REGIONS] WHERE id = " + id_region + " ";

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

            query = " SELECT * FROM [base_monsanto].[dbo].[SAFRA] ";
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

            query = "INSERT INTO [base_monsanto].[dbo].[SAFRA] (safra) VALUES ('" + safra + "') ";

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string deleteSafra(string id_safra)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[CONTRACT] where safra = (select distinct safra from [base_monsanto].[dbo].[SAFRA] where id = '" + id_safra + "') ";

            dbAcess.stringSql(query);

            if (dbAcess.retQuery != "0")
                ret = "Exclusão não permitida!";
            else
            {
                query = "DELETE FROM [base_monsanto].[dbo].[SAFRA] WHERE ID = " + id_safra + " ";

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
            query += " FROM [base_monsanto].[dbo].[CONTRACT] c ";
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
            query += " FROM [base_monsanto].[dbo].[CONTRACT] c";
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
            query += " FROM [base_monsanto].[dbo].[CONTRACT] c";
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






            query = @" SELECT distinct client.name as 'Nome do Cliente', 
              client.document as 'Documento do Cliente', 
               safra as 'Safra', c.id as 'ID', 
               case when type_contract = 1 then 'Distribuidor' 
             when type_contract = 2 then 'POD' 
             when type_contract = 3 then 'Produtor de Sementes' 
             when type_contract = 4 then 'Monsoy' 
             when type_contract = 5 then 'Comissões' 
             when type_contract = 6 then 'Groud Breakers' 
             when type_contract = 7 then 'Distribuidor Algodão' 
             WHEN type_contract = 8 then 'Multiplicador Algodão' 
             else 'Outro Tipo' end as 'TipoContrato', 
                substring(CAST(dt_receb as varchar),9,2) + '/' +  substring(CAST(dt_receb as varchar),6,2) + '/' + substring(CAST(dt_receb as varchar),1,4) as 'Data de Recebimento', 
                case when status = 1 then 'Aprovado' else 'Reprovado' end as 'Status', 
               id_user_rtv as 'RTV', 
                id_user_gr as 'GR', 
               substring(CAST(dt_digital as varchar),9,2) + '/' +  substring(CAST(dt_digital as varchar),6,2) + '/' + substring(CAST(dt_digital as varchar),1,4) as 'Data de Digitalização', 
                substring(CAST(dt_approv as varchar),9,2) + '/' +  substring(CAST(dt_approv as varchar),6,2) + '/' + substring(CAST(dt_approv as varchar),1,4) as 'Data de Aprovação', 
               substring(CAST(dt_archive as varchar),9,2) + '/' +  substring(CAST(dt_archive as varchar),6,2) + '/' + substring(CAST(dt_archive as varchar),1,4) as 'Data Keepers', 
               keeper as 'N. Caixa Keepers', case when term = 'at' then 'Aditivo ao Termo' when term = 'ta' then 'Termo de Aditivo'  when term = 'tx' then 'Termo Aditivo - Taxa de Serviço' else '' end as 'Tipo de Termo',
case when c.type_doc_prdsem = '1' then 'Contrato de Licenciamento de Tecnologia Intacta RR2 PRO' 
               when c.type_doc_prdsem = '2' then 'Contrato de Prestação de Serviços e outras avenças' 
               when c.type_doc_prdsem = '3' then 'Termo de Licenciamento' 
               when c.type_doc_prdsem = '4' then 'Procuração' 
               when c.type_doc_prdsem = '5' then 'Contrato de Licenciamento de Tecnologia Bollgard II e RRFlex' 
               else '' end TipoProdutorSementes,               
c.dt_contrato as 'Data Contrato',
               c.vigencia as 'Vigencia',
               c.rs_vol_total as 'R$/Vol Total',
               c.rs_vol_testada_mais as 'R$/Vol Testada +',
               c.rs_vol_testada_menos as 'R$/Vol Testada -',
               c.baixa_credito as 'Baixa de Crédito',
               c.fixacao as 'Fixacao',
               c.rs_valor_fixado as 'R$ Valor Fixado',
               c.rs_vol_outros_partic as 'R$ Vol Outros Partic',
               c.valor_taxas as 'Valor das Taxas',
               c.bonus_semestral as 'Bônus Semestral',
               c.reajuste as 'Reajuste',
               c.rs_valor_adiantamento as 'R$ Valor Adiantamento'
             FROM [base_monsanto].[dbo].[CONTRACT]c 
             LEFT JOIN base_monsanto.dbo.register AS gr on gr.name = id_user_gr AND gr.type = 4 
             LEFT JOIN base_monsanto.dbo.register AS rtv on rtv.name = id_user_rtv AND rtv.type = 3
             LEFT JOIN base_monsanto.dbo.customer AS client on client.ID = id_client
              WHERE (dt_receb IS NOT NULL AND dt_receb <> '') 
             AND status in (1,2) ";

            if (!String.IsNullOrEmpty(safra) && safra != "Todos" && safra != "")
                query += " AND safra = '" + safra + "'";

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

            if ( ! String.IsNullOrEmpty(tpTermo))
                query += " AND term = '" + tpTermo + "' ";

            query += "  ORDER BY 1";

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arqui_select_detail_all.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet retDistByRTV (String rtv)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            rtv = String.IsNullOrEmpty(rtv) ? rtv : rtv.ToUpper();

            query = " SELECT distinct distrito FROM [base_monsanto].[dbo].[REGISTER] WHERE name = '"+rtv+"' and type = 3 and (del = '' or del is null) ";

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

            query = " SELECT distinct regional FROM [base_monsanto].[dbo].[REGISTER] WHERE name = '" + gr + "' and type = 4 ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public Boolean valueValidation (String value, int type)
        {
            bool ret = false;
            string query = "";

            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[REGISTER] WHERE name = '" + value + "' and type = "+type+" AND (DEL IS NULL OR DEL = '') ";

            dbAcess.stringSql(query);

            if (dbAcess.retQuery != "0")
                ret = true;

            return ret;
        }
    }
}