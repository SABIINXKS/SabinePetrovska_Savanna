using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SavannaContracts;

namespace SavannaEngine
{
    public class AnimalPluginLoader
    {
        public IEnumerable<IAnimalBehavior> LoadAnimalPlugins(string pluginDirectory)
        {
            var animalBehaviors = new List<IAnimalBehavior>();

            if (!Directory.Exists(pluginDirectory))
                return animalBehaviors;

            var dllFiles = Directory.GetFiles(pluginDirectory, "*.dll");
            foreach (var dll in dllFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dll);
                    var types = assembly.GetTypes()
                        .Where(t => typeof(IAnimalBehavior).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    foreach (var type in types)
                    {
                        if (Activator.CreateInstance(type) is IAnimalBehavior behavior)
                        {
                            animalBehaviors.Add(behavior);
                        }
                    }
                }
                catch
                {
                    // Optionally log or handle plugin load errors
                }
            }

            return animalBehaviors;
        }
    }
}