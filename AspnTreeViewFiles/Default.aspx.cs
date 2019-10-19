using System;
using System.IO;
using System.Web.UI.WebControls;
namespace Aspnet_TvArquivos
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //Tv1.Nodes[0].Value = Server.MapPath("Arquivos");
            }
        }
        protected void Tv1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            if (IsCallback == true)
            {
                if (e.Node.ChildNodes.Count == 0)
                {
                    LoadChildNode(e.Node);
                }
            }
        }
        private void LoadChildNode(TreeNode node)
        {
            DirectoryInfo directory = null;
            directory = new DirectoryInfo(node.Value);
            foreach (DirectoryInfo subtree in directory.GetDirectories())
            {
                TreeNode subNode = new TreeNode(subtree.Name);
                subNode.Value = subtree.FullName;
                try
                {
                    if (subtree.GetDirectories().Length > 0 | subtree.GetFiles().Length > 0)
                    {
                        subNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                        subNode.PopulateOnDemand = true;
                        subNode.NavigateUrl = "#";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                node.ChildNodes.Add(subNode);
            }
            foreach (FileInfo fi in directory.GetFiles())
            {
                TreeNode subNode = new TreeNode(fi.Name);
                node.ChildNodes.Add(subNode);
                subNode.NavigateUrl = "Arquivos/" + fi.Name.ToString();
                //subNode.NavigateUrl = Server.MapPath(fi.Name.ToString());
            }
        }
    }
}