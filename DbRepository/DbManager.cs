using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Linq;
using System.Media;
using BaseCore;
using BaseCore.Model;
using DbRepository.Models;

namespace DbRepository
{
    [Export(typeof(ISerializator<AssemblyBase>))]
    public class DbManager : ISerializator<AssemblyBase>
    {
        public void Serialize(AssemblyBase assemblyBase)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DbSaverContext>());
            AssemblyDbSaver assembly = new AssemblyDbSaver(assemblyBase);

            using (var context = new DbSaverContext())
            {
                context.AssemblyDbSavers.Add(assembly);
                context.SaveChanges();
                SystemSounds.Beep.Play();
            }
        }

        public AssemblyBase Deserialize()
        {
            AssemblyBase assembly = new AssemblyBase();
            using (var context = new DbSaverContext())
            {
                context.AssemblyDbSavers.Load();
                context.NamespaceDbSavers.Load();
                context.TypeDbSavers.Load();
                context.MethodDbSavers.Load();
                context.PropertyDbSavers.Load();
                context.ParameterDbSavers.Load();

                assembly = DataTransferGraphMapper.AssemblyBase(context.AssemblyDbSavers.First());
            }

            return assembly;
        }
    }
}