using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using QJVRMS.Common;

namespace WebUI.Modules.Manage
{
    public partial class IndexFlashImagesConfig :AuthPage
    {
        private string xmlFile = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            xmlFile = Server.MapPath("/xml/IndexFlashImages.xml");
            if (!IsPostBack)
            {
                bindImages();
            }
        }


        private void bindImages()
        {

            string imagePath=GetImagePath();
            DataTable dtUpload = GetUploadImages();
            DataTable dtDefault = GetDefaultImages();
            DataTable dtAll = new DataTable();

            if (dtUpload == null)
            {
                dtUpload = new DataTable("UploadImage");
                foreach (DataColumn columm in dtDefault.Columns)
                {
                    dtUpload.Columns.Add(columm.ColumnName, columm.DataType);
                    dtAll.Columns.Add(columm.ColumnName, columm.DataType);

                }   
            }

            DataColumn dc = new DataColumn("Source");
            DataColumn dc1 = new DataColumn("Source");
            dtUpload.Columns.Add(dc);
            dtDefault.Columns.Add(dc1);

            if (dtUpload.Rows.Count > 0)
            {
                foreach (DataRow dr in dtUpload.Rows)
                {
                    dr["source"] = "1";
                }
            }


            if (dtDefault.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDefault.Rows)
                {
                    dr["source"] = "0";
                }

            }

            //DataView dv = dtUpload.DefaultView;
            


            dtAll.Merge(dtUpload);
            dtAll.Merge(dtDefault);
            //dtUpload.Merge(dtDefault);

            DataView dv = dtAll.DefaultView;
            dv.Sort = "Order";

