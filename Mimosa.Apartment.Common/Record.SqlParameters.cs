using System;
using System.Data.SqlClient;

namespace Mimosa.Apartment.Common
{
    public abstract partial class Record
    {
        public abstract SqlParameter[] SqlParameters();
    }
}
