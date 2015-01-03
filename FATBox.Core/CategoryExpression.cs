using System;
using System.Collections.Generic;
using System.Linq;
using FATBox.Core.ModCatalog;

namespace FATBox.Ui
{
    public class CategoryExpression
    {
        private ExpressionToken[][] _expressions;

        public CategoryExpression(string expression)
        {
            var expressionStrings = expression.Split(',');
            _expressions = expressionStrings
                .Select(es => es.Split(' ')
                    .Select(t => {
                                     var cat = t;
                                     if (!String.IsNullOrEmpty(t))
                                     {
                                         var exclude = cat.StartsWith("-");
                                         if (exclude) cat = cat.Substring(1);
                                         return new ExpressionToken {cat = cat, exclude = exclude};
                                     }
                                     else
                                     {
                                         return null;
                                     }
                    })
                    .Where(x => x != null)
                    .ToArray()
                ).ToArray();

            IsEmpty = !_expressions.Any(y => y.Any());
        }

        public bool IsEmpty { get; set; }

        public bool Matches(Blueprint bp)
        {
            return Matches(bp.Categories);
        }

        public bool Matches(IEnumerable<string> categories)
        {
            if (categories == null) return false;
            foreach (var ex in _expressions)
            {
                var match = true;
                foreach (var ct in ex)
                {
                    var hasCat = categories.Contains(ct.cat);
                    var isGood = ct.exclude ? !hasCat : hasCat;
                    match = match & isGood;
                }
                if (match) return true;
            }
            return false;
        }

        private class ExpressionToken
        {
            public string cat { get; set; }
            public bool exclude { get; set; }
        }
    }
}