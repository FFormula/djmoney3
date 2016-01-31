using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace djMoney3.Models
{
    public class Pages
    {
        public Article[] articles;
        MySQL sql;

        public Pages ()
        {
            articles = null;
            sql = new MySQL();
        }

        public void Load ()
        {
            DataTable table = sql.Select("SELECT * FROM story ORDER BY id DESC LIMIT 40");
            articles = new Article[table.Rows.Count];
            for (int j = 0; j < table.Rows.Count; j++)
                articles[j] = new Article(table.Rows[j]);
        }

        public Article [] GetArticles ()
        {
            return articles;
        }
    }
}