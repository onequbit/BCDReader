using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Management;
using ROOT.WMI;

namespace BCDEditor
{
    public partial class Maiin : Form
    {
        public Maiin()
        {
            InitializeComponent();
        }

        private void Maiin_Load(object sender, EventArgs e)
        {
            LoadAllOS();   
        }

        BcdObject systemObject;

        private void LoadAllOS()
        {
            ConnectionOptions connectionOptions = new ConnectionOptions();
            connectionOptions.Impersonation = ImpersonationLevel.Impersonate;
            connectionOptions.EnablePrivileges = true;

            ManagementScope managementScope = new ManagementScope(@"root\WMI", connectionOptions);

            // SYSTEM STORE GUID = {9dea862c-5cdd-4e70-acc1-f32b344d4795}
            systemObject = new BcdObject(managementScope, "{9dea862c-5cdd-4e70-acc1-f32b344d4795}", "");

            ManagementBaseObject mboOut;
            bool success = systemObject.GetElement((uint)BCDConstants.BcdBootMgrElementTypes.BcdBootMgrObjectList_DisplayOrder, out mboOut);

            if (success)
            {
                string[] osIdList = (string[])mboOut.GetPropertyValue("Ids");
                foreach (string osGuid in osIdList)
                {
                    BcdObject osObj = new BcdObject(managementScope, osGuid, "");
                    success = osObj.GetElement((uint)BCDConstants.BcdLibraryElementTypes.BcdLibraryString_Description, out mboOut);

                    if (success)
                    {
                        OS myOS = new OS();
                        myOS.Name = mboOut.GetPropertyValue("String").ToString();
                        myOS.GUID = osGuid;
                        myOS.osObj = osObj;
                        lstOS.Items.Add(myOS);                        
                    }                    
                }
            }

            success = systemObject.GetElement((uint)BCDConstants.BcdBootMgrElementTypes.BcdBootMgrInteger_Timeout, out mboOut);
            if (success)
            {
                txtTimeOut.Text = mboOut.GetPropertyValue("Integer").ToString();
            }
        }

        private void lstOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstOS.SelectedIndex != -1)
            {
                OS myOS = (OS)lstOS.SelectedItem;
                lblOSGUID.Text = myOS.GUID;
                txtDisplayName.Text = myOS.Name;
                BcdObject osObj = myOS.osObj;
                ManagementBaseObject mboOut;
                bool success=osObj.GetElement((uint)BCDConstants.BcdBootMgrElementTypes.BcdBootMgrObject_DefaultObject, out mboOut);

                chkDefault.Checked = success;
            }
        }

        private void btnOSSet_Click(object sender, EventArgs e)
        {
            if (txtDisplayName.Text.Length > 0)
            {
                DialogResult dr=MessageBox.Show("Are you sure?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        BcdObject osObj = ((OS)lstOS.SelectedItem).osObj;
                        osObj.SetStringElement(txtDisplayName.Text, (uint)BCDConstants.BcdLibraryElementTypes.BcdLibraryString_Description);                        
                    }
                    catch(Exception exep)
                    {
                        MessageBox.Show(exep.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("OS Decription Updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSetTimeout_Click(object sender, EventArgs e)
        {
            if (txtTimeOut.Text.Length > 0)
            {
                DialogResult dr = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {                        
                        systemObject.SetIntegerElement(ulong.Parse(txtTimeOut.Text), (uint)BCDConstants.BcdBootMgrElementTypes.BcdBootMgrInteger_Timeout);
                    }
                    catch (Exception exep)
                    {
                        MessageBox.Show(exep.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    MessageBox.Show("Timeout Updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }

    class OS
    {
        public string Name;
        public string GUID;
        public BcdObject osObj;

        public override string ToString()
        {
            return Name;
        }
    }
}