﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radix.DatabaseManagement
{
    public class SQLDeleteQuery : SQLQuery
    {
        private Dictionary<string, string> mWhereValue = new Dictionary<string, string>();


        protected void AddWhereValue(string pRow, string pValue)
        {
            //Check is exist
            mWhereValue.Add(pRow, pValue);
        }

        public override string GetQuery()
        {
            string query = "DELETE FROM ";
            query += TableName;
            query += " WHERE ";

            int index = 0;
            foreach (KeyValuePair<string, string> pair in mWhereValue)
            {
                query += pair.Key;
                query += "='";
                query += pair.Value;
                query += "'";

                if (index != mWhereValue.Count - 1)
                {
                    query += " AND ";
                }

                index++;
            }

            query += ";";

            return query;
        }
    }
}
