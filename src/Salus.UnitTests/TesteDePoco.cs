namespace Salus.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;
    using TheJoyOfCode.QualityTools;

    [TestClass]
    public class TesteDePoco<T> where T : new()
    {
        protected T entity;

        [TestInitialize]
        public void SetupPropertyAndConstructorTest()
        {
            this.entity = new T();
        }

        [TestMethod]
        public void ChecaPropriedades()
        {
            var propertyTester = new PropertyTester(new T());
            this.IgnorePropertiesOfCollectionType(propertyTester);
            propertyTester.TestProperties();
        }

        [TestMethod]
        public void ChecaConstrutores()
        {
            new ConstructorTester(typeof(T)).TestConstructors(true);
        }

        private void IgnorePropertiesOfCollectionType(PropertyTester propertyTester)
        {
            typeof(T).GetProperties()
                .Where(x => x.PropertyType == typeof(ICollection<>) == false)
                .Select(x => x)
                .ToList()
                .ForEach(x => propertyTester.IgnoredProperties.Add(x.Name));
        }
    }
}