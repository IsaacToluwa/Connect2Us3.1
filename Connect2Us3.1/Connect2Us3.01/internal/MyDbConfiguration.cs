using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Connect2Us3._01.Internal
{
    public class MyDbConfiguration : DbConfiguration
    {
        public MyDbConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new PublicExecutionStrategy());
        }
    }

    public class PublicExecutionStrategy : DbExecutionStrategy
    {
        protected override bool ShouldRetryOn(System.Exception exception)
        {
            return false;
        }
    }
}