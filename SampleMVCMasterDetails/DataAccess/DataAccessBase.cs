using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleMVCMasterDetails.DataAccess
{
    public class DataAccessBase
    {
        private Database _database;

        public Database MyDatabase
        {
            get { return _database; }
            set { _database = value; }
        }

        public DataAccessBase()
        {

            _database = new DatabaseProviderFactory().Create("MyConnectionString");

        }
    }
}