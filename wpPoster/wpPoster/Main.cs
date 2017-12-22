using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JoeBlogs;

namespace wpPoster
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        public int postWordpress(string title_, string body_, string category_, string keywords_)
        {
            try
            {
                // you should change constructor values to yours info
                WordPressWrapper wp = new WordPressWrapper("http://mywordpress.com/xmlrpc.php", "USERNAME","PASSWORD",1);

                // our keywords count can more than 1, so we can add whole keywords to string list
                string[] keyList;
                if (keywords_.Contains(' ')) keyList = keywords_.Split(',');
                else
                {
                    keyList = new string[1];
                    keyList[0] = keywords_;
                }
                // we created new object in Post Class and fill required post fields through this object
                var post = new Post();
                post.DateCreated = DateTime.Now;

                post.Title = title_;
                post.Tags = keyList;
                post.Categories = new string[] { category_ };
                post.Body = body_;
                int index = wp.NewPost(post, true);
                return index; // it will publish post and return post id
            }
            catch (Exception ex)
            {
                // if there is a mistake, the catch block will show message to us
                MessageBox.Show(ex.Message);
            }
            return 0; // if there is a mistake it will return 0
        }
        private void sendButton_Click(object sender, EventArgs e)
        {
            // it will call our function and get post id
            int postId = postWordpress(title.Text, body.Text, category.Text, keywords.Text);
            MessageBox.Show(postId.ToString());
        }
    }
}
