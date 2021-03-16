using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Stealer
{
    public class Spy
    {
        public string StealFieldInfo(string classToInvestigate, params string[] fields)
        {
            Type type = Type.GetType(classToInvestigate);
            object instance = Activator.CreateInstance(type, new object[]{ });

            FieldInfo[] info = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static |
                                              BindingFlags.NonPublic);
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Class under investigation: {type.GetType().Name}");

            foreach (var field in info.Where(x => fields.Contains(x.Name)))
            {
                sb.AppendLine($"{field.Name} = {field.GetValue(instance)}");
            }

            return sb.ToString().Trim();
        }
    }
}