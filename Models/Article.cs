using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace djMoney3.Models
{
    public class Article
    {
        public string ID;
        public string Title;
        public string PostDate;
        public string Content;
        public string Likes;
        private MySQL sql;

        public Article ()
        {
            sql = new MySQL();
        }

        public Article (DataRow row)
        {
            ID = row["id"].ToString();
            Title = row["title"].ToString();
            PostDate = row["post_date"].ToString();
            Content = row["story"].ToString();
            Likes = row["likes"].ToString();
        }

        public void GetArticle(string id)
        {
            ID = id;
            try
            {
                DataTable story = sql.Select("SELECT id, title, post_date, story, likes FROM story WHERE id = '" + id + "' LIMIT 1");
                Title = story.Rows[0]["title"].ToString();
                PostDate = story.Rows[0]["post_date"].ToString();
                Content = story.Rows[0]["story"].ToString();
                Likes = story.Rows[0]["likes"].ToString();
            }
            catch
            {
                Title = "--";
                PostDate = "2006-01-01";
                Content = ":)";
                Likes = "0";
            }
        }

        public int GetRandom()
        {
            try
            {
                DataTable story = sql.Select(
                    "SELECT id FROM story ORDER BY rand() LIMIT 1");
                return int.Parse(story.Rows[0]["id"].ToString());
            }
            catch
            {
                return 1;
            }
        }

        public void Insert ()
        {
            long id = sql.Insert(
                       @"INSERT INTO story 
                            SET title = '" + sql.addslashes(Title) + @"',
                                post_date = NOW(),
                                story = '" + sql.addslashes(Content) + @"',
                                likes = 0");
            this.ID = id.ToString();
        }

    }
}