using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            lc = mainForm.dictControls.Keys.ToList<Control>();
            searchString = textBox1.Text.Trim();
            searchName();
            searchText();
            searchComment();
        }
        List<Control> lc = mainForm.dictControls.Keys.ToList<Control>();
        List<Control> nameLc = new List<Control>();
        List<Control> textLc = new List<Control>();
        List<Control> commentLc = new List<Control>();

        void searchName()
        {
            nameLc.Clear();
            comboBox1.Items.Clear();
            propertyGrid1.SelectedObjects = null;
            for (int i = 0; i <lc.Count;i++ )
            {
                if (lc[i].Name.Contains(searchString))
                {
                    nameLc.Add(lc[i]);
                    comboBox1.Items.Add(lc[i].Name);
                }
            }
            if (nameLc.Count == 0) { comboBox1.Text = "未搜索到相关数据"; return; }
            List<property> ps = new List<property>();
            foreach (Control c in nameLc)
            {
                ps.Add(new property(c));
            }
            propertyGrid1.SelectedObjects = ps.ToArray();
            propertyGrid1.SelectedGridItem = propertyGrid1.EnumerateAllItems().First((item) =>
                item.PropertyDescriptor != null && item.PropertyDescriptor.Name == "Name");
            BindingSource bs = new BindingSource();
            bs.DataSource = nameLc.Select(l => new
            {
                Name = l.Name,
                Text = l.Text
            });
            comboBox1.DataSource = bs;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Name";
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.SelectedIndex = 0;
        }
        void searchText()
        {
            textLc.Clear();
            comboBox2.Items.Clear();
            propertyGrid2.SelectedObjects = null;
            for (int i = 0; i < lc.Count; i++)
            {
                if (lc[i].Text.Contains(searchString))
                {
                    textLc.Add(lc[i]); comboBox2.Items.Add(lc[i].Name);
                }
            }
            if (textLc.Count == 0) { comboBox2.Text = "未搜索到相关数据"; return; }
            List<property> ps = new List<property>();
            foreach (Control c in textLc)
            {
                ps.Add(new property(c));
            }
            propertyGrid2.SelectedObjects = ps.ToArray();

            propertyGrid2.SelectedGridItem = propertyGrid2.EnumerateAllItems().First((item) =>
                item.PropertyDescriptor != null && item.PropertyDescriptor.Name == "Text");
            BindingSource bs = new BindingSource();
            bs.DataSource = textLc.Select(l => new
            {
                Name = l.Name,
                Text = l.Text
            });
            comboBox2.DataSource = bs;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "Name";
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.SelectedIndex = 0;
        }
        void searchComment()
        {
            commentLc.Clear();
            comboBox3.Items.Clear();
            propertyGrid3.SelectedObjects = null;
            for (int i = 0; i < lc.Count; i++)
            {
                privateInfo p = (privateInfo)lc[i].Tag;
                if (p.comment.Contains(searchString))
                {
                    commentLc.Add(lc[i]); comboBox3.Items.Add(lc[i].Name);
                }
            }
            if (commentLc.Count == 0) { comboBox3.Text = "未搜索到相关数据"; return; }
            List<property> ps = new List<property>();
            foreach (Control c in commentLc)
            {
                ps.Add(new property(c));
            }
            propertyGrid3.SelectedObjects = ps.ToArray();
            propertyGrid3.SelectedGridItem = propertyGrid3.EnumerateAllItems().First((item) =>
                           item.PropertyDescriptor != null && item.PropertyDescriptor.Name == "Comment");
            BindingSource bs = new BindingSource();
            bs.DataSource = commentLc.Select(l => new
            {
                Name = l.Name,
                Text = l.Text
            });
            comboBox3.DataSource = bs;
            comboBox3.DisplayMember = "Name";
            comboBox3.ValueMember = "Name";
            comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox3.SelectedIndex = 0;
        }

        private void searchForm_Leave(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void searchForm_Load(object sender, EventArgs e)
        {
           
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
