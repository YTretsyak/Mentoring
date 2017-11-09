using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample03
{
	public class ExpressionToFTSRequestTranslator : ExpressionVisitor
	{
		StringBuilder resultString;
	  List<string> queries = new List<string>();

    public string Translate(Expression exp)
		{
			resultString = new StringBuilder();
			Visit(exp);

			return resultString.ToString();
		}

	  public List<string> TranslateNew(Expression exp)
	  {
	    resultString = new StringBuilder();
	    queries = new List<string>();
	    Visit(exp);

	    string toAdd = resultString.ToString();

	    if (!string.IsNullOrWhiteSpace(toAdd))
	    {
	      queries.Add(toAdd);
	    }
	    return queries;
    }

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (node.Method.DeclaringType == typeof(Queryable)
				&& node.Method.Name == "Where")
			{
				var predicate = node.Arguments[1];
				Visit(predicate);

				return node;
			}

      if (node.Method.DeclaringType == typeof(string) && node.Method.Name == "StartsWith")
      {
        Visit(node.Object);

        resultString.Append("(");
        VisitConstant((ConstantExpression)node.Arguments[0]);
        resultString.Append("*)");

        return node;
      }

		  if (node.Method.DeclaringType == typeof(string) && node.Method.Name == "EndsWith")
		  {
		    Visit(node.Object);

		    resultString.Append("(*");
        VisitConstant((ConstantExpression)node.Arguments[0]);
		    resultString.Append(")");

		    return node;
		  }

		  if (node.Method.DeclaringType == typeof(string) && node.Method.Name == "Contains")
		  {
		    Visit(node.Object);

		    resultString.Append("(*");
		    VisitConstant((ConstantExpression)node.Arguments[0]);
        resultString.Append("*)");

		    return node;
		  }

      return base.VisitMethodCall(node);
		}

    protected override Expression VisitBinary(BinaryExpression node)
		{
			switch (node.NodeType)
			{
				case ExpressionType.Equal:
					if (!(node.Left.NodeType == ExpressionType.MemberAccess))
						throw new NotSupportedException(string.Format("Left operand should be property or field", node.NodeType));

					if (!(node.Right.NodeType == ExpressionType.Constant))
						throw new NotSupportedException(string.Format("Right operand should be constant", node.NodeType));

					Visit(node.Left);
					resultString.Append("(");
					Visit(node.Right);
					resultString.Append(")");
					break;

        case ExpressionType.And:
        case ExpressionType.AndAlso:
          var lTranslator = new ExpressionToFTSRequestTranslator();
          var result = lTranslator.TranslateNew(node.Left);
          var rTranslator = new ExpressionToFTSRequestTranslator();
          var result2 = rTranslator.TranslateNew(node.Right);
          queries.AddRange(result);
          queries.AddRange(result2);
        break;

				default:
					throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
			};

			return node;
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			resultString.Append(node.Member.Name).Append(":");

			return base.VisitMember(node);
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			resultString.Append(node.Value);

			return node;
		}
	}
}
