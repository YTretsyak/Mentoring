using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task3
{
    [TestClass]
    public class TestClass
    {
        [TestMethod]
        public void TestMethod3()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>(new Foo { IntProp = 5, stringProp = "5" });

            var res = mapper.Map();

            Assert.AreEqual(res.IntProp, 5);
            Assert.AreEqual(res.stringProp, "5");
        }
    }

    public class Mapper<TDestination>
    {
        Func<TDestination> mapFunction;
        internal Mapper(Expression<Func<TDestination>> expression)
        {
            mapFunction = expression.Compile();
        }
        public TDestination Map()
        {
            return mapFunction.Invoke();
        }
    }

    public class MappingGenerator
    {
        public Mapper<TDestination> Generate<TSource, TDestination>(TSource source) where TSource : new()
        {
            if (source == null)
            {
                throw new ArgumentException("Source");
            }
            var sourceType = Type.GetType(typeof(TSource).ToString(), false, true);
            var destType = Type.GetType(typeof(TDestination).ToString(), false, true);
            List<MemberBinding> bindings = new List<MemberBinding>();

            foreach (var field in destType.GetFields())
            {
                var fieldToMigrate = sourceType.GetFields().First(x => x.Name == field.Name && x.FieldType == field.FieldType);
                if (fieldToMigrate != null)
                {
                    var value = fieldToMigrate.GetValue(source);
                    var parameter = Expression.Constant(value, fieldToMigrate.FieldType);

                    var fieldInfo = destType.GetField(fieldToMigrate.Name);
                    var binding = Expression.Bind(fieldInfo, parameter);
                    bindings.Add(binding);
                }
            }

            foreach (var property in destType.GetProperties())
            {
                var propertyToMigrate = sourceType.GetProperties().First(x => x.Name == property.Name && x.PropertyType == property.PropertyType);
                if (propertyToMigrate != null)
                {
                    var value = propertyToMigrate.GetValue(source);
                    var parameter = Expression.Constant(value, propertyToMigrate.PropertyType);
                    var fieldInfo = destType.GetProperty(propertyToMigrate.Name);
                    var binding = Expression.Bind(fieldInfo, parameter);
                    bindings.Add(binding);
                }
            }

            var ctr = Expression.New(typeof(TDestination));
            var memberInit = Expression.MemberInit(ctr, bindings);
            var lambda = Expression.Lambda<Func<TDestination>>(memberInit);

            return new Mapper<TDestination>(lambda);
        }
    }

    public class Foo
    {
        public string stringProp;
        public int IntProp { get; set; }
    }

    public class Bar
    {
        public string stringProp;
        public int IntProp { get; set; }

        public Bar()
        {

        }

        public Bar(string stringProp, int intProp)
        {
            this.stringProp = stringProp;
            this.IntProp = intProp;
        }
    }

}