            foreach (DataRow dr in dtAll.Rows)
            {
                dr["src"] = imagePath + dr["src"]+".jpg";
                //dr["status"] = (dr["status"].ToString() == "1" ? "可用" : "禁用");
            }
            this.grvImages.DataSource = dtAll;
            this.grvImages.DataBind();
        }


        /// <summary>
        /// 默认的图片信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetDefaultImages()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);
            return ds.Tables["DefaultImage"];
        }


        /// <summary>
        /// 默认的图片信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetUploadImages()
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);
            return ds.Tables["UploadImage"];
        }

        private string GetImagePath()
        {
            string tmp = "";
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);
            DataTable dt = ds.Tables["Config"];
            if (dt != null && dt.Rows.Count > 0)
            {

                tmp=dt.Rows[0][0].ToString();
                if (!tmp.EndsWith("/"))
                {
                    tmp = tmp + "/";
                }
            }
            return tmp;
        }

       

        protected void grvImages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;


                Control c=e.Row.FindControl("rblIsUsed");
                if (c == null)
                { }
                else
                {
                    RadioButtonList rbl = c as RadioButtonList;
                    rbl.SelectedValue = rowView["status"].ToString();
                }

                if (rowView["source"].ToString() == "0") 
                {
                    LinkButton lbEdit = e.Row.FindControl("btnEdit") as LinkButton;
                    LinkButton lbDel = e.Row.FindControl("btnDel") as LinkButton;

                    //lbEdit.Enabled = false;
                    //lbDelt.Enabled = false;
                   // lbEdit.Visible = false;
                    lbDel.Visible = false;



                
                }
                else
                {
                
                }

            }

        }

        protected void grvImages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Response.Write(e.CommandName);
            
            

            string cmd = e.CommandName.ToLower();
            string id = e.CommandArgument.ToString().ToLower();

            if (cmd.Equals("modify"))
            {


                
                this.grvImages.EditIndex = 1;
                bindImages();





                DataRow dr=getById(id);
                if (dr != null)
                {


                    this.txtHiddenId.Value = id;
                    this.divUpload.Visible = true;
                    this.fuImage.Enabled = false;
                    this.btnUpload.Text = "修改";



                    this.txtDescription.Text = dr["description"].ToString();
                    this.txtLink.Text = dr["link"].ToString();
                    this.txtOrder.Text = dr["order"].ToString();
                    this.rblStatus.SelectedValue = dr["status"].ToString();
                    

                
                }
                


            }
            else if (cmd.Equals("del"))
            {
                delById(id);
                bindImages();

                  
            }
            //Response.Write(cmd + ":" + id);

            
        }


        private void delById(string id)
        {

            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);

            string imagePath = GetImagePath();


            DataTable dt1 = ds.Tables["UploadImage"];
            DataRow[] drs = dt1.Select("id='"+id+"'");
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                foreach (DataRow dr in drs)
                {
                    
                    //删除图片
                    string imgFile = Server.MapPath(imagePath + dr["src"].ToString() + ".jpg");
                    if(File.Exists(imgFile))
                    {
                        File.Delete(imgFile);
                    }


                    dr.Delete();
                }
            }

            ds.WriteXml(xmlFile);
        
        }


        private DataRow getById(string id)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);


            DataTable dtUpload = ds.Tables["UploadImage"];
            DataTable dtDefault=ds.Tables["DefaultImage"];


            if (dtUpload == null)
            {
                dtUpload = new DataTable("UploadImage");
                foreach (DataColumn columm in dtDefault.Columns)
                {
                    dtUpload.Columns.Add(columm.ColumnName, columm.DataType);
                    

                }   
            }



            DataRow[] drs = dtUpload.Select("id='" + id + "'");
            if (drs.Length == 0)
            {
                DataRow[] drDefault = dtDefault.Select("id='" + id + "'");

                if (drDefault.Length > 0)
                {
                    return drDefault[0];
                }

            }
            else
            {
                return drs[0];
            }

            return null;
        
        }

        private void AddNewImage()
        {
        
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            this.divUpload.Visible = true;

            this.fuImage.Enabled = true;
            this.btnUpload.Text = "上传";

            this.txtDescription.Text = "";
            this.txtLink.Text = "";
            this.txtOrder.Text = "1";
            this.rblStatus.SelectedValue = "1";
        }

        protected void btnUpload_Click1(object sender, EventArgs e)
        {
            //检查图片文件是否正确

            if (this.btnUpload.Text.Equals("上传"))
            {
                string strFile = "";
                if (this.fuImage.HasFile)
                {
                    strFile = this.fuImage.FileName.ToLower();

                    if (!strFile.EndsWith(".jpg"))
                    {
                        ShowMessage("只能上传jpg格式的图片");
                        return;
                    }

                    
                    string filePath = this.GetImagePath();
                    string newFileName = DateTime.Now.ToString("yyyyMMddhhmmss");
                    string serverFileName = Server.MapPath(filePath + newFileName + ".jpg");
                    this.fuImage.SaveAs(serverFileName);
                    string description = this.txtDescription.Text.Trim();
                    string link = this.txtLink.Text.Trim();
                    string order = this.txtOrder.Text.Trim();
                    string id = Guid.NewGuid().ToString();
                    string status = this.rblStatus.SelectedValue.ToString();


                    //图片的要求  宽>500  宽:高 <= 2.0

                    ImageController ic = new ImageController(serverFileName);
                    long w = ic.Width;
                    long h = ic.Height;

                    if (w > 550 && ((w / h) < 2.0))

                    { }
                    else
                    {
                        ShowMessage("上传的图片不符合要求，请重新上传。");
                        return;
                    }




                    DataSet ds = new DataSet();
                    ds.ReadXml(xmlFile);
                    DataTable dt = ds.Tables["UploadImage"];
                    DataTable dtDefault = ds.Tables["DefaultImage"];

                    if (dt == null)
                    {
                        dt = new DataTable("UploadImage");
                        foreach (DataColumn columm in dtDefault.Columns)
                        {
                            dt.Columns.Add(columm.ColumnName, columm.DataType);                           

                        }
                        ds.Tables.Add(dt);
                    }



                    DataRow newDr = dt.NewRow();
                    newDr["id"] = id;
                    newDr["src"] = newFileName;
                    newDr["link"] = link;
                    newDr["description"] = description;
                    newDr["order"] = order;
                    newDr["status"] = status;

                    dt.Rows.Add(newDr);
                    ds.WriteXml(xmlFile);

                    this.bindImages();
                    this.divUpload.Visible = false;

                }
                else
                {
                    ShowMessage("请选择一个图片文件上传");
                }
            }
            else if (this.btnUpload.Text.Equals("修改"))
            {
                string description = this.txtDescription.Text.Trim();
                string link = this.txtLink.Text.Trim();
                string order = this.txtOrder.Text.Trim();
                string id = this.txtHiddenId.Value;
                string status = this.rblStatus.SelectedValue.ToString();


                DataSet ds = new DataSet();
                ds.ReadXml(xmlFile);
                
                DataTable dtUpload = ds.Tables["UploadImage"];
                DataTable dtDefault = ds.Tables["DefaultImage"];


                if (dtUpload == null)
                {
                    dtUpload = new DataTable("UploadImage");
                    foreach (DataColumn columm in dtDefault.Columns)
                    {
                        dtUpload.Columns.Add(columm.ColumnName, columm.DataType);
                    }
                }



                DataRow[] drs = dtUpload.Select("id='" + id + "'");
                if (drs.Length == 0)
                {
                    DataRow[] drDefault = dtDefault.Select("id='" + id + "'");

                    if (drDefault.Length > 0)
                    {
                        drDefault[0]["description"]=description;
                        drDefault[0]["link"] = link;
                        drDefault[0]["order"] = order;
                        drDefault[0]["status"] = status;
                        
                    }

                }
                else
                {
                    drs[0]["description"] = description;
                    drs[0]["link"] = link;
                    drs[0]["order"] = order;
                    drs[0]["status"] = status;
                }


                ds.WriteXml(xmlFile);
                bindImages();
                this.divUpload.Visible = false;
                ShowMessage("修改成功");


            }
        }

        protected void grvImages_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //this.grvImages.EditIndex = e.RowIndex;
            //bindImages();
            //Response.Write("ddd"+e.RowIndex.ToString());

            string id = this.grvImages.DataKeys[e.RowIndex].Value.ToString();




            string description=((TextBox)this.grvImages.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string link = ((TextBox)this.grvImages.Rows[e.RowIndex].Cells[2].Controls[0]).Text;
            string order = ((TextBox)this.grvImages.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string status = ((RadioButtonList)this.grvImages.Rows[e.RowIndex].FindControl("rblIsUsed")).SelectedValue;


            //Response.Write(id + ":" + description);




            //string description = this.txtDescription.Text.Trim();
            //string link = this.txtLink.Text.Trim();
            //string order = this.txtOrder.Text.Trim();
            //string id = this.txtHiddenId.Value;
            //string status = this.rblStatus.SelectedValue.ToString();


            DataSet ds = new DataSet();
            ds.ReadXml(xmlFile);

            DataTable dtUpload = ds.Tables["UploadImage"];
            DataTable dtDefault = ds.Tables["DefaultImage"];


            if (dtUpload == null)
            {
                dtUpload = new DataTable("UploadImage");
                foreach (DataColumn columm in dtDefault.Columns)
                {
                    dtUpload.Columns.Add(columm.ColumnName, columm.DataType);
                }
            }



            DataRow[] drs = dtUpload.Select("id='" + id + "'");
            if (drs.Length == 0)
            {
                DataRow[] drDefault = dtDefault.Select("id='" + id + "'");

                if (drDefault.Length > 0)
                {
                    drDefault[0]["description"] = description;
                    drDefault[0]["link"] = link;
                    drDefault[0]["order"] = order;
                    drDefault[0]["status"] = status;

                }

            }
            else
            {
                drs[0]["description"] = description;
                drs[0]["link"] = link;
                drs[0]["order"] = order;
                drs[0]["status"] = status;
            }


            ds.WriteXml(xmlFile);


            this.grvImages.EditIndex = -1;
            bindImages();
            //this.divUpload.Visible = false;
            //ShowMessage("修改成功");



        }

        protected void grvImages_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.grvImages.EditIndex = e.NewEditIndex;
            bindImages();

            
        }

        protected void grvImages_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.grvImages.EditIndex = -1;
            bindImages();
        }

        public string GetCode(string s)
        {
            return s == "1" ? "可用" : "禁用";
        }

        protected void grvImages_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = this.grvImages.DataKeys[e.RowIndex].Value.ToString();
            delById(id);
            bindImages();
        }

        protected void grvImages_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            
        }
    }
}
