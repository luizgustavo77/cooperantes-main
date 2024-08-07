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
    public class coopcom
    {
        dbAcess dbAcess = new dbAcess();
        String query = "";
        public DataSet login(String login, String pass)
        {
            DataSet dsRetorno = new DataSet();

            query = " SELECT NAME, EMAIL, LOGIN, TYPE, CHANGEPASS FROM [base_monsanto].[dbo].[REGISTER] WHERE TYPE in(1,2,3,4,5,6,99) AND LOGIN='" + login + "' AND PASS='" + dbAcess.Encrypt(pass) + "' AND ACTIVE=1";

            dbAcess.execSql("query", query);

            if (dbAcess.dsReturn != null && dbAcess.dsReturn.Tables[0].Rows.Count > 0)
            {
                dsRetorno = dbAcess.dsReturn;
            }

            return dsRetorno;
        }

        public String customer_add(int type, String name, String doc, String gr, String rtv, String agric, string rua, string numero, string complemento, string bairro, string cidade, string estado,
            string telefone, string email)
        {
            String ret = "";
            DataSet dsRet = new DataSet();
            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[COOP_CUSTOMER] WHERE name = '" + name.ToUpper() + "' AND document = '" + doc + "' ";
            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (Convert.ToInt16(dsRet.Tables[0].Rows[0][0]) > 0)
                {
                    ret = "Cooperante já cadastrado!";
                    return ret;
                }
            }

            query = " INSERT INTO [base_monsanto].[dbo].[COOP_CUSTOMER] (name, document, type, gr, rtv, rua, numero, complemento, bairro, cidade, estado, telefone, email) ";
            query += " VALUES('" + name.ToUpper() + "', '" + doc + "', " + type + ",'" + gr + "','" + rtv + "','" + rua + "','" + numero + "', '" + complemento + "','" + bairro + "','" + cidade + "','" + estado + "','" + telefone + "','" + email + "')";

            dbAcess.execSql("insert", query);

            ret = "Novo cooperante cadastrado com sucesso!";
            return ret;
        }

        public DataSet retCustomers()
        {
            DataSet dsRet = new DataSet();

            // type 2 = Clientes

            query = " SELECT * FROM [base_monsanto].[dbo].[COOP_CUSTOMER] WHERE DEL = '' ORDER BY ID DESC";
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

            query = " SELECT * FROM [base_monsanto].[dbo].[COOP_CUSTOMER] WHERE DEL is null ";

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

        public DataSet searcCustomers(String name, String document, String id, String gla, String ContratoMae, String ContratoBase)
        {
            DataSet dsRet = new DataSet();

            // type 2 = Clientes

            query = " SELECT * FROM [base_monsanto].[dbo].[COOP_CUSTOMER] CUS WHERE DEL is null ";

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

            if (!String.IsNullOrEmpty(ContratoMae))
            {
                query += " AND EXISTS (SELECT * FROM COOP_PROCESS PRO WHERE NUM_CONT_MAE = '"+ ContratoMae + "' AND PRO.id_coop = CUS.ID) ";
            }

            if (!String.IsNullOrEmpty(ContratoBase))
            {
                query += " AND EXISTS (SELECT * FROM COOP_PROCESS PRO WHERE NUM_CONT_BASE = '" + ContratoBase + "' AND PRO.id_coop = CUS.ID) ";
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

        public String changeCustomer(Int64 id, String name, String document, String delete, string rtv, string gr, string email, string tel, string cep, string num)
        {
            String ret = "";
            DataSet dsRet = new DataSet();
            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[COOP_CUSTOMER] WHERE id = '" + id + "' ";
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

                query = " UPDATE [base_monsanto].[dbo].[COOP_CUSTOMER] SET name = '" + name.ToUpper() + "', document = '" + document + "', del = " + delete + ", gr = '" + gr + "', rtv = '" + rtv + "', email = '" + email + "', telefone = '" + tel + "', cep = '" + cep + "', numero = '" + num + "' ";
                query += " WHERE ID = " + id + " ";

                dbAcess.execSql("update", query);

                ret = "Dados Atualizados!";
            }

            return ret;
        }

        public string insertContract(string type, string id_client, ref string id_contract, string unidade)
        {
            string ret = "";
            string query = "";

            query = " SELECT ISNULL(MAX(ID),0)+1 from [base_monsanto].[dbo].[COOP_PROCESS] ";

            dbAcess.execSql("select", query);

            id_contract = dbAcess.dsReturn.Tables[0].Rows[0][0].ToString();

            query = " INSERT INTO [base_monsanto].[dbo].[COOP_PROCESS] (type, id_coop, dtemis, etapa, unidade) ";
            query += " VALUES ('{0}','{1}', GETDATE(), 1, '{2}')";

            query = String.Format(query, type, id_client, unidade);

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string updateContract(string id, string dtcont, string tpsafra, string safra, string unidade,
            string obs, string documentos, string tecnico, string gerente, string numcontbase, string numcontmae, string fazenda, string endfazenda,
            string area, string incra, string desagio, string teto, string dtinipla, string dtfimpla, string garantias,
            string garantiar, string matricula, string supervisor, string tpcontrato, string data_folha_rosto, string urgente)
        {
            string ret = "";
            string query = "";

            query = @" UPDATE [base_monsanto].[dbo].[COOP_PROCESS] SET dtemis = CONVERT(DATETIME, NULLIF('" + dtcont + "',''), 105), " +
                     "   tpsafra = '" + tpsafra + "', safra = '" + safra + "', unidade = '" + unidade + "', obs = '" + obs + "', documentos = '" + documentos + "', tecnico = '" + tecnico + "', " +
                     "   gerente = '" + gerente + "', num_cont_base = '" + numcontbase + "', num_cont_mae = '" + numcontmae + "', fazenda = '" + fazenda + "', " +
                     "   end_fazenda = '" + endfazenda + "', area = '" + area + "', inscricao = '" + incra + "', desagio = '" + desagio + "', teto = '" + teto + "' , " +
                     "   dt_ini_pla = CONVERT(DATETIME, NULLIF('" + dtinipla + "',''), 105), dt_fim_pla = CONVERT(DATETIME, NULLIF('" + dtfimpla + "',''), 105), " +
                     "   garantias = '" + garantias + "', garantiar = '" + garantiar + "', matricula = '" + matricula + "', supervisor = '" + supervisor + "', type = '" + tpcontrato + "', data_folha_rosto = CONVERT(DATETIME, NULLIF('" + data_folha_rosto + "',''), 105), urgente = '" + urgente + "' ";
            query += " WHERE id = " + id + "";

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log.txt", query);

            dbAcess.execSql("update", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string updateFluxContract(string id_contract, string id_flux)
        {
            string ret = "";
            string query = "";

            query = @" UPDATE [base_monsanto].[dbo].[COOP_PROCESS] SET etapa = '" + id_flux + "' WHERE ID = " + id_contract;

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_update_etapa.txt", query);

            dbAcess.execSql("update", query);

            ret = dbAcess.retQuery;

            return ret;
        }
        public DataSet selectContract(String id, string unidade)
        {
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();
            String query = "";

            query = " SELECT * "; // pra trazer todos os campos a medida que forem adicionados novos
            query += " FROM [base_monsanto].[dbo].[COOP_PROCESS] ";
            query += " WHERE id = " + id + "";

            if (!string.IsNullOrEmpty(unidade))
                query += " AND unidade = '" + unidade + "' ";

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
            query += " FROM [base_monsanto].[dbo].[COOP_CONTRACT] ";
            query += " WHERE id = " + id + "";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectContractByClient(String id_coop, String safra, string tpsafra, string etapa, string unidade)
        {
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();
            String query = "";
            string filter = "";

            query = @" SELECT * 
                       FROM [base_monsanto].[dbo].[COOP_PROCESS] 
                       WHERE id_coop = '" + id_coop + "'";

            if (!String.IsNullOrEmpty(safra) && safra != "- Ano - ")
                query += " AND LTRIM(RTRIM(safra)) = '" + safra + "'";

            if (!String.IsNullOrEmpty(tpsafra))
                query += " AND LTRIM(RTRIM(tpsafra)) = '" + tpsafra + "'";

            if (!String.IsNullOrEmpty(etapa) && etapa != "0")
                query += " AND LTRIM(RTRIM(etapa)) = '" + etapa + "'";

            if (!String.IsNullOrEmpty(unidade))
            {
                string[] units = unidade.Split(';');
                foreach (string unit in units)
                {
                    if (!string.IsNullOrEmpty(unit))
                        filter += "'" + unit + "',";
                }

                filter = filter.Substring(0, (filter.Trim().Length - 1));

                query += " AND LTRIM(RTRIM(unidade)) IN (" + filter + ")";
            }


            query += " ORDER BY 1 ";
            dbAcess.execSql("select", query);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_selectContractByClient.txt", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectContractByClient(String id_coop, String safra, String tpsafra, String etapa, String unidade, String ContratoMae, String ContratoBase)
        {
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();
            String query = "";
            string filter = "";

            query = @" SELECT * 
                       FROM [base_monsanto].[dbo].[COOP_PROCESS] 
                       WHERE id_coop = '" + id_coop + "'";

            if (!String.IsNullOrEmpty(safra) && safra != "- Ano - ")
                query += " AND LTRIM(RTRIM(safra)) = '" + safra + "'";

            if (!String.IsNullOrEmpty(tpsafra))
                query += " AND LTRIM(RTRIM(tpsafra)) = '" + tpsafra + "'";

            if (!String.IsNullOrEmpty(etapa) && etapa != "0")
                query += " AND LTRIM(RTRIM(etapa)) = '" + etapa + "'";

            if (!String.IsNullOrEmpty(ContratoMae) && ContratoMae != "0")
                query += " AND LTRIM(RTRIM(num_cont_mae)) = '" + ContratoMae + "'";

            if (!String.IsNullOrEmpty(ContratoBase) && ContratoBase != "0")
                query += " AND LTRIM(RTRIM(num_cont_base)) = '" + ContratoBase + "'";

            if (!String.IsNullOrEmpty(unidade))
            {
                string[] units = unidade.Split(';');
                foreach (string unit in units)
                {
                    if (!string.IsNullOrEmpty(unit))
                        filter += "'" + unit + "',";
                }

                filter = filter.Substring(0, (filter.Trim().Length - 1));

                query += " AND LTRIM(RTRIM(unidade)) IN (" + filter + ")";
            }


            query += " ORDER BY 1 ";
            dbAcess.execSql("select", query);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_selectContractByClient.txt", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public bool vldContractCanceled(string id_contract)
        {
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();
            String query = "";
            string filter = "";

            query = @" SELECT COUNT(*) TOT 
                       FROM [base_monsanto].[dbo].[COOP_FLUX_DATA] 
                       WHERE id_contract = '" + id_contract + "' AND stat = 4";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (int.Parse(dsRet.Tables[0].Rows[0]["TOT"].ToString()) > 0)
                    return true;
            }

            return false;
        }

        public String insertStartFlux(String id_contract)
        {
            String query = "";
            String ret = "";

            query = " INSERT INTO [base_monsanto].[dbo].[COOP_FLUX_DATA] (id_contract, id_flux, stat) ";
            query += " VALUES ('{0}', 1, 1)";

            query = String.Format(query, id_contract);

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public void fixStartflux()
        {
            String query = "";
            dbAcess.dsReturn.Clear();

            query = "SELECT ID FROM COOP_PROCESS P WHERE NOT EXISTS (SELECT * FROM COOP_FLUX_DATA D WHERE D.ID_CONTRACT = P.ID)";

            dbAcess.execSql("select", query);

            if (dbAcess.dsReturn != null)
            {
                for (int i = 0; i < dbAcess.dsReturn.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        insertStartFlux(dbAcess.dsReturn.Tables[0].Rows[i][0].ToString());
                    }
                    catch (Exception ex)
                    {

                        throw new Exception(ex.Message);
                    }

                }
            }

        }

        public String insertDocument(String id_contract, String id_client, String docname, String path, string date, string obs)
        {
            String query = "";
            String ret = "";

            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.ToString("dd/MM/yyyy");
            }

            query = " INSERT INTO [base_monsanto].[dbo].[COOP_DOCUMENT] (id_contract, id_client, docname, path, date, obs) ";
            query += " VALUES ('{0}','{1}', '{2}', '{3}',  CAST(NULLIF('{4}','') as datetime), '{5}')";

            query = String.Format(query, id_contract, id_client, docname, path, date, obs);

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public DataSet selectDocument(String id_contract)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();

            query = " SELECT * FROM [base_monsanto].[dbo].[COOP_DOCUMENT] WHERE id_contract = '" + id_contract + "' ";

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
            query = " DELETE FROM [base_monsanto].[dbo].[COOP_DOCUMENT] ";
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
            query = " DELETE FROM [base_monsanto].[dbo].[COOP_DOCUMENT] ";
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
            query += " SELECT name FROM [base_monsanto].[dbo].[REGISTER] WHERE del is null AND ACTIVE = 1 AND TYPE = 3 AND COOPERANTES = 'S' ";

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
            query += " SELECT name FROM [base_monsanto].[dbo].[REGISTER] WHERE del is null AND ACTIVE = 1 AND TYPE = 4 AND COOPERANTES = 'S' ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectSR()
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT '' name union all ";
            query += " SELECT name FROM [base_monsanto].[dbo].[REGISTER] WHERE del is null AND ACTIVE = 1 AND TYPE = 5 AND COOPERANTES = 'S' ";

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

            query = " SELECT id_doc FROM [base_monsanto].[dbo].[COOP_CONTRACT] WHERE id = " + id_contract + " ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (String.IsNullOrEmpty(dsRet.Tables[0].Rows[0][0].ToString()))
                {
                    query = " UPDATE [base_monsanto].[dbo].[COOP_CONTRACT] SET id_doc = '" + docname + "' WHERE id = " + id_contract + "";

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
            string distrito, string regional, string cont1, string cont2, string endereco, string usergla, string visualiza)
        {
            String ret = "";
            DataSet dsRet = new DataSet();
            /*query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[REGISTER] WHERE login = '" + login + "' AND (del is null or del = '') AND COOPERANTES = 'S' ";
            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (Convert.ToInt16(dsRet.Tables[0].Rows[0][0]) > 0)
                {
                    ret = "Usuário já cadastrado!";
                    return ret;
                }
            }*/

            query = " INSERT INTO [base_monsanto].[dbo].[REGISTER] (name, email, login, type, active, distrito, regional, contato1, contato2, endereco, usergla, cooperantes, visualiza) ";
            query += " VALUES('" + name.ToUpper() + "', '" + email + "', '" + login + "', '" + type + "', '" + active + "', '" + distrito + "','" + regional + "','" + cont1 + "','" + cont2 + "','" + endereco + "','" + usergla + "','S', '" + visualiza + "')";

            dbAcess.execSql("insert", query);

            ret = "Novo usuário cadastrado com sucesso!";
            return ret;
        }

        public DataSet selectUsers(string name, string email, string login, string type,
            string distrito, string cont1)
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT login, name, email, case when active = 1 then 'ATIVO' else 'INATIVO' end status, distrito, case when visualiza = 'S' then 'Sim' else 'Não' end visualiza FROM [base_monsanto].[dbo].[REGISTER] ";
            query += " WHERE del is null AND TYPE = " + type + " AND COOPERANTES = 'S' ";
            if (!string.IsNullOrEmpty(login))
                query += " AND login like '%" + login + "%'";
            if (!string.IsNullOrEmpty(name))
                query += " AND name like '%" + name + "%'";
            if (!string.IsNullOrEmpty(email))
                query += " AND email like '%" + email + "%'";
            if (!string.IsNullOrEmpty(distrito) && !distrito.Contains("Selecione"))
                //query += " AND distrito like '%" + distrito + "%'";
                if (!string.IsNullOrEmpty(cont1))
                    query += " AND contato1 like '%" + cont1 + "%'";

            query += " ORDER BY name ";

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_users.txt", query);

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
            query += " WHERE del is null AND TYPE = " + type + " AND COOPERANTES = 'S' ";
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

            query = " SELECT login FROM [base_monsanto].[dbo].[REGISTER] WHERE login = '" + login + "' AND COOPERANTES = 'S' ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (!String.IsNullOrEmpty(dsRet.Tables[0].Rows[0][0].ToString()))
                {
                    query = " UPDATE [base_monsanto].[dbo].[REGISTER] SET del = '*' WHERE login = '" + login + "' AND COOPERANTES = 'S'";

                    dbAcess.execSql("update", query);
                }
            }
        }

        public void deleteContract(string id_contract)
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " SELECT id FROM [base_monsanto].[dbo].[COOP_PROCESS] WHERE id = '" + id_contract + "' ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                if (!String.IsNullOrEmpty(dsRet.Tables[0].Rows[0][0].ToString()))
                {
                    query = " DELETE FROM [base_monsanto].[dbo].[COOP_PROCESS] WHERE id = '" + id_contract + "'";

                    dbAcess.execSql("delete", query);
                }
            }
        }

        public String updateUser(string name, string email, string login, string type, string active,
            string distrito, string regional, string cont1, string cont2, string endereco, string visualiza)
        {
            String ret = "";
            DataSet dsRet = new DataSet();
            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[REGISTER] WHERE login = '" + login + "' AND COOPERANTES = 'S' ";
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

            query = " UPDATE [base_monsanto].[dbo].[REGISTER] set name = '" + name.ToUpper() + "', ";
            query += " email = '" + email + "', ";
            query += " type =  '" + type + "', ";
            query += "active = '" + active + "', ";
            query += "distrito = '" + distrito + "',";
            query += "regional = '" + regional + "',";
            query += "contato1 = '" + cont1 + "',";
            query += "contato2 = '" + cont2 + "',";
            query += "visualiza = '" + visualiza + "',";
            query += "endereco = '" + endereco + "'";
            query += "WHERE login = '" + login + "' AND COOPERANTES = 'S' ";

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

            query = " SELECT distinct email FROM [base_monsanto].[dbo].[REGISTER] ";
            query += " WHERE del is null AND name = '" + name + "' AND COOPERANTES = 'S' ";

            dbAcess.stringSql(query);

            return dbAcess.retQuery;
        }

        public string retUserMailbyLogin(String login)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT distinct email FROM [base_monsanto].[dbo].[REGISTER] ";
            query += " WHERE del is null AND login = '" + login + "' AND COOPERANTES = 'S' ";

            dbAcess.stringSql(query);

            return dbAcess.retQuery;
        }

        public string retNameCont(String id_contract)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT id_doc FROM [base_monsanto].[dbo].[COOP_CONTRACT] ";
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
                        INNER JOIN [base_monsanto].[dbo].[COOP_CONTRACT] D
                        ON D.ID = REPLACE(REPLACE(A.PK, '<ID=', ''), '>', '')
                        INNER JOIN [base_monsanto].[dbo].[COOP_CUSTOMER] C
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

            query = " SELECT * FROM [base_monsanto].[dbo].[PERMISSION] ORDER BY dt_REGISTER DESC";

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

            query = " INSERT INTO [base_monsanto].[dbo].[PERMISSION] (id_user, dt_perm_ini, dt_perm_end, profile, dt_REGISTER)";
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
                query += " FROM [base_monsanto].[dbo].[PERMISSION] WHERE id_user = '" + id_user + "' ";

                dbAcess.stringSql(query);

                if (dbAcess.retQuery == "ALLOWED" || dbAcess.retQuery == "")
                    return "OK";
            }

            if (operation == "DELETE" || operation == "VALIDATE")
                type = 2;

            if (operation == "INSERT")
                type = 1;

            query = " UPDATE [base_monsanto].[dbo].[REGISTER] set TYPE = " + type + " where login = '" + id_user + "' AND COOPERANTES = 'S' ";

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

            query = " SELECT name, login FROM [base_monsanto].[dbo].[REGISTER] WHERE del is null AND type = 2 AND COOPERANTES = 'S' ORDER BY 1 DESC";

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
            query = " SELECT PASS FROM [base_monsanto].[dbo].[REGISTER] WHERE del is null AND login = '" + login + "' AND COOPERANTES = 'S' ";

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

            query = "INSERT INTO [base_monsanto].[dbo].[CRITERIA] (criteria, type) VALUES ('" + criteria + "','" + type + "') ";

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

        public DataSet selectAllUnidades(bool all, string unidades)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";
            string filter = "";

            if (all)
                query = "SELECT '' id_unidade, '- Selecione -' nome UNION ALL ";

            query += " SELECT id_unidade, nome FROM [base_monsanto].[dbo].[COOP_UNIDADES] ";

            if (!String.IsNullOrEmpty(unidades))
            {
                string[] units = unidades.Split(';');
                foreach (string unit in units)
                {
                    if (!string.IsNullOrEmpty(unit))
                        filter += "'" + unit + "',";
                }

                filter = filter.Substring(0, (filter.Trim().Length - 1));

                query += " WHERE NOME IN (" + filter + ")";
            }

            query += "    ORDER BY 1 ASC";

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_units.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public string insertUnidades(string unidade)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = "INSERT INTO [base_monsanto].[dbo].[COOP_UNIDADES] (nome) VALUES ('" + unidade + "') ";

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string deleteUnidades(string id_unidade)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = "DELETE FROM [base_monsanto].[dbo].[COOP_UNIDADES] WHERE id_unidade = " + id_unidade + " ";

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

            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[REGISTER] where regional = (select distinct region from [base_monsanto].[dbo].[REGIONS] where id = '" + id_region + "') AND COOPERANTES = 'S' ";

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

            query = " SELECT '' ID, '- Ano - ' SAFRA UNION ALL ";
            query += " SELECT * FROM [base_monsanto].[dbo].[COOP_SAFRA] ";
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

            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[COOP_CONTRACT] where safra = (select distinct safra from [base_monsanto].[dbo].[SAFRA] where id = '" + id_safra + "') ";

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
            query += " FROM [base_monsanto].[dbo].[COOP_CONTRACT] c ";
            query += " LEFT JOIN base_monsanto.dbo.REGISTER AS gr on gr.name = id_user_gr AND gr.type = 4 ";
            query += " LEFT JOIN base_monsanto.dbo.REGISTER AS rtv on rtv.name = id_user_rtv AND rtv.type = 3";
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
            query += " FROM [base_monsanto].[dbo].[COOP_CONTRACT] c";
            query += " LEFT JOIN base_monsanto.dbo.REGISTER AS gr on gr.name = id_user_gr AND gr.type = 4 ";
            query += " LEFT JOIN base_monsanto.dbo.REGISTER AS rtv on rtv.name = id_user_rtv AND rtv.type = 3";
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
            query += " FROM [base_monsanto].[dbo].[COOP_CONTRACT] c";
            query += " LEFT JOIN base_monsanto.dbo.REGISTER AS gr on gr.name = id_user_gr AND gr.type = 4 ";
            query += " LEFT JOIN base_monsanto.dbo.REGISTER AS rtv on rtv.name = id_user_rtv AND rtv.type = 3";
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
            query += " else '' end as 'Tipo de Contrato', ";
            query += "    substring(CAST(dt_receb as varchar),9,2) + '/' +  substring(CAST(dt_receb as varchar),6,2) + '/' + substring(CAST(dt_receb as varchar),1,4) as 'Data de Recebimento', ";
            query += "    case when status = 1 then 'Aprovado' else 'Reprovado' end as 'Status', ";
            query += "   id_user_rtv as 'RTV', ";
            query += "    id_user_gr as 'GR', ";
            query += "   substring(CAST(dt_digital as varchar),9,2) + '/' +  substring(CAST(dt_digital as varchar),6,2) + '/' + substring(CAST(dt_digital as varchar),1,4) as 'Data de Digitalização', ";
            query += "    substring(CAST(dt_approv as varchar),9,2) + '/' +  substring(CAST(dt_approv as varchar),6,2) + '/' + substring(CAST(dt_approv as varchar),1,4) as 'Data de Aprovação', ";
            query += "   substring(CAST(dt_archive as varchar),9,2) + '/' +  substring(CAST(dt_archive as varchar),6,2) + '/' + substring(CAST(dt_archive as varchar),1,4) as 'Data Keepers', ";
            query += "   keeper as 'N. Caixa Keepers', case when term = 'at' then 'Aditivo ao Termo' when term = 'ta' then 'Termo de Aditivo' else '' end as 'Tipo de Termo' ";
            query += " FROM [base_monsanto].[dbo].[COOP_CONTRACT]c ";
            query += " LEFT JOIN base_monsanto.dbo.REGISTER AS gr on gr.name = id_user_gr AND gr.type = 4 ";
            query += " LEFT JOIN base_monsanto.dbo.REGISTER AS rtv on rtv.name = id_user_rtv AND rtv.type = 3";
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

            query = " SELECT distinct distrito FROM [base_monsanto].[dbo].[REGISTER] WHERE name = '" + rtv + "' and type = 3 and (del = '' or del is null) ";

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

        public Boolean valueValidation(String value, int type)
        {
            bool ret = false;
            string query = "";

            query = " SELECT COUNT(*) FROM [base_monsanto].[dbo].[REGISTER] WHERE name = '" + value + "' and type = " + type + " ";

            dbAcess.stringSql(query);

            if (dbAcess.retQuery != "0")
                ret = true;

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

        public List<string> statusAtualContract(string id_contract)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();
            DataSet dsValue = new DataSet();
            List<string> retList = new List<string>();

            try
            {

                query = "SELECT MAX(D.ID) ID FROM [base_monsanto].[dbo].[COOP_FLUX] F INNER JOIN [base_monsanto].[dbo].[COOP_FLUX_DATA] D ON D.ID_FLUX = F.ID  WHERE D.id_contract = '" + id_contract + "'";

                string path = HttpContext.Current.Server.MapPath("data");
                memowrit(path + "/arquivo_log_stat_flux.txt", query);

                dbAcess.execSql("select", query);

                dsRet = dbAcess.dsReturn;

                if (dsRet != null && dsRet.Tables.Count > 0)
                {

                    query = "SELECT D.ID ID_REGISTRO, F.NAME NOME_FLUXO, D.DATEUPD DATA_FLUXO, D.STAT STATUS_FLUXO, D.ID_FLUX ID_FLUXO FROM [base_monsanto].[dbo].[COOP_FLUX] F INNER JOIN [base_monsanto].[dbo].[COOP_FLUX_DATA] D ON D.ID_FLUX = F.ID  WHERE D.ID = '" + dsRet.Tables[0].Rows[0][0] + "'";

                    path = HttpContext.Current.Server.MapPath("data");
                    memowrit(path + "/arquivo_log_stat_flux_final.txt", query);

                    dbAcess.dsReturn.Clear();

                    dbAcess.execSql("select", query);

                    dsValue = dbAcess.dsReturn;

                    for (int d = 0; d < dsValue.Tables[0].Rows.Count; d++)
                    {
                        retList.Add(dsValue.Tables[0].Rows[d]["ID_REGISTRO"].ToString());
                        retList.Add(dsValue.Tables[0].Rows[d]["NOME_FLUXO"].ToString());
                        retList.Add(dsValue.Tables[0].Rows[d]["DATA_FLUXO"].ToString());
                        retList.Add(dsValue.Tables[0].Rows[d]["STATUS_FLUXO"].ToString());
                        retList.Add(dsValue.Tables[0].Rows[d]["ID_FLUXO"].ToString());
                    }

                    return retList;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return null;
        }

        public void updateStatusFlux(string id_flux, string status, string dateflux)
        {
            String query = "";
            DataSet dsRet = new DataSet();

            query = " UPDATE [base_monsanto].[dbo].[COOP_FLUX_DATA] SET dateupd = CONVERT(DATE,'" + dateflux + "',105), stat = '" + status + "' WHERE id = " + id_flux + "";

            dbAcess.execSql("update", query);
        }

        public string retDateFromFlux(string id_flux, string id_contract, string stat)
        {
            string query = "";
            dbAcess.retQuery = "";

            query = " SELECT MAX(DATEUPD) dateupd FROM [base_monsanto].[dbo].[COOP_FLUX_DATA] WHERE ID_FLUX = " + id_flux + " AND ID_CONTRACT = " + id_contract;


            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_retDateFromFlux" + id_flux + ".txt", query);

            dbAcess.stringSql(query);

            if (string.IsNullOrEmpty(dbAcess.retQuery))
                return "";
            else
                return dbAcess.retQuery;
        }

        public DataSet selectFlux()
        {
            DataSet dsRet = new DataSet();
            dbAcess.retQuery = "";

            query = " SELECT '' ID, '- Etapa -' NAME UNION ALL ";
            query += " SELECT ID, NAME FROM [base_monsanto].[dbo].[COOP_FLUX] ORDER BY ID";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;

            return dsRet;
        }

        public String insertAndamento(String id_contract, String andamento, String dtandamento, string lembrar, string dtnewlembrete)
        {
            String query = "";
            String ret = "";
            DataSet dsRet = new DataSet();

            query = " INSERT INTO [base_monsanto].[dbo].[COOP_PROGRESS] (id_contract, andamento, dtandamento, lembrar, dtlembrar) ";
            query += " VALUES ( {0}, '{1}', CAST(NULLIF('{2}','') as datetime), '{3}', CAST(NULLIF('{4}','') as datetime) )";

            query = String.Format(query, id_contract, andamento, dtandamento, lembrar, dtnewlembrete);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log.txt", query);

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public DataSet selectAndamento(String id_contract)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();

            query = " SELECT andamento 'Andamento', dtandamento as 'Data do Andamento', case when lembrar = 'S' then 'Sim' else 'Não' end lembrar, dtlembrar as 'Data do Lembrete', ID FROM [base_monsanto].[dbo].[COOP_PROGRESS] WHERE id_contract = '" + id_contract + "' ORDER BY 2 DESC ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0 && dsRet.Tables[0].Rows.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public String insertContact(String id_contract, String andamento, String dtandamento, string lembrar, string dtnewlembrete)
        {
            String query = "";
            String ret = "";
            DataSet dsRet = new DataSet();

            query = " INSERT INTO [base_monsanto].[dbo].[COOP_PROGRESS] (id_contract, andamento, dtandamento, lembrar, dtlembrar) ";
            query += " VALUES ( {0}, '{1}', CAST(NULLIF('{2}','') as datetime), '{3}', CAST(NULLIF('{4}','') as datetime) )";

            query = String.Format(query, id_contract, andamento, dtandamento, lembrar, dtnewlembrete);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log.txt", query);

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public String deleteContact(String id)
        {
            String query = "";
            String ret = "";
            query = " DELETE FROM [base_monsanto].[dbo].[COOP_PROGRESS] ";
            query += " WHERE id ='{0}' ";

            query = String.Format(query, id);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_del_contact.txt", query);

            dbAcess.execSql("delete", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public DataSet selectContactRegister(String id)
        {
            String query = "";
            query = " SELECT * FROM [base_monsanto].[dbo].[COOP_CONTACT] ";
            query += " WHERE id_coop_contact ='{0}' ";

            query = String.Format(query, id);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_sel_contact.txt", query);

            dbAcess.execSql("select", query);

            return dbAcess.dsReturn;
        }


        public DataSet selectContact(String id_contract)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();

            query = " SELECT andamento 'Contato', CAST(dtandamento AS DATE) as 'Data do Contato', case when lembrar = 'S' then 'Sim' else 'Não' end lembrar, CAST( dtlembrar AS DATE) as 'Data do Lembrete', ID FROM [base_monsanto].[dbo].[COOP_PROGRESS] WHERE id_contract = '" + id_contract + "' AND andamento not like '%<br%' ORDER BY 2 DESC ";

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0 && dsRet.Tables[0].Rows.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public String updateContact(String id)
        {
            String query = "";
            String ret = "";
            query = " UPDATE [base_monsanto].[dbo].[COOP_PROGRESS] SET lembrar = 'N' ";
            query += " WHERE id ='{0}' ";

            query = String.Format(query, id);

            dbAcess.execSql("update", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public DataSet retRemember()
        {
            dbAcess dbAcess = new dbAcess();
            DataSet dsRet = new DataSet();
            string query = "";
            dbAcess.dsReturn.Clear();

            query = @"  SELECT *, P.ID ID_ACOMP ";
            query += "        FROM [base_monsanto].[DBO].COOP_PROGRESS P ";
            query += "        INNER JOIN [{0}].[DBO].[COOP_PROCESS] C ON C.ID = P.ID_CONTRACT ";
            query += "        INNER JOIN [{0}].[DBO].[COOP_CUSTOMER] CL ON CL.ID = C.ID_COOP ";
            query += "       AND P.LEMBRAR = 'S' ";
            query = String.Format(query, "base_monsanto");

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;

            return dsRet;
        }

        public String updateProgress(String id)
        {
            String query = "";
            String ret = "";
            query = " UPDATE [base_monsanto].[dbo].[COOP_PROGRESS] SET lembrar = 'N' ";
            query += " WHERE id ='{0}' ";

            query = String.Format(query, id);

            dbAcess.execSql("update", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public DataSet selectProcessToReport(string tipo, string unidade, string document, string etapa, string nome, string unilimits, bool cancelados,
            string tpsafra, string safra, string contrato_mae, string contrato_base)
        {
            dbAcess dbAcess = new dbAcess();
            DataSet dsRet = new DataSet();
            string query = "";
            dbAcess.dsReturn.Clear();
            string filter = "";

            query = @"  SELECT DISTINCT PRO.ID,
                               PRO.num_cont_mae NroContratoMae,
                               PRO.num_cont_base NroContratoBase,
                               inf.NAME     Tipo,
                               CLI.NAME     Cooperante,
                               CLI.DOCUMENT Documento,
                               CONVERT(VARCHAR, PRO.dtemis, 103) DataContrato,
                               CONVERT(VARCHAR, FLUD_3.dateupd, 103) DataEnvioCooperante,
                               CONVERT(VARCHAR, FLUD_6.dateupd, 103) DataEnvioMonsanto,
                               CASE WHEN (SELECT COUNT(*) FROM [COOP_FLUX_DATA] WHERE id_contract = PRO.id AND STAT = 4) > 0
                               THEN 'Documento Cancelado' ELSE FLU.NAME END Status,
                               ( SELECT CONVERT(VARCHAR, MAX(DATE_CONTACT), 103)  FROM coop_contact_joined joi WHERE joi.id_process = pro.id) DataContato,
                               ( select TOP 1 con.description from COOP_CONTACT_JOINED joi
                               inner join COOP_CONTACT con ON con.id_coop_contact = joi.id_coop_contact
                               where id_process = PRO.id
                               order by joi.id_contact_joined DESC) HistoricoContato,
                               UNIDADE Unidade,
                               CASE
                                 WHEN TPSAFRA = 'V' THEN 'Verão'
                                 WHEN tpsafra = 'I' THEN 'Inverno'
                                 ELSE ''
                               END          Safra,
                               SAFRA        Ano,
                               PRO.FAZENDA Fazenda,
                               CAST(PRO.DESAGIO AS VARCHAR) + '%' Desagio, 
                               CAST(PRO.AREA AS VARCHAR) as AreaContratada,
                               CAST(PRO.TETO AS VARCHAR) Teto,
                               CONVERT(VARCHAR, PRO.DT_INI_PLA, 103) DtIniPlantio,
                               CONVERT(VARCHAR, PRO.DT_FIM_PLA, 103) DtFimPlantio,
                               CASE WHEN PRO.urgente = 'S' THEN 'SIM' ELSE 'Não' END urgente
                        FROM   [COOP_PROCESS] PRO
                               INNER JOIN [COOP_CUSTOMER] CLI
                                       ON CLI.ID = PRO.ID_COOP
                                      AND (CLI.del is null or CLI.del = '')
                               INNER JOIN [COOP_FLUX] FLU
                                       ON FLU.ID = PRO.ETAPA
                               LEFT JOIN [COOP_FLUX_DATA] FLUD_3
								ON FLUD_3.id_contract = PRO.id 
								AND FLUD_3.id_flux = 3
								LEFT JOIN [COOP_FLUX_DATA] FLUD_6
								ON FLUD_6.id_contract = PRO.id 
								AND FLUD_6.id_flux = 6
                               INNER JOIN [COOP_PROCESS_INFO] inf
                                       ON inf.id_process = PRO.type and inf.active = 'S'
                               WHERE PRO.dtemis >= '2016-01-01'";

            if (!string.IsNullOrEmpty(tipo) && tipo != "0")
                query += " AND PRO.type IN (" + tipo + ") ";

            if (!string.IsNullOrEmpty(unidade) && !unidade.Contains("Selecione"))
            {

                query += " AND PRO.unidade = '" + unidade + "' ";

            }

            if (!cancelados)
            {
                query += " AND NOT EXISTS (SELECT * FROM [{0}].[dbo].[COOP_FLUX_DATA] WHERE ID_CONTRACT = PRO.ID AND STAT = 4) ";
            }

            if (!String.IsNullOrEmpty(unilimits))
            {
                string[] units = unilimits.Split(';');
                foreach (string unit in units)
                {
                    if (!string.IsNullOrEmpty(unit))
                        filter += "'" + unit + "',";
                }

                filter = filter.Substring(0, (filter.Trim().Length - 1));

                query += " AND PRO.unidade in (" + filter + ")";
            }


            if (!string.IsNullOrEmpty(document))
                query += " AND CLI.DOCUMENT = '" + document + "' ";

            if (!string.IsNullOrEmpty(nome))
                query += " AND CLI.NAME like '%" + nome + "%' ";

            if (!string.IsNullOrEmpty(etapa) && etapa != "0")
                query += " AND PRO.ETAPA = '" + etapa + "' ";

            if (!string.IsNullOrEmpty(tpsafra) && tpsafra != "0")
                query += " AND PRO.tpsafra = '" + tpsafra + "' ";

            if (!string.IsNullOrEmpty(safra) && !safra.Contains("Ano"))
                query += " AND PRO.safra = '" + safra + "' ";

            if (!string.IsNullOrEmpty(contrato_mae))
                query += " AND PRO.num_cont_mae like '%" + contrato_mae + "%' ";

            if (!string.IsNullOrEmpty(contrato_base))
                query += " AND PRO.num_cont_base like '%" + contrato_base + "%' ";

            query += " ORDER BY CLI.NAME";

            query = String.Format(query, "base_monsanto");

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_report.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;

            return dsRet;
        }

        public DataSet retTotByEtapa(string tipo, string unidade, string document, string etapa, string nome, string tpsafra, string safra, bool cancelados)
        {
            dbAcess dbAcess = new dbAcess();
            DataSet dsRet = new DataSet();
            string query = "";
            dbAcess.dsReturn.Clear();

            query = @"  SELECT Count(PRO.id)TOTAL,
                               PRO.etapa    ETAPA
                        FROM   [{0}].[dbo].[COOP_PROCESS] PRO
                        INNER JOIN [{0}].[dbo].[COOP_CUSTOMER] CLI ON CLI.id = PRO.id_coop
                        INNER JOIN [{0}].[dbo].[COOP_PROCESS_INFO] inf  ON inf.id_process = PRO.type and inf.active = 'S'
                        WHERE  PRO.etapa IS NOT NULL AND PRO.dtemis >= '2016-01-01' ";

            if (!string.IsNullOrEmpty(tipo) && tipo != "0")
                query += " AND PRO.type IN (" + tipo + ") ";

            if (!string.IsNullOrEmpty(unidade) && !unidade.Contains("Selecione"))
                query += " AND PRO.unidade = '" + unidade + "' ";

            if (!string.IsNullOrEmpty(tpsafra) && tpsafra != "0")
                query += " AND PRO.tpsafra = '" + tpsafra + "' ";

            if (!string.IsNullOrEmpty(safra) && !safra.Contains("Ano"))
                query += " AND PRO.safra = '" + safra + "' ";

            if (!string.IsNullOrEmpty(document))
                query += " AND CLI.DOCUMENT = '" + document + "' ";

            if (!string.IsNullOrEmpty(nome))
                query += " AND CLI.NAME like '%" + nome + "%' ";

            if (!string.IsNullOrEmpty(etapa) && etapa != "0")
                query += " AND PRO.ETAPA = '" + etapa + "' ";

            if (!cancelados)
            {
                query += " AND NOT EXISTS (SELECT * FROM [{0}].[dbo].[COOP_FLUX_DATA] WHERE ID_CONTRACT = PRO.ID AND STAT = 4) ";
            }

            query += @" GROUP  BY PRO.etapa
                        ORDER  BY PRO.etapa  ";

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_report_pizza.txt", query);

            query = String.Format(query, "base_monsanto");

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;

            return dsRet;
        }

        public Decimal retTotalToPizza(string tipo, string unidade, string document, string etapa, string nome, string tpsafra, string safra, bool cancelados)
        {
            dbAcess dbAcess = new dbAcess();
            DataSet dsRet = new DataSet();
            string query = "";
            dbAcess.dsReturn.Clear();

            query = @"  SELECT Count(PRO.id)TOTAL
                        FROM   [{0}].[dbo].[COOP_PROCESS] PRO
                        INNER JOIN [{0}].[dbo].[COOP_CUSTOMER] CLI ON CLI.id = PRO.id_coop
                        INNER JOIN [{0}].[dbo].[COOP_PROCESS_INFO] inf  ON inf.id_process = PRO.type and inf.active = 'S'
                        WHERE  PRO.etapa IS NOT NULL AND PRO.dtemis >= '2016-01-01' ";

            if (!string.IsNullOrEmpty(tipo) && tipo != "0")
                query += " AND PRO.type IN (" + tipo + ") ";

            if (!string.IsNullOrEmpty(unidade) && !unidade.Contains("Selecione"))
                query += " AND PRO.unidade = '" + unidade + "' ";

            if (!string.IsNullOrEmpty(tpsafra) && tpsafra != "0")
                query += " AND PRO.tpsafra = '" + tpsafra + "' ";

            if (!string.IsNullOrEmpty(safra) && !safra.Contains("Ano"))
                query += " AND PRO.safra = '" + safra + "' ";

            if (!string.IsNullOrEmpty(document))
                query += " AND CLI.DOCUMENT = '" + document + "' ";

            if (!string.IsNullOrEmpty(nome))
                query += " AND CLI.NAME like '%" + nome + "%' ";

            if (!string.IsNullOrEmpty(etapa) && etapa != "0")
                query += " AND PRO.ETAPA = '" + etapa + "' ";

            if (!cancelados)
            {
                query += " AND NOT EXISTS (SELECT * FROM [{0}].[dbo].[COOP_FLUX_DATA] WHERE ID_CONTRACT = PRO.ID AND STAT = 4) ";
            }

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_report_pizza_total.txt", query);

            query = String.Format(query, "base_monsanto");

            dbAcess.stringSql(query);

            return (!string.IsNullOrEmpty(dbAcess.retQuery)) ? Convert.ToDecimal(dbAcess.retQuery) : 1;
        }

        public DataSet selectAllEtapas(bool all)
        {
            dbAcess dbAcess = new dbAcess();
            DataSet dsRet = new DataSet();
            string query = "";
            dbAcess.dsReturn.Clear();

            if (all)
                query = @" SELECT '' ID, '- Todas -' ETAPA, '' SEQUENCIA UNION ALL ";
            query += @"  SELECT id, name, sequence FROM [{0}].[dbo].[COOP_FLUX] ORDER BY ID ";

            query = String.Format(query, "base_monsanto");

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_report_data.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;

            return dsRet;
        }

        public DataSet selectKPI(string tipo, string unidade, string document, string etapa, string nome, string unilimits, bool cancelados,
            string tpsafra, string safra, string cooperante)
        {
            dbAcess dbAcess = new dbAcess();
            DataSet dsRet = new DataSet();
            string query = "";
            dbAcess.dsReturn.Clear();
            string filter = "";

            query += @"  SELECT PROCE.ID CONTRACT, (SELECT name FROM COOP_PROCESS_INFO INF WHERE INF.id_process = PROCE.TYPE) TIPO_CONTRATO, 
                            PROCE.num_cont_base CONTRATO_BASE, 
                            PROCE.num_cont_mae CONTRATO_MAE,
                            CASE WHEN (SELECT COUNT(*) FROM [base_monsanto].[dbo].[COOP_FLUX_DATA] WHERE id_contract = PROCE.id AND STAT = 4) > 0
                            THEN 'Documento Cancelado' ELSE FLU.NAME END Status,
                            CASE WHEN PROCE.tpsafra = 'I' THEN 'Inverno' WHEN PROCE.tpsafra = 'V' THEN 'Verão' ELSE '' END SAFRA,
                            PROCE.safra ANO,
                            coop.NAME,  
                            (SELECT MAX(DATEUPD) 
                            FROM [base_monsanto].[dbo].COOP_FLUX_DATA DT 
                            WHERE DT.ID_CONTRACT = PROCE.ID 
                            AND DT.ID_FLUX = 1
                            AND DT.dateupd is not null) '1',
                            (SELECT MAX(DATEUPD) 
                            FROM [base_monsanto].[dbo].COOP_FLUX_DATA DT 
                            WHERE DT.ID_CONTRACT = PROCE.ID 
                            AND DT.ID_FLUX = 2
                            AND DT.dateupd is not null) '2',
                            (SELECT MAX(DATEUPD) 
                            FROM [base_monsanto].[dbo].COOP_FLUX_DATA DT 
                            WHERE DT.ID_CONTRACT = PROCE.ID 
                            AND DT.ID_FLUX = 3
                            AND DT.dateupd is not null) '3',
                            (SELECT MAX(DATEUPD) 
                            FROM [base_monsanto].[dbo].COOP_FLUX_DATA DT 
                            WHERE DT.ID_CONTRACT = PROCE.ID 
                            AND DT.ID_FLUX = 4
                            AND DT.dateupd is not null) '4',
                            (SELECT MAX(DATEUPD) 
                            FROM [base_monsanto].[dbo].COOP_FLUX_DATA DT 
                            WHERE DT.ID_CONTRACT = PROCE.ID 
                            AND DT.ID_FLUX = 5
                            AND DT.dateupd is not null) '5',
                            (SELECT MAX(DATEUPD) 
                            FROM [base_monsanto].[dbo].COOP_FLUX_DATA DT 
                            WHERE DT.ID_CONTRACT = PROCE.ID 
                            AND DT.ID_FLUX = 6
                            AND DT.dateupd is not null) '6',
                            (SELECT MAX(DATEUPD) 
                            FROM [base_monsanto].[dbo].COOP_FLUX_DATA DT 
                            WHERE DT.ID_CONTRACT = PROCE.ID 
                            AND DT.ID_FLUX = 7
                            AND DT.dateupd is not null) '7'
                            FROM [base_monsanto].[dbo].COOP_PROCESS PROCE
                            INNER JOIN COOP_CUSTOMER COOP ON COOP.ID = PROCE.ID_COOP
                            INNER JOIN [base_monsanto].[dbo].[COOP_FLUX] FLU
                            ON FLU.ID = PROCE.ETAPA
                            WHERE PROCE.ID > 0 ";

            if (!string.IsNullOrEmpty(tipo) && tipo != "0")
                query += " AND PROCE.type = '" + tipo + "' ";

            if (!string.IsNullOrEmpty(unidade) && !unidade.Contains("Selecione"))
            {

                query += " AND PROCE.unidade = '" + unidade + "' ";

            }

            if (!cancelados)
            {
                query += " AND NOT EXISTS (SELECT * FROM [{0}].[dbo].[COOP_FLUX_DATA] WHERE ID_CONTRACT = PROCE.ID AND STAT = 4) ";
            }

            if (!String.IsNullOrEmpty(unilimits))
            {
                string[] units = unilimits.Split(';');
                foreach (string unit in units)
                {
                    if (!string.IsNullOrEmpty(unit))
                        filter += "'" + unit + "',";
                }

                filter = filter.Substring(0, (filter.Trim().Length - 1));

                query += " AND PROCE.unidade in (" + filter + ")";
            }

            if (!string.IsNullOrEmpty(etapa) && etapa != "0")
                query += " AND PROCE.ETAPA = '" + etapa + "' ";

            if (!string.IsNullOrEmpty(tpsafra) && tpsafra != "0")
                query += " AND PROCE.tpsafra = '" + tpsafra + "' ";

            if (!string.IsNullOrEmpty(safra) && !safra.Contains("Ano"))
                query += " AND PROCE.safra = '" + safra + "' ";

            if (!string.IsNullOrEmpty(cooperante))
            {
                query += " AND PROCE.id_coop IN (SELECT ID FROM COOP_CUSTOMER WHERE NAME like '%" + cooperante + "%' )";
            }

            query += " ORDER BY COOP.name, PROCE.type";

            query = String.Format(query, "base_monsanto");

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_report_kpi_data.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;

            return dsRet;
        }

        public string getTimeFluxById(int id_contract, int id_flux_ini, int id_flux_end)
        {
            string query = "";

            query = @"DECLARE @Startdate DATETIME, @Enddate DATETIME
                        SET @Startdate = (select MAX(dateupd) from [dbo].COOP_FLUX_DATA where id_contract = {1} and id_flux = {2} and dateupd is not null)
                        SET @Enddate = (select MAX(dateupd) from [dbo].COOP_FLUX_DATA where id_contract = {1} and id_flux = {3} and dateupd is not null)

                        -- Query by SqlServerCurry.com
                        -- Total seconds in a day
                        DECLARE @TotalSec int
                        SET @TotalSec = 24*60*60;

                        -- Convert DateDiff into seconds
                        DECLARE @DiffSecs int
                        SET @DiffSecs = DATEDIFF(SECOND, @Startdate, @Enddate)

                        SELECT
                        CONVERT(varchar, (@DiffSecs/@TotalSec)) + ' Dias' + ' ' +
                        CONVERT(char(2), ((@DiffSecs%@TotalSec)/3600)) + ' Horas' + ' ' +
                        CONVERT(char(2), (((@DiffSecs%@TotalSec)%3600)/60)) + ' Minutos' + ' ' +
                        CONVERT(char(2), (((@DiffSecs%@TotalSec)%3600)%60)) + ' Segundos' as Tempo";

            query = string.Format(query, "base_monsanto", id_contract, id_flux_ini, id_flux_end);

            dbAcess.stringSql(query);

            if (!string.IsNullOrEmpty(dbAcess.retQuery))
                return dbAcess.retQuery;
            else
                return "";
        }

        public Int64 getDayFluxById(int id_contract, int id_flux_ini, int id_flux_end)
        {
            string query = "";

            query = @"DECLARE @Startdate DATETIME, @Enddate DATETIME
                        SET @Startdate = (select MAX(dateupd) from [dbo].COOP_FLUX_DATA where id_contract = {1} and id_flux = {2} and dateupd is not null)
                        SET @Enddate = (select MAX(dateupd) from [dbo].COOP_FLUX_DATA where id_contract = {1} and id_flux = {3} and dateupd is not null)

                        -- Query by SqlServerCurry.com
                        -- Total seconds in a day
                        DECLARE @TotalSec int
                        SET @TotalSec = 24*60*60;

                        -- Convert DateDiff into seconds
                        DECLARE @DiffSecs int
                        SET @DiffSecs = DATEDIFF(SECOND, @Startdate, @Enddate)

                        SELECT
                        CONVERT(varchar, (@DiffSecs/@TotalSec)) Tempo";

            query = string.Format(query, "base_monsanto", id_contract, id_flux_ini, id_flux_end);

            dbAcess.stringSql(query);

            if (!string.IsNullOrEmpty(dbAcess.retQuery))
                return dbAcess.retQuery == null ? -1 : Convert.ToInt64(dbAcess.retQuery);
            else
                return 0;
        }

        public void retDataToKPIGraph(ref int kpi3, ref int kpi4, ref int kpi14, ref int kpi22, ref int kpi30, ref int kpi31)
        {
            dbAcess.dsReturn.Clear();
            string query = @" SELECT distinct id_contract ID_CONTRACT FROM COOP_FLUX_DATA WHERE stat = 2 AND dateupd is not null ORDER BY 1 ";

            dbAcess.execSql("select", query);

            if (dbAcess.dsReturn != null)
            {
                for (int i = 0; i < dbAcess.dsReturn.Tables[0].Rows.Count; i++)
                {
                    var splint = getDayFluxById(int.Parse(dbAcess.dsReturn.Tables[0].Rows[i]["ID_CONTRACT"].ToString()), 1, 6);

                    if (splint >= 1 && splint <= 3)
                        kpi3++;
                    else if (splint >= 4 && splint <= 7)
                        kpi4++;
                    else if (splint >= 8 && splint <= 14)
                        kpi14++;
                    else if (splint >= 15 && splint <= 22)
                        kpi22++;
                    else if (splint >= 23 && splint <= 30)
                        kpi30++;
                    else if (splint >= 31)
                        kpi31++;
                }

            }
        }

        public DataSet selectContactPrev(string idContact)
        {
            dbAcess dbAcess = new dbAcess();
            DataSet dsRet = new DataSet();
            string query = "";
            dbAcess.dsReturn.Clear();

            query = @"  SELECT * FROM [{0}].[dbo].[COOP_CONTACT] ";

            if (!string.IsNullOrEmpty(idContact))
                query += @" WHERE id_coop_contact = '" + idContact + "' ";

            query = String.Format(query, "base_monsanto");

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_contact_data.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;

            return dsRet;
        }

        public DataSet selectContactJoined(string id_process)
        {
            dbAcess dbAcess = new dbAcess();
            DataSet dsRet = new DataSet();
            string query = "";
            dbAcess.dsReturn.Clear();

            query += @"  select id_contact_joined, date_contact data, description contato, flux_data.name etapa
                     from COOP_CONTACT_JOINED joi
                    inner join COOP_CONTACT con on con.id_coop_contact = joi.id_coop_contact
                    inner join COOP_FLUX_data flux on flux.id_contract = joi.id_process
                    and flux.id_flux = joi.id_coop_flux
                    inner join coop_FLUX flux_data on flux_data.id = flux.id_flux
                    where joi.id_process = {0}  ";

            query = String.Format(query, id_process);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_contact_joined__data.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;

            return dsRet;
        }

        public String insertContactPrev(String description)
        {
            String query = "";
            String ret = "";

            query = " INSERT INTO [base_monsanto].[dbo].[COOP_CONTACT] (description) ";
            query += " VALUES ('{0}')";

            query = String.Format(query, description);

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public String insertContactJoined(int id_process, int id_coop_contract, int id_coop_flux, string date_contact)
        {
            String query = "";
            String ret = "";

            query = " INSERT INTO [base_monsanto].[dbo].[COOP_CONTACT_JOINED] (id_process, id_coop_contact, id_coop_flux, date_contact) ";
            query += " VALUES ({0}, {1}, {2}, CAST(NULLIF('{3}','') as date) )";

            query = String.Format(query, id_process, id_coop_contract, id_coop_flux, date_contact);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_insert_prev_data.txt", query);

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string updateContactPrev(string id_coop_contact, string description)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = "UPDATE [base_monsanto].[dbo].[COOP_CONTACT] SET description = '" + description + "' WHERE id_coop_contact = " + id_coop_contact + " ";

            dbAcess.execSql("delete", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string deleteContactPrev(string id_coop_contact)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = "DELETE FROM [base_monsanto].[dbo].[COOP_CONTACT] WHERE id_coop_contact = " + id_coop_contact + " ";

            dbAcess.execSql("delete", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public string deleteContactJoined(string id_contact_joined)
        {
            String query = "";
            string ret = "";
            dbAcess.retQuery = "";

            query = "DELETE FROM [base_monsanto].[dbo].[COOP_CONTACT_JOINED] WHERE id_contact_joined = " + id_contact_joined + " ";

            dbAcess.execSql("delete", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public void rollbackFlux(string id_contract)
        {
            string query = " UPDATE  [base_monsanto].[dbo].[COOP_FLUX_DATA] SET stat = 1  WHERE id_contract = " + id_contract + " AND id_flux  = (SELECT (MAX(id_flux)-1) FROM [base_monsanto].[dbo].[COOP_FLUX_DATA] WHERE id_contract = " + id_contract + ") ";

            dbAcess.execSql("update", query);

            query = "DELETE FROM [base_monsanto].[dbo].[COOP_FLUX_DATA] WHERE id_contract = " + id_contract + " AND id_flux = (SELECT MAX(id_flux) FROM [base_monsanto].[dbo].[COOP_FLUX_DATA] WHERE id_contract = " + id_contract + ") ";

            dbAcess.execSql("delete", query);

            query = " UPDATE [base_monsanto].[dbo].[COOP_PROCESS] SET etapa = CASE WHEN etapa = 1 THEN etapa ELSE (etapa-1) END WHERE id = " + id_contract + " ";

            dbAcess.execSql("update", query);
        }

        public String insertTipoProcesso(string name, string ativo)
        {
            String query = "";
            String ret = "";

            query = " INSERT INTO [base_monsanto].[dbo].[COOP_PROCESS_INFO] (id_process, name, active) ";
            query += " VALUES ( (SELECT MAX(ID_PROCESS)+1 FROM [base_monsanto].[dbo].[COOP_PROCESS_INFO]), '{0}', '{1}')";

            query = String.Format(query, name, ativo);

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_insert_tipo_process.txt", query);

            dbAcess.execSql("insert", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public String deleteTipoProcesso(String id_process)
        {
            String query = "";
            String ret = "";
            query = " DELETE FROM [base_monsanto].[dbo].[COOP_PROCESS_INFO] ";
            query += " WHERE id_process ='{0}' ";

            query = String.Format(query, id_process);

            dbAcess.execSql("delete", query);

            ret = dbAcess.retQuery;

            return ret;
        }

        public DataSet selectTipoProcesso(String id_process, String ativo)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();

            query = "SELECT '' id_process, '- Todos -' name, '' active UNION ALL";

            query += " SELECT id_process, name, active FROM [base_monsanto].[dbo].[COOP_PROCESS_INFO] WHERE id_process > 0 ";

            if (!string.IsNullOrEmpty(id_process))
                query += " and id_process = '" + id_process + "' ";

            if (!string.IsNullOrEmpty(ativo))
                query += " AND active = '" + ativo + "' ";

            query += " ORDER BY 1";

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_select_tipo_process.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0 && dsRet.Tables[0].Rows.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectTipoProcessoBase(String id_process, String ativo)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();

            query += " SELECT id_process, name, active FROM [base_monsanto].[dbo].[COOP_PROCESS_INFO] WHERE id_process > 0 ";

            if (!string.IsNullOrEmpty(id_process))
                query += " and id_process = '" + id_process + "' ";

            if (!string.IsNullOrEmpty(ativo))
                query += " AND active = '" + ativo + "' ";

            query += " ORDER BY 1";

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_select_tipo_process.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0 && dsRet.Tables[0].Rows.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public DataSet selectTipoProcessoToReport(String id_process, String ativo)
        {
            String query = "";
            DataSet dsRet = new DataSet();
            dbAcess.dsReturn.Clear();

            query += " SELECT 0 id_process, '- Todos -' name UNION ALL ";
            query += " SELECT id_process, name FROM [base_monsanto].[dbo].[COOP_PROCESS_INFO] WHERE id_process > 0";

            if (!string.IsNullOrEmpty(id_process))
                query += " and id_process = '" + id_process + "' ";

            if (!string.IsNullOrEmpty(ativo))
                query += " AND active = '" + ativo + "' ";

            query += " ORDER BY 1";

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_select_tipo_process_report.txt", query);

            dbAcess.execSql("select", query);

            dsRet = dbAcess.dsReturn;
            if (dsRet != null && dsRet.Tables.Count > 0 && dsRet.Tables[0].Rows.Count > 0)
            {
                return dsRet;
            }

            return null;
        }

        public String updateTipoProcesso(String id_process, String active, String name)
        {
            String query = "";
            String ret = "";
            query = " UPDATE [base_monsanto].[dbo].[COOP_PROCESS_INFO] SET active = '" + active + "', name = '" + name + "' WHERE id_process = '" + id_process + "' ";

            string path = HttpContext.Current.Server.MapPath("data");
            memowrit(path + "/arquivo_log_update_tipo_process.txt", query);

            dbAcess.execSql("update", query);

            ret = dbAcess.retQuery;

            return ret;
        }

    }
}