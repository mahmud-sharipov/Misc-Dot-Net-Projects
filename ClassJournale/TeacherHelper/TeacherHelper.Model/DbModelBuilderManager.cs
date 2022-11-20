using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TeacherHelper.Model
{
    public class DbModelBuilderManager
    {
        public static string ModelPrefix => "";
        public static string UIPrefix => "";

        public static void BuildModels(DbModelBuilder dbModelBuilder)
        {
            EntityBaseConfiguration.Configure(dbModelBuilder, ModelPrefix);
            UserConfiguration.Configure(dbModelBuilder, ModelPrefix);
            StudentConfiguration.Configure(dbModelBuilder, ModelPrefix);
            ParentConfiguration.Configure(dbModelBuilder, ModelPrefix);
            TeacherConfiguration.Configure(dbModelBuilder, ModelPrefix);
            JournalSettingsConfiguration.Configure(dbModelBuilder, ModelPrefix);
            SubjectConfiguration.Configure(dbModelBuilder, ModelPrefix);

        }
    }
}
