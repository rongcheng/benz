using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

namespace WebUI
{
    public partial class U8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.rptProduct.Visible = false;
                this.btnUpdate.Visible = false;

            }

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindSearch();
        }


        private void bindSearch()
        {
            string sql = "select constid,productid from saletou8 where productid not in (select productid from product)";
            string connU8="user id=sa;password=;initial catalog=UFInterfaceDB;data source=192.168.124.117;";
            try
            {
                SqlConnection conn1 = new SqlConnection(connU8);
                conn1.Open();

                SqlCommand cmd1 = new SqlCommand(sql, conn1);
                cmd1.CommandType = CommandType.Text;


                SqlDataReader sdr=cmd1.ExecuteReader();
                //SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                //DataSet ds1 = new DataSet();
                //sda.Fill(ds1);

                
                

                //this.dataGridView1.DataSource = ds1.Tables[0];
                //this.dataGridView1.bi

                if (sdr.HasRows)
                {
                    this.rptProduct.DataSource = sdr;
                    this.rptProduct.DataBind();

                    this.btnUpdate.Visible = true;
                    this.rptProduct.Visible = true;
                }
                else
                {
                    this.rptProduct.Visible = false;
                    this.btnUpdate.Visible = false;
                }
                
                sdr.Close();
                conn1.Close();


            }
            catch (Exception e1)
            {


            }
        
        
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!this.rptProduct.Visible)
                return;

            string pids = string.Empty;
            bool isCheck = false;

            foreach (RepeaterItem ri in this.rptProduct.Items)
            {

                CheckBox cb = (CheckBox)ri.FindControl("chb");

                if (cb.Checked)
                {
                    //Response.Write(ri.DataItem["constid"]);
                    //Response.Write("<Br>");

                    HtmlInputControl hic=(HtmlInputControl)ri.FindControl("pid");
                    string pid = hic.Value;
                    pids = pids + "'"+pid + "',";

                    
                    //Response.Write(hic.Value);

                    isCheck = true;
                   
                }
            }

            pids = pids.TrimEnd(",".ToCharArray());

            if (isCheck)
            { 
                //更新数据库
                string connBoss = "user id=psboss;password=pan@data2006;initial catalog=psboss;data source=192.168.72.22,1712;";
                try
                {
                    SqlConnection conn1 = new SqlConnection(connBoss);
                    conn1.Open();

                    string sql = string.Format("update product_master set oprflag=0 where productid in ({0})",pids);
                    SqlCommand cmd1 = new SqlCommand(sql, conn1);
                    cmd1.CommandType = CommandType.Text;

                    int i = cmd1.ExecuteNonQuery();

                    this.lblMsg.Text = "执行完成，共 "+i.ToString()+" 条数据被更新";
                }
                catch (Exception e1)
                {


                }


            
            }

            //Response.Write(pids);


            //bindSearch();

        }
}
}
