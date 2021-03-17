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
            object instance = Activator.CreateInstance(type, new object[] { });

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

        public string AnalyzeAccessModifiers(string className)
        {
            StringBuilder sb = new StringBuilder();

            Type type = Type.GetType(className);

            FieldInfo[] fieldInfo = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            MethodInfo[] publicMethodInfo = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            MethodInfo[] nonPublicMethodsInfo = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (FieldInfo field in fieldInfo)
            {
                sb.AppendLine($"{field.Name} must be private!");
            }

            foreach (MethodInfo getter in publicMethodInfo.Where(x => x.Name.StartsWith("get")))
            {
                sb.AppendLine($"{getter.Name} must be public!");
            }

            foreach (MethodInfo setters in nonPublicMethodsInfo.Where(x => x.Name.StartsWith("set")))
            {
                sb.AppendLine($"{setters.Name} must be private!");
            }
            return sb.ToString().Trim();
        }

        public string RevealPrivateMethods(string className)
        {
            StringBuilder sb = new StringBuilder();

            Type type = Type.GetType(className);

            TypeInfo info = type.GetTypeInfo();
            sb.AppendLine($"All Private Methods of Class: {info.FullName}");
            sb.AppendLine($"Base Class: {info.BaseType.Name}");

            MethodInfo[] privateMethodInfo = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (MethodInfo methodinfo in privateMethodInfo)
            {
                sb.AppendLine($"{methodinfo.Name}");
            }

            return sb.ToString().Trim();
        }

        public string GetGetterAndSetterReturnType(string className)
        {
            Type type = Type.GetType(className);

            MethodInfo[] getters = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(x => x.Name.StartsWith("get")).ToArray();

            MethodInfo[] setters = type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(x => x.Name.StartsWith("set"))
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (MethodInfo getter in getters)
            {
                sb.AppendLine($"{getter.Name} will return {getter.ReturnType.FullName}");
            }

            foreach (MethodInfo setter in setters)
            {
                sb.AppendLine($"{setter.Name} will set field {setter.GetParameters().First().ParameterType}");
            }

            return sb.ToString().Trim();
        }
    }
}