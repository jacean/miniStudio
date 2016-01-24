using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace miniStudio
{
    public partial class searchForm : Form
    {
        public searchForm()
        {
            InitializeComponent();
        }
        public string searchString = "";
        private void button1_Click(object sender, EventArgs e)
        {
            dg.Rows.Clear();
            nameLc.Clear();
            comboBox1.Items.Clear();
            propertyGrid1.SelectedObjects = null;
            textLc.Clear();
            comboBox2.Items.Clear();
            propertyGrid2.SelectedObjects = null;
            commentLc.Clear();
            comboBox3.Items.Clear();
            propertyGrid3.SelectedObjects = null;
          
            lc = mainForm.dictControls.Keys.ToList<Control>();
            if (lc.Count == 0) return;
              
            searchString = textBox1.Text.Trim();
          
            for (int i = 0; i < lc.Count; i++)
            {
                filter = new string[] { "0", "0", "0" };
                if (lc[i].Name.Contains(searchString))
                {
                    nameLc.Add(lc[i]);
                    nameps.Add(new property(lc[i]));
                    comboBox1.Items.Add(lc[i].Name);
                    filter[0] = "1";
                }
                if (lc[i].Text.Contains(searchString))
                {
                    textLc.Add(lc[i]); textps.Add(new property(lc[i])); comboBox2.Items.Add(lc[i].Name); filter[1] = "1";
                }
                privateInfo p = (privateInfo)lc[i].Tag;
                if (p.comment.Contains(searchString))
                {
                    commentLc.Add(lc[i]); commentps.Add(new property(lc[i])); comboBox3.Items.Add(lc[i].Name); filter[2] = "1";
                }
                if (filter.Contains("1")) dictPro.Add(new property(lc[i]), new string[] { filter[0], filter[1], filter[2] });
            }
            addNewRow(dictPro);
            searchName();
            searchText();
            searchComment();
            dg.EnableHeadersVisualStyles = false;
            dg.RowHeadersVisible = false;
        }
        List<Control> lc = mainForm.dictControls.Keys.ToList<Control>();
        List<Control> nameLc = new List<Control>();
        List<Control> textLc = new List<Control>();
        List<Control> commentLc = new List<Control>();
        List<property> nameps = new List<property>();
        List<property> textps = new List<property>();
        List<property> commentps = new List<property>();
        string[] filter = new string[] {"0","0","0"};
        Dictionary<property, string[]> dictPro = new Dictionary<property, string[]>();
        void searchName()
        {
           
            if (nameLc.Count == 0) { comboBox1.Text = "未搜索到相关数据"; return; }           
            propertyGrid1.SelectedObjects = nameps.ToArray();
            propertyGrid1.SelectedGridItem = propertyGrid1.EnumerateAllItems().First((item) =>
                item.PropertyDescriptor != null && item.PropertyDescriptor.Name == "Name");
            updateComBox(nameLc, comboBox1);
           
        }
        void searchText()
        {
            if (textLc.Count == 0) { comboBox2.Text = "未搜索到相关数据"; return; }          
            propertyGrid2.SelectedObjects = textps.ToArray();
            propertyGrid2.SelectedGridItem = propertyGrid2.EnumerateAllItems().First((item) =>
                item.PropertyDescriptor != null && item.PropertyDescriptor.Name == "Text");
            updateComBox(textLc, comboBox2);
           
        }
        void searchComment()
        {
            if (commentLc.Count == 0) { comboBox3.Text = "未搜索到相关数据"; return; }
            propertyGrid3.SelectedObjects = commentps.ToArray();
            propertyGrid3.SelectedGridItem = propertyGrid3.EnumerateAllItems().First((item) =>
                           item.PropertyDescriptor != null && item.PropertyDescriptor.Name == "Comment");
            updateComBox(commentLc, comboBox3);            
        }
        void updateComBox(List<Control> lc,ComboBox cb)
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = lc.Select(l => new
            {
                Name = l.Name,
                Text = l.Text
            });
            cb.DataSource = bs;
            cb.DisplayMember = "Name";
            cb.ValueMember = "Name";
            cb.AutoCompleteSource = AutoCompleteSource.ListItems;
            cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb.SelectedIndex = 0;
        }

        private void searchForm_Leave(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void searchForm_Load(object sender, EventArgs e)
        {
            foreach (PropertyInfo x in actFunc.getPropertyInfo(typeof(property)))
            {
                dg.Columns.Add(x.Name, x.Name);
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void searchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void addNewRow(Dictionary<property,string[]> dc)
        {
            foreach (property c in dc.Keys)
            {
                int i = dg.Rows.Add();         
                foreach (PropertyInfo pi in actFunc.getPropertyInfo(typeof(property)))
                {           
                    dg.Rows[i].Cells[pi.Name].Value = pi.GetValue(c, null).ToString();
                    if (dc[c][0] == "1") dg.Rows[i].Cells["name"].Style.BackColor = Color.NavajoWhite;
                    if (dc[c][1] == "1") dg.Rows[i].Cells["text"].Style.BackColor = Color.NavajoWhite;
                    if (dc[c][1] == "1") dg.Rows[i].Cells["comment"].Style.BackColor = Color.NavajoWhite;
                }
            }
        }
               
    }
    public static class PropertyGridExtensions
    {
        public static IEnumerable<GridItem> EnumerateAllItems(this PropertyGrid grid)
        {
            if (grid == null)
                yield break;

            // get to root item
            GridItem start = grid.SelectedGridItem;
            while (start.Parent != null)
            {
                start = start.Parent;
            }

            foreach (GridItem item in start.EnumerateAllItems())
            {
                yield return item;
            }
        }

        public static IEnumerable<GridItem> EnumerateAllItems(this GridItem item)
        {
            if (item == null)
                yield break;

            yield return item;
            foreach (GridItem child in item.GridItems)
            {
                foreach (GridItem gc in child.EnumerateAllItems())
                {
                    yield return gc;
                }
            }
        }
    }
}
