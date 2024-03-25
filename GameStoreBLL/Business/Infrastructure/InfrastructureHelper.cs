using Business.Enums;

namespace Business.Infrastructure
{
    public static class InfrastructureHelper
    {
        public static DatabaseEnum DefineDatabase(int id, int dbValue)
        {
            if (id >= dbValue)
            {
                return DatabaseEnum.SQL;
            }
            else
            {
                return DatabaseEnum.Mongo;
            }
        }
    }
}
