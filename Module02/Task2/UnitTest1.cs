using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task2
{
    [TestClass]
    public class ChangeExpressionTest
    {
        public class SwapParamssWIthConstsTransformVisitor : ExpressionVisitor
        {
            private List<Parameter> _parameters;

            public Expression Modify(Expression expression, List<Parameter> parameters)
            {
                this._parameters = parameters;
                return VisitAndConvert(expression,"");
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                var test = _parameters.Find(x => x.ParameterName == node.Name);
                if (test!= null)
                {
                    return Expression.Constant(test.Value, test.Type);
                }
                return base.VisitParameter(node);
            }

            protected override Expression VisitLambda<T>(Expression<T> node)
            {
                return Expression.Lambda<Func<int>>(Visit(node.Body));
            }
        } 


        [TestMethod]
        public void TestMethod1()
        {
            Expression<Func<int, int>> source_exp = (a) => a + (a + 1) * (a + 5) * (a - 1);
            var test = new List<Parameter>();
            test.Add(new Parameter{ParameterName = "a", Type = typeof(Int32), Value = 1});
            var result_exp = new SwapParamssWIthConstsTransformVisitor().Modify(source_exp, test);
            Console.WriteLine(result_exp);
        }
    }

    public class Parameter
    {
        public string ParameterName { get; set; }
        public Type Type { get; set; }
        public object Value { get; set; }
    }
}
